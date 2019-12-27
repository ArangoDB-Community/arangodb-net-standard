using ArangoDBNetStandard.CollectionApi.Models;
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

        /// Read properties of a collection.
        /// GET /_api/collection/{collection-name}/properties
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<GetCollectionPropertiesResponse> GetCollectionPropertiesAsync(string collectionName);

        /// <summary>
        /// Rename a collection.
        /// PUT /_api/collection/{collection-name}/rename
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="request"></param>
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
        /// <param name="options"></param>
        /// <returns></returns>
        Task<GetCollectionFiguresResponse> GetCollectionFiguresAsync(string collectionName);
    }
}
