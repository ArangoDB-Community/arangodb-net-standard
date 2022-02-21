using ArangoDBNetStandard.IndexApi.Models;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.IndexApi
{
    /// <summary>
    /// Defines a client to access the ArangoDB Indexes API.
    /// </summary>
    internal interface IIndexApiClient
    {
        /// <summary>
        /// Fetches data about the specified index.
        /// </summary>
        /// <param name="indexId">The index identifier.</param>
        /// <returns></returns>
        Task<GetIndexResponse> GetIndexAsync(string indexId);

        /// <summary>
        /// Delete an index permanently.
        /// </summary>
        /// <param name="indexId">The index identifier.</param>
        /// <returns></returns>
        Task<DeleteIndexResponse> DeleteIndexAsync(string indexId);

        /// <summary>
        /// Fetch the list of indexes for a collection.
        /// </summary>
        /// <param name="query">Query parameters for the request.</param>
        /// <returns></returns>
        Task<GetAllCollectionIndexesResponse> GetAllCollectionIndexesAsync(GetAllCollectionIndexesQuery query);

        /// <summary>
        /// Creates a new index
        /// </summary>
        /// <param name="indexType">The type of index to create.</param>
        /// <param name="query">Query parameters for the request.</param>
        /// <param name="body">The properties of the new index.</param>
        /// <returns></returns>
        Task<IndexResponseBase> CreateIndexAsync(IndexType indexType, CreateIndexQuery query, CreateIndexBody body);
    }
}
