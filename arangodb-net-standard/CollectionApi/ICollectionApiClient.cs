using ArangoDBNetStandard.CollectionApi.Models;
using ArangoDBNetStandard.Serialization;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.CollectionApi
{
    /// <summary>
    /// Defines a client to access the ArangoDB Collections API.
    /// </summary>
    public interface ICollectionApiClient
    {
        /// <summary>
        /// Creates a new collection
        /// </summary>
        /// <param name="body">Attributes of the new collection</param>
        /// <param name="options">Query string options for the task.</param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<PostCollectionResponse> PostCollectionAsync(
           PostCollectionBody body,
           PostCollectionQuery options = null, ApiHeaderProperties headers = null,
            CancellationToken token = default);

        /// <summary>
        /// Deletes a collection.
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<DeleteCollectionResponse> DeleteCollectionAsync(string collectionName, ApiHeaderProperties headers = null,
            CancellationToken token = default);

        /// <summary>
        /// Truncates a collection, i.e. removes all documents in the collection.
        /// PUT/_api/collection/{collection-name}/truncate
        /// </summary>
        /// <param name="collectionName">Name of the collection</param>
        /// <param name="headers">Headers (such as transaction id) to use for this operation.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<TruncateCollectionResponse> TruncateCollectionAsync(string collectionName, 
              CollectionHeaderProperties headers = null,
              CancellationToken token = default);

        /// <summary>
        /// Gets count of documents in a collection.
        /// GET/_api/collection/{collection-name}/count
        /// </summary>
        /// <param name="collectionName">Name of the collection</param>
        /// <param name="headers">Headers (such as transaction id) to use for this operation.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<GetCollectionCountResponse> GetCollectionCountAsync(string collectionName, 
              CollectionHeaderProperties headers = null, 
              CancellationToken token = default);

        /// <summary>
        /// Get all collections.
        /// GET/_api/collection
        /// </summary>
        /// <param name="query"></param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<GetCollectionsResponse> GetCollectionsAsync(GetCollectionsQuery query = null, ApiHeaderProperties headers = null,
            CancellationToken token = default);

        /// <summary>
        /// Gets the requested collection.
        /// GET/_api/collection/{collection-name}
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<GetCollectionResponse> GetCollectionAsync(string collectionName, ApiHeaderProperties headers = null,
            CancellationToken token = default);

        /// <summary>
        /// Read properties of a collection.
        /// GET /_api/collection/{collection-name}/properties
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<GetCollectionPropertiesResponse> GetCollectionPropertiesAsync(string collectionName, ApiHeaderProperties headers = null,
            CancellationToken token = default);

        /// <summary>
        /// Rename a collection.
        /// PUT /_api/collection/{collection-name}/rename
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="body"></param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<RenameCollectionResponse> RenameCollectionAsync(
            string collectionName,
            RenameCollectionBody body, ApiHeaderProperties headers = null,
            CancellationToken token = default);

        /// <summary>
        /// Get a revision of the collection. 
        /// GET /_api/collection/{collection-name}/revision
        /// </summary>
        /// <param name="collectionName">Name of the collection</param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<GetCollectionRevisionResponse> GetCollectionRevisionAsync(string collectionName, ApiHeaderProperties headers = null,
            CancellationToken token = default);

        /// <summary>
        /// Changes the properties of a collection
        /// PUT /_api/collection/{collection-name}/properties
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="body"></param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<PutCollectionPropertyResponse> PutCollectionPropertyAsync(
           string collectionName,
           PutCollectionPropertyBody body, ApiHeaderProperties headers = null,
            CancellationToken token = default);

        /// <summary>
        /// Gets the number of documents and additional statistical information about the collection.
        /// GET/_api/collection/{collection-name}/figures
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<GetCollectionFiguresResponse> GetCollectionFiguresAsync(string collectionName, ApiHeaderProperties headers = null,
            CancellationToken token = default);

        /// <summary>
        /// Get the checksum for a specific collection.
        /// GET /_api/collection/{collection-name}/checksum
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="query">Query options.</param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<GetChecksumResponse> GetChecksumAsync(string collectionName,
            GetChecksumQuery query = null, ApiHeaderProperties headers = null,
            CancellationToken token = default);

        /// <summary>
        /// Load Indexes into Memory.
        /// Caches all index entries of this collection into the main memory. 
        /// Therefore it iterates over all indexes of the collection and 
        /// stores the indexed values, not the entire document data, 
        /// in memory.
        /// PUT /_api/collection/{collection-name}/loadIndexesIntoMemory
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<PutLoadIndexesIntoMemoryResponse> PutLoadIndexesIntoMemoryAsync(
           string collectionName, ApiHeaderProperties headers = null,
            CancellationToken token = default);

        /// <summary>
        /// Recalculates the document count of a collection.        
        /// PUT /_api/collection/{collection-name}/recalculateCount
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<PutRecalculateCountResponse> PutRecalculateCountAsync(
           string collectionName, ApiHeaderProperties headers = null,
            CancellationToken token = default);

        /// <summary>
        /// Returns the responsible shard for a document.        
        /// PUT /_api/collection/{collection-name}/responsibleShard
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="body">
        /// Body of the request consisting of key/value
        /// pairs with at least the collection’s shard 
        /// key attributes set to some values.
        /// </param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<PutDocumentShardResponse> PutDocumentShardAsync(
           string collectionName,
           Dictionary<string, object> body, ApiHeaderProperties headers = null,
            CancellationToken token = default);

        /// <summary>
        /// Returns the shard ids of a collection.
        /// This method is only available in a cluster Coordinator.
        /// GET /_api/collection/{collection-name}/shards
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<GetCollectionShardsResponse> GetCollectionShardsAsync(
           string collectionName, ApiHeaderProperties headers = null,
            CancellationToken token = default);

        /// <summary>
        /// Returns the shard ids of a collection.
        /// This method is only available in a cluster Coordinator.
        /// The response also contains shard IDs as object attribute 
        /// keys, and the responsible servers for each shard mapped 
        /// to them. The leader shards will be first in the arrays.
        /// GET /_api/collection/{collection-name}/shards?details=true
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<GetCollectionShardsDetailedResponse> GetCollectionShardsWithDetailsAsync(
           string collectionName, ApiHeaderProperties headers = null,
            CancellationToken token = default);

        /// <summary>
        /// Compacts the data of a collection in order to reclaim disk space.
        /// The operation will compact the document and index data by rewriting
        /// the underlying .sst files and only keeping the relevant entries. 
        /// PUT /_api/collection/{collection-name}/compact
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<PutCompactCollectionDataResponse> PutCompactCollectionDataAsync(
           string collectionName, ApiHeaderProperties headers = null,
            CancellationToken token = default);
    }
}