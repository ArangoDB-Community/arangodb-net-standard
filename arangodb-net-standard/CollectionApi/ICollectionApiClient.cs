using ArangoDBNetStandard.CollectionApi.Models;
using ArangoDBNetStandard.Serialization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.CollectionApi
{
    /// <summary>
    /// Defines a client to access the ArangoDB Collections API.
    /// </summary>
    public interface ICollectionApiClient
    {
        Task<PostCollectionResponse> PostCollectionAsync(
           PostCollectionBody body,
           PostCollectionQuery options = null);

        Task<DeleteCollectionResponse> DeleteCollectionAsync(string collectionName);

        /// <summary>
        /// Truncates a collection, i.e. removes all documents in the collection.
        /// PUT/_api/collection/{collection-name}/truncate
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        Task<TruncateCollectionResponse> TruncateCollectionAsync(string collectionName);

        /// <summary>
        /// Gets count of documents in a collection.
        /// GET/_api/collection/{collection-name}/count
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        Task<GetCollectionCountResponse> GetCollectionCountAsync(string collectionName);

        /// <summary>
        /// Get all collections.
        /// GET/_api/collection
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<GetCollectionsResponse> GetCollectionsAsync(GetCollectionsQuery query = null);

        /// <summary>
        /// Gets the requested collection.
        /// GET/_api/collection/{collection-name}
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        Task<GetCollectionResponse> GetCollectionAsync(string collectionName);

        /// <summary>
        /// Read properties of a collection.
        /// GET /_api/collection/{collection-name}/properties
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        Task<GetCollectionPropertiesResponse> GetCollectionPropertiesAsync(string collectionName);

        /// <summary>
        /// Rename a collection.
        /// PUT /_api/collection/{collection-name}/rename
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task<RenameCollectionResponse> RenameCollectionAsync(
            string collectionName,
            RenameCollectionBody body);

        /// <summary>
        /// Get a revision of the collection. 
        /// GET /_api/collection/{collection-name}/revision
        /// </summary>
        /// <param name="collectionName">Name of the collection</param>
        /// <returns></returns>
        Task<GetCollectionRevisionResponse> GetCollectionRevisionAsync(string collectionName);

        /// <summary>
        /// Changes the properties of a collection
        /// PUT /_api/collection/{collection-name}/properties
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task<PutCollectionPropertyResponse> PutCollectionPropertyAsync(
           string collectionName,
           PutCollectionPropertyBody body);

        /// <summary>
        /// Contains the number of documents and additional statistical information about the collection.
        /// GET/_api/collection/{collection-name}/figures
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        Task<GetCollectionFiguresResponse> GetCollectionFiguresAsync(string collectionName);

        /// <summary>
        /// Get the checksum for a specific collection.
        /// GET /_api/collection/{collection-name}/checksum
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="query">Query options.</param>
        /// <returns></returns>
        Task<GetChecksumResponse> GetChecksumAsync(string collectionName, 
            GetChecksumQuery query = null);

        /// <summary>
        /// Load Indexes into Memory.
        /// Caches all index entries of this collection into the main memory. 
        /// Therefore it iterates over all indexes of the collection and 
        /// stores the indexed values, not the entire document data, 
        /// in memory.
        /// PUT /_api/collection/{collection-name}/loadIndexesIntoMemory
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <returns></returns>
        Task<LoadIndexesIntoMemoryResponse> LoadIndexesIntoMemoryAsync(
           string collectionName);

        /// <summary>
        /// Recalculates the document count of a collection.        
        /// PUT /_api/collection/{collection-name}/recalculateCount
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <returns></returns>
        Task<RecalculateCountResponse> RecalculateCountAsync(
           string collectionName);

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
        /// <returns></returns>
        Task<DocumentShardResponse> PutDocumentShardAsync(
           string collectionName, Dictionary<string,object> body);

        /// <summary>
        /// Returns the shard ids of a collection.
        /// This method is only available in a cluster Coordinator.
        /// GET /_api/collection/{collection-name}/shards
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <returns></returns>
        Task<CollectionShardsResponse> GetCollectionShardsAsync(
           string collectionName);

        /// <summary>
        /// Returns the shard ids of a collection.
        /// This method is only available in a cluster Coordinator.
        /// The response also contains shard IDs as object attribute 
        /// keys, and the responsible servers for each shard mapped 
        /// to them. The leader shards will be first in the arrays.
        /// GET /_api/collection/{collection-name}/shards?details=true
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <returns></returns>
        Task<CollectionShardsDetailedResponse> GetCollectionShardsWithDetailsAsync(
           string collectionName);

        /// <summary>
        /// Compacts the data of a collection in order to reclaim disk space.
        /// The operation will compact the document and index data by rewriting
        /// the underlying .sst files and only keeping the relevant entries. 
        /// PUT /_api/collection/{collection-name}/compact
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <returns></returns>
        Task<CompactCollectionDataResponse> CompactCollectionDataAsync(
           string collectionName);
    }
}