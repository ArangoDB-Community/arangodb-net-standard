using ArangoDB.Extensions.VectorData.Helpers;
using ArangoDB.Extensions.VectorData.Helpers.LinqExpressionHelpers;

using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi.Models;
using ArangoDBNetStandard.CursorApi.Models;

using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.VectorData;

using System.Linq.Expressions;
using System.Net;
using System.Runtime.CompilerServices;

namespace ArangoDB.Extensions.VectorData;

public sealed partial class ArangoDynamicCollection(
    VectorStore store,
    string name,
    IServiceProvider serviceProvider
) : VectorStoreCollection<object, Dictionary<string, object?>>
{
    private bool _disposedValue = false;

    public ArangoDynamicCollection(
        VectorStore store,
        string name,
        VectorStoreCollectionDefinition? definition,
        IServiceProvider serviceProvider
    ) : this(store, name, serviceProvider)
    {
        Definition = definition;
    }

    public override string Name { get; } = name;

    public VectorStoreCollectionDefinition? Definition { get; }

    public override async Task<bool> CollectionExistsAsync(CancellationToken cancellationToken = default)
    {
        return await store.CollectionExistsAsync(Name, cancellationToken).ConfigureAwait(false);
    }

    public override async Task DeleteAsync(
        object key, 
        CancellationToken cancellationToken = default)
    {
        string id = key.ToString();
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Key string is null or empty.", nameof(key));
        }

        if (!id.Contains('/'))
        {
            id = $"{Name}/{id}";
        }
        using IArangoDBClient client = GetRequiredService<IArangoDBClient>();
        await client.Document
            .DeleteDocumentAsync(id, token: cancellationToken)
            .ConfigureAwait(false);
    }

    public override Task EnsureCollectionDeletedAsync(
        CancellationToken cancellationToken = default)
    {
        return store.EnsureCollectionDeletedAsync(
            Name,
            cancellationToken);
    }

    public override async Task EnsureCollectionExistsAsync(
        CancellationToken cancellationToken = default)
    {
        if (!await CollectionExistsAsync(cancellationToken).ConfigureAwait(false))
        {
            PostCollectionBody body = new()
            {
                Name = Name
            };
            using IArangoDBClient client = GetRequiredService<IArangoDBClient>();
            await client.Collection
                .PostCollectionAsync(
                    body,
                    token: cancellationToken)
                .ConfigureAwait(false);
        }
    }

    public override async Task<Dictionary<string, object?>?> GetAsync(
        object key,
        RecordRetrievalOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        string id = key.ToString();
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Key string is null or empty.", nameof(key));
        }

        if (!id.Contains('/'))
        {
            id = $"{Name}/{id}";
        }

        try
        {
            using IArangoDBClient client = GetRequiredService<IArangoDBClient>();
            Dictionary<string, object?> res = await client.Document
                .GetDocumentAsync<Dictionary<string, object?>>(id, token: cancellationToken)
                .ConfigureAwait(false);
            return res;
        }
        catch (ApiErrorException)
        {
            return null;
        }
    }

    public override async IAsyncEnumerable<Dictionary<string, object?>> GetAsync(
        Expression<Func<Dictionary<string, object?>, bool>> filter,
        int top,
        FilteredRecordRetrievalOptions<Dictionary<string, object?>>? options = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (top <= 0)
        {
            yield break;
        }

        Dictionary<string, object> bindVars = [];
        string whereClause = filter.BuildWhereClause(bindVars);

        string query = string.IsNullOrWhiteSpace(whereClause)
            ? $"FOR doc IN {Name} LIMIT @limit RETURN doc"
            : $"FOR doc IN {Name} FILTER {whereClause} LIMIT @limit RETURN doc";

        bindVars["limit"] = top;

        using IArangoDBClient client = GetRequiredService<IArangoDBClient>();
        CursorResponse<Dictionary<string, object?>> response = await client.Cursor
            .PostCursorAsync<Dictionary<string, object?>>(
                query,
                bindVars,
                token: cancellationToken)
            .ConfigureAwait(false);

        foreach (Dictionary<string, object?> record in response.Result)
        {
            yield return record;
        }
    }

    public override object? GetService(
        Type serviceType,
        object? serviceKey = null)
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

    public override async IAsyncEnumerable<VectorSearchResult<Dictionary<string, object?>>> SearchAsync<TInput>(
        TInput searchValue,
        int top,
        VectorSearchOptions<Dictionary<string, object?>>? options = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (top <= 0)
        {
            yield break;
        }

        using IArangoDBClient client = GetRequiredService<IArangoDBClient>();
        IEmbeddingGenerator<TInput, Embedding<float>>? generator
            = GetService<IEmbeddingGenerator<TInput, Embedding<float>>>()
            ?? throw new InvalidOperationException("Vector search requires options.EmbeddingGenerator implementing IEmbeddingGenerator<TInput, Embedding<float>>.");

        if (Definition is { EmbeddingGenerator: null })
        {
            Definition.EmbeddingGenerator = generator;
        }

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
            ["queryVec"] = vector,
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
        string? vectorFieldPath = options?.IncludeVectors == true && options.VectorProperty is LambdaExpression vecExpr
                                ? vecExpr.BuildMemberAccessPath()
                                : null;
        string? similarityTarget = string.IsNullOrWhiteSpace(vectorFieldPath)
                                ? null
                                : vectorFieldPath;

        // If caller requested vectors, return the whole document (including vectors).
        string docProjectionExpr = options?.IncludeVectors == true
                                 ? "doc"
                                 : ProcessProjectionexpression(vectorFieldPath);

        string aql = $"FOR doc IN {Name}{filterClause} LET score = COSINE_SIMILARITY({similarityTarget}, @queryVec) SORT score DESC {limitClause} RETURN {{ doc: {docProjectionExpr}, score: score }}";

        CursorResponse<CursorRow<Dictionary<string, object?>>> response = await client.Cursor
            .PostCursorAsync<CursorRow<Dictionary<string, object?>>>(
                aql,
                bindVars,
                token: cancellationToken)
            .ConfigureAwait(false);

        foreach (CursorRow<Dictionary<string, object?>> row in response.Result)
        {
            Dictionary<string, object?>? rec = row.Doc;
            double score = row.Score;
            if (rec is not null)
            {
                yield return new VectorSearchResult<Dictionary<string, object?>>(rec, score);
            }
        }
    }

    public override async Task UpsertAsync(
        Dictionary<string, object?> record,
        CancellationToken cancellationToken = default)
    {
        IArangoDBClient client = GetRequiredService<IArangoDBClient>();
        try
        {
            await client.Document
                .PutDocumentAsync(Name, record, token: cancellationToken)
                .ConfigureAwait(false);
        }
        catch (ApiErrorException ex) when (ex.ApiError?.Code == HttpStatusCode.NotFound)
        {
            await client.Document
                .PostDocumentAsync(Name, record, token: cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            client.Dispose();
        }
    }

    public override async Task UpsertAsync(
        IEnumerable<Dictionary<string, object?>> records,
        CancellationToken cancellationToken = default)
    {
        using IArangoDBClient client = GetRequiredService<IArangoDBClient>();
        try
        {
            await client.Document
                .PutDocumentsAsync(Name, records, token: cancellationToken)
                .ConfigureAwait(false);
        }
        catch (ApiErrorException ex) when (ex.ApiError?.Code == HttpStatusCode.NotFound)
        {
            await client.Document
                .PostDocumentsAsync(Name, records, token: cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            client.Dispose();
        }
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (_disposedValue)
        {
            // If already disposed, do nothing
            return;
        }

        if (!disposing)
        {
            // Dispose unmanaged resources here if any
        }

        // Dispose managed resources here if any
        store.Dispose();
        _disposedValue = true;
    }

    private static string ProcessProjectionexpression(string? vectorFieldPath)
    {
        string docProjectionExpr;
        string vectorName = vectorFieldPath ?? "embedding";

        string[] props = typeof(Dictionary<string, object?>)
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

    private T? GetService<T>(
        object? serviceKey = null
    )
    {
        return serviceKey is null
            ? serviceProvider.GetService<T>()
            : serviceProvider.GetKeyedService<T>(serviceKey);
    }

    private T GetRequiredService<T>(
        object? serviceKey = null
    ) where T : notnull
    {
        return serviceKey is null
            ? serviceProvider.GetRequiredService<T>()
            : serviceProvider.GetRequiredKeyedService<T>(serviceKey);
    }
}
