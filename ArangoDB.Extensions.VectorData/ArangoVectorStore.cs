using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi.Models;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.VectorData;

using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.CompilerServices;

namespace ArangoDB.Extensions.VectorData;

public class ArangoVectorStore(
    IArangoDBClient client,
    IServiceProvider serviceProvider
) : VectorStore
{
    private bool _disposedValue;

    public override async Task<bool> CollectionExistsAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        try
        {
            GetCollectionResponse getCollectionResponse = await client.Collection
                .GetCollectionAsync(name, cancellationToken);
            return getCollectionResponse.Name == name;
        }
        catch (ApiErrorException ex) when (ex.ApiError?.Code == HttpStatusCode.NotFound)
        {
            return false;
        }
        catch (ApiErrorException ex) when (ex.ApiError?.Code != HttpStatusCode.NotFound)
        {
            throw;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public override async Task EnsureCollectionDeletedAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await client.Collection
                .DeleteCollectionAsync(
                    name,
                    token: cancellationToken)
                .ConfigureAwait(false);
        }
        catch (ApiErrorException ex) when (ex is { ApiError.Code: HttpStatusCode.NotFound })
        {
            // Ignore if doesn't exist
        }
        catch (Exception)
        {
            throw;
        }
    }

    public override VectorStoreCollection<TKey, TRecord> GetCollection<TKey, TRecord>(
        string name,
        VectorStoreCollectionDefinition? definition = null)
    {
        return new ArangoCollection<TKey, TRecord>(
            this,
            name,
            definition,
            serviceProvider);
    }

    public override VectorStoreCollection<object, Dictionary<string, object?>> GetDynamicCollection(
        string name,
        VectorStoreCollectionDefinition definition)
    {
        return new ArangoDynamicCollection(
            this,
            name,
            definition,
            serviceProvider);
    }

    public override object? GetService(Type serviceType, object? serviceKey = null)
    {
        if (serviceKey is null
         || (serviceKey is string serviceKeyString 
          && string.IsNullOrWhiteSpace(serviceKeyString)))
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

    public override async IAsyncEnumerable<string> ListCollectionNamesAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        GetCollectionsResponse response = await client.Collection
            .GetCollectionsAsync(token: cancellationToken)
            .ConfigureAwait(false);
        foreach (GetCollectionsResponseResult col in response.Result)
        {
            yield return col.Name;
        }
    }

    [ExcludeFromCodeCoverage]
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

        // Dispose unmanaged resources here if any
        client.Dispose();

        _disposedValue = true;
    }
}
