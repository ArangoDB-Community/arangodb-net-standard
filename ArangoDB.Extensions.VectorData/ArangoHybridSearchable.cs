using ArangoDB.Extensions.VectorData.Helpers;
using ArangoDB.Extensions.VectorData.Helpers.LinqExpressionHelpers;

using ArangoDBNetStandard;
using ArangoDBNetStandard.CursorApi.Models;

using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.VectorData;

using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace ArangoDB.Extensions.VectorData;

public sealed class ArangoHybridSearchable<TRecord>(
    string name,
    IServiceProvider serviceProvider
) : IKeywordHybridSearchable<TRecord>
{
    public ArangoHybridSearchable(
        string name,
        VectorStoreCollectionDefinition definition,
        IServiceProvider serviceProvider
    ) : this(name, serviceProvider)
    {
        Definition = definition;
    }

    public string Name { get; } = name;

    public VectorStoreCollectionDefinition? Definition { get; }

    public object? GetService(Type serviceType, object? serviceKey = null)
    {
        if (serviceKey is null)
        {
            return serviceProvider.GetService(serviceType);
        }
        try
        {
            return serviceProvider.GetRequiredKeyedService(serviceType, serviceKey);
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<VectorSearchResult<TRecord>> HybridSearchAsync<TInput>(
        TInput searchValue,
        ICollection<string> keywords,
        int top,
        HybridSearchOptions<TRecord>? options = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    ) where TInput : notnull
    {
        if (top <= 0)
        {
            yield break;
        }

        // Validate that VectorProperty is provided - hybrid search requires vector search
        if (options?.VectorProperty is not Expression<Func<TRecord, object?>> vectorProperty)
        {
            throw new ArgumentNullException(nameof(options.VectorProperty), "VectorProperty must be specified for hybrid search operations.");
        }

        // Validate that AdditionalProperty is provided - hybrid search requires keyword search field
        if (options?.AdditionalProperty is not Expression<Func<TRecord, object?>> keywordProperty)
        {
            throw new ArgumentNullException(nameof(options.AdditionalProperty), "AdditionalProperty must be specified for hybrid search operations to identify the keyword search field.");
        }

        using IArangoDBClient client = GetRequiredService<IArangoDBClient>();

        // First try to get embedding generator from definition, then from DI
        IEmbeddingGenerator<TInput, Embedding<float>>? generator =
            Definition?.EmbeddingGenerator as IEmbeddingGenerator<TInput, Embedding<float>>
            ?? GetService<IEmbeddingGenerator<TInput, Embedding<float>>>()
            ?? throw new InvalidOperationException("Hybrid search requires options.EmbeddingGenerator implementing IEmbeddingGenerator<TInput, Embedding<float>>.");

        // Generate embedding for vector search
        Embedding<float> embedding = await generator
            .GenerateAsync(
                searchValue,
                new EmbeddingGenerationOptions
                {

                },
                cancellationToken)
            .ConfigureAwait(false);
        float[] vector = embedding.Vector.Span.ToArray();

        // Build optional filter clause from options.Filter
        Dictionary<string, object> bindVars = new()
        {
            ["vector"] = vector,
            ["limit"] = top,
            ["keywords"] = keywords.ToArray()
        };
        if (options?.Skip is int skip && skip > 0)
        {
            bindVars["skip"] = skip;
        }

        string filterClause = options?.Filter?.BuildFilterClause(bindVars)
                           ?? string.Empty;

        // LIMIT clause supports paging with skip
        string limitClause = bindVars.ContainsKey("skip")
                           ? "LIMIT @skip, @limit"
                           : "LIMIT @limit";

        // Resolve vector field path
        string? vectorFieldPath = vectorProperty is LambdaExpression vecExpr
                                ? vecExpr.BuildMemberAccessPath()
                                : null;
        string? similarityTarget = string.IsNullOrWhiteSpace(vectorFieldPath)
                                ? "doc.embedding" // Default vector field name
                                : vectorFieldPath;

        // Resolve keyword search field path
        string? keywordFieldPath = keywordProperty is LambdaExpression keywordExpr
                                ? keywordExpr.BuildMemberAccessPath()
                                : null;
        string keywordTarget = string.IsNullOrWhiteSpace(keywordFieldPath)
                             ? "doc.text" // Default text field name
                             : $"doc.{keywordFieldPath}";

        // Build keyword search clause using the specified field
        string keywordClause = string.Empty;
        keywordClause = keywords.Count > 0
            ? $@"LET keywordScore = {keywordTarget} IN @keywords ? 1 : 0"
            : "LET keywordScore = 0";

        // If caller requested vectors, return the whole document (including vectors).
        string docProjectionExpr = options?.IncludeVectors == true
                                 ? "doc"
                                 : ProcessProjectionExpression(vectorFieldPath);

        // Combine vector similarity score with keyword score
        // Weight the scores - you can adjust these weights based on your needs
        double vectorWeight = 0.7; // 70% weight for vector similarity
        double keywordWeight = 0.3; // 30% weight for keyword matching

        string aql = $@"FOR doc IN {Name}{filterClause}
            LET vectorScore = COSINE_SIMILARITY({similarityTarget}, @vector)
            {keywordClause}
            LET hybridScore = ({vectorWeight} * vectorScore) + ({keywordWeight} * keywordScore)
            SORT hybridScore DESC
            {limitClause}
            RETURN {{ doc: {docProjectionExpr}, score: hybridScore, vectorScore: vectorScore, keywordScore: keywordScore }}";

        CursorResponse<HybridSearchRow<TRecord>> response = await client.Cursor
            .PostCursorAsync<HybridSearchRow<TRecord>>(
                aql,
                bindVars,
                token: cancellationToken)
            .ConfigureAwait(false);

        foreach (HybridSearchRow<TRecord> row in response.Result)
        {
            TRecord? rec = row.Doc;
            double score = row.Score;
            if (rec is not null)
            {
                yield return new VectorSearchResult<TRecord>(rec, score);
            }
        }
    }

    private T GetRequiredService<T>(
        object? serviceKey = null
    ) where T : notnull
    {
        return serviceKey is null
            ? serviceProvider.GetRequiredService<T>()
            : serviceProvider.GetRequiredKeyedService<T>(serviceKey);
    }

    private T? GetService<T>(
        object? serviceKey = null
    )
    {
        return serviceKey is null
            ? serviceProvider.GetService<T>()
            : serviceProvider.GetKeyedService<T>(serviceKey);
    }

    private static string ProcessProjectionExpression(string? vectorFieldPath)
    {
        string docProjectionExpr;
        string vectorName = vectorFieldPath ?? "embedding";

        string[] props = typeof(TRecord)
            .GetPublicPropertyNamesExcluding(vectorName);

        if (props.Length == 0)
        {
            // If no properties found (or all excluded), return an empty object
            docProjectionExpr = "{}";
        }
        else
        {
            // Build AQL object projection: { prop1: doc.prop1, prop2: doc.prop2, ... }
            string projection = string.Join(
                ", ",
                props.Select(p => $"{p}: doc.{p}")
            );
            docProjectionExpr = $"{{ {projection} }}";
        }

        return docProjectionExpr;
    }
}