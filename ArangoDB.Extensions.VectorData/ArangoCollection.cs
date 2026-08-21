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
using System.Text;

namespace ArangoDB.Extensions.VectorData;

public sealed partial class ArangoCollection<TKey, TRecord>
    : VectorStoreCollection<TKey, TRecord>
where TKey : notnull
where TRecord : class
{
    private bool _disposedValue = false;
    private readonly VectorStore _store;
    private readonly IServiceProvider _serviceProvider;

    public ArangoCollection(
        VectorStore store,
        string name,
        IServiceProvider serviceProvider
    )
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(
                nameof(name),
                "Collection name cannot be null or empty.");
        }
        (Name, _store, _serviceProvider) = (name, store, serviceProvider);
    }

    public ArangoCollection(
        VectorStore store,
        string name,
        VectorStoreCollectionDefinition? definition,
        IServiceProvider serviceProvider
    ) : this(store, name, serviceProvider)
    {
        Definition = definition;
    }

    public override string Name { get; }

    public VectorStoreCollectionDefinition? Definition { get; }

    public override async Task<bool> CollectionExistsAsync(CancellationToken cancellationToken = default)
    {
        return await _store
            .CollectionExistsAsync(Name, cancellationToken)
            .ConfigureAwait(false);
    }

    public override async Task DeleteAsync(
        TKey key,
        CancellationToken cancellationToken = default)
    {
        string documentId = key.SanitizeKeyAndGetId(Name);

        using IArangoDBClient client = GetRequiredService<IArangoDBClient>();
        await client.Document
            .DeleteDocumentAsync(documentId, token: cancellationToken)
            .ConfigureAwait(false);
    }

    public override async Task EnsureCollectionDeletedAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            await _store.EnsureCollectionDeletedAsync(
                Name,
                cancellationToken);
        }
        catch (ApiErrorException ex) when (ex.ApiError is { Code: HttpStatusCode.NotFound } or { ErrorNum: 1203 })
        {
            // If the collection does not exist, ignore the exception
            return;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public override async Task EnsureCollectionExistsAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            PostCollectionBody body = new()
            {
                Name = Name,
                Type = CollectionType.Document,
            };
            using IArangoDBClient client = GetRequiredService<IArangoDBClient>();
            await client.Collection
                .PostCollectionAsync(
                    body,
                    token: cancellationToken)
                .ConfigureAwait(false);
        }
        catch (ApiErrorException ex) when (ex.ApiError is { Code: HttpStatusCode.Conflict } or { ErrorNum: 1207 })
        {
            // If the collection already exists, ignore the exception
            return;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public override async Task<TRecord?> GetAsync(
        TKey key,
        RecordRetrievalOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        string id = key.SanitizeKeyAndGetId(Name);

        try
        {
            using IArangoDBClient client = GetRequiredService<IArangoDBClient>();
            TRecord res = await client.Document
                .GetDocumentAsync<TRecord>(id, token: cancellationToken)
                .ConfigureAwait(false);
            return res;
        }
        catch (ApiErrorException ex) when (ex.ApiError is { Code: HttpStatusCode.NotFound } or { ErrorNum: 1202 })
        {
            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public override async IAsyncEnumerable<TRecord> GetAsync(
        Expression<Func<TRecord, bool>> filter,
        int top,
        FilteredRecordRetrievalOptions<TRecord>? options = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (top <= 0)
        {
            yield break;
        }

        Dictionary<string, object> bindVars = [];

        bindVars["limit"] = top;
        if (options?.Skip is int skip && skip > 0)
        {
            bindVars["skip"] = skip;
        }
        string limitClause = bindVars.ContainsKey("skip")
                           ? "@skip, @limit"
                           : "@limit";

        StringBuilder queryBuilder = new($"FOR doc IN {Name}");
        string whereClause = filter.BuildWhereClause(bindVars);
        if (!string.IsNullOrWhiteSpace(whereClause))
        {
            queryBuilder.Append($" FILTER {whereClause}");
        }
        FilteredRecordRetrievalOptions<TRecord>.OrderByDefinition orderByDefinition = new();
        string sortClause = options?
                            .OrderBy?
                            .Invoke(orderByDefinition)
                            .BuildOrderByClause()
                         ?? string.Empty;
        if (!string.IsNullOrWhiteSpace(sortClause))
        {
            queryBuilder.Append($" SORT {sortClause}");
        }
        queryBuilder.Append($" LIMIT {limitClause} RETURN doc");

        string query = queryBuilder.ToString();

        using IArangoDBClient client = GetRequiredService<IArangoDBClient>();
        CursorResponse<TRecord> response = await client.Cursor
            .PostCursorAsync<TRecord>(
                query,
                bindVars,
                token: cancellationToken)
            .ConfigureAwait(false);

        foreach (TRecord record in response.Result)
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
            return _serviceProvider.GetService(serviceType);
        }
        try
        {
            return _serviceProvider.GetRequiredKeyedService(serviceType, serviceKey);
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    public override async IAsyncEnumerable<VectorSearchResult<TRecord>> SearchAsync<TInput>(
        TInput searchValue,
        int top,
        VectorSearchOptions<TRecord>? options = null,
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


    public override async Task UpsertAsync(
        TRecord record,
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
        IEnumerable<TRecord> records,
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
        _store.Dispose();
        _disposedValue = true;
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

    private T? GetService<T>(
        object? serviceKey = null
    )
    {
        return serviceKey is null
            ? _serviceProvider.GetService<T>()
            : _serviceProvider.GetKeyedService<T>(serviceKey);
    }

    private T GetRequiredService<T>(
        object? serviceKey = null
    ) where T : notnull
    {
        return serviceKey is null
            ? _serviceProvider.GetRequiredService<T>()
            : _serviceProvider.GetRequiredKeyedService<T>(serviceKey);
    }
}
