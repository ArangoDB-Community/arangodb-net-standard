using ArangoDBNetStandard.IndexApi.Models;
using System.Threading;
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
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<GetIndexResponse> GetIndexAsync(string indexId,
            CancellationToken token = default);

        /// <summary>
        /// Delete an index permanently.
        /// </summary>
        /// <param name="indexId">The index identifier.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<DeleteIndexResponse> DeleteIndexAsync(string indexId,
            CancellationToken token = default);

        /// <summary>
        /// Fetch the list of indexes for a collection.
        /// </summary>
        /// <param name="query">Query parameters for the request.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<GetAllCollectionIndexesResponse> GetAllCollectionIndexesAsync(GetAllCollectionIndexesQuery query,
            CancellationToken token = default);

        /// <summary>
        /// Creates a new index
        /// </summary>
        /// <param name="query">Query parameters for the request.</param>
        /// <param name="body">The properties of the new index.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<IndexResponseBase> PostIndexAsync(
            PostIndexQuery query, 
            PostIndexBody body,
            CancellationToken token = default);
    }
}