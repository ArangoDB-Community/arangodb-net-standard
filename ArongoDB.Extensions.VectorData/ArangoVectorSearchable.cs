using ArangoDB.Extensions.VectorData.Helpers;
using ArangoDB.Extensions.VectorData.Helpers.LinqExpressionHelpers;

using ArangoDBNetStandard;
using ArangoDBNetStandard.CursorApi.Models;

using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.VectorData;

using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace ArangoDB.Extensions.VectorData;

public sealed class ArangoVectorSearchable<TRecord>(
    string name,
    IServiceProvider serviceProvider
) : IVectorSearchable<TRecord>
{
    public ArangoVectorSearchable(
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
    public async IAsyncEnumerable<VectorSearchResult<TRecord>> SearchAsync<TInput>(
        TInput searchValue,
        int top,
        VectorSearchOptions<TRecord>? options = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    ) where TInput : notnull
    {
        if (top <= 0)
        {
            yield break;
        }

        // Validate that VectorProperty is provided - vector search is meaningless without it
        if (options?.VectorProperty is not Expression<Func<TRecord, object?>> vectorProperty)
        {
            throw new ArgumentNullException(nameof(options.VectorProperty), "VectorProperty must be specified for vector search operations.");
        }

        using IArangoDBClient client = GetRequiredService<IArangoDBClient>();

        // First try to get embedding generator from definition, then from DI
        IEmbeddingGenerator<TInput, Embedding<float>>? generator =
            Definition?.EmbeddingGenerator as IEmbeddingGenerator<TInput, Embedding<float>>
            ?? GetService<IEmbeddingGenerator<TInput, Embedding<float>>>()
            ?? throw new InvalidOperationException("Vector search requires options.EmbeddingGenerator implementing IEmbeddingGenerator<TInput, Embedding<float>>.");

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
            ["limit"] = top
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

        // If caller requested vectors, return the whole document (including vectors).
        string docProjectionExpr = options?.IncludeVectors == true
                                 ? "doc"
                                 : ProcessProjectionexpression(vectorFieldPath);

        string aql = $"FOR doc IN {Name}{filterClause} LET score = COSINE_SIMILARITY({similarityTarget}, @vector) SORT score DESC {limitClause} RETURN {{ doc: {docProjectionExpr}, score: score }}";

        CursorResponse<CursorRow<TRecord>> response = await client.Cursor
            .PostCursorAsync<CursorRow<TRecord>>(
                aql,
                bindVars,
                token: cancellationToken)
            .ConfigureAwait(false);

        foreach (CursorRow<TRecord> row in response.Result)
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

    private static string ProcessProjectionexpression(string? vectorFieldPath)
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
