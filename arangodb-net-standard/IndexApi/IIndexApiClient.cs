
using ArangoDBNetStandard.IndexApi.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.IndexApi
{
    /// <summary>
    /// Defines a client to access the ArangoDB Indexes API.
    /// </summary>
    public interface IIndexApiClient
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
        /// Generic method to create an index.
        /// It is highly recommended that you use a specialized method like
        /// <see cref="PostGeoSpatialIndexAsync(PostIndexQuery, PostGeoSpatialIndexBody, CancellationToken)"/>
        /// to create indexes. Use this method to create indexes that do not
        /// have a specialized method available.
        /// </summary>
        /// <param name="query">Query parameters for the request.</param>
        /// <param name="body">The properties of the new index.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>       
        Task<IndexResponseBase> PostIndexAsync(PostIndexQuery query,
            PostIndexBody body,
            CancellationToken token = default);

        /// <summary>
        /// Creates a new fulltext index.
        /// Deprecated in v3.10.
        /// </summary>
        /// <param name="query">Query parameters for the request.</param>
        /// <param name="body">The properties of the new index.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        /// <remarks>
        /// Deprecated in v3.10.
        /// </remarks>
        [Obsolete("Use ArangoSearch for advanced full-text search capabilities.")]
        Task<IndexResponseBase> PostFulltextIndexAsync(PostIndexQuery query,
            PostFulltextIndexBody body,
            CancellationToken token = default);

        /// <summary>
        /// Creates a new geo-spatial index
        /// </summary>
        /// <param name="query">Query parameters for the request.</param>
        /// <param name="body">The properties of the new index.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<IndexResponseBase> PostGeoSpatialIndexAsync(PostIndexQuery query,
            PostGeoSpatialIndexBody body,
            CancellationToken token = default);

        /// <summary>
        /// Creates a new hash index.
        /// Deprecated in v3.9.
        /// </summary>
        /// <param name="query">Query parameters for the request.</param>
        /// <param name="body">The properties of the new index.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        /// <remarks>
        /// Deprecated in v3.9.
        /// </remarks>
        [Obsolete("A hash index is now an alias for a persistent index. Use PostPersistentIndexAsync().")]
        Task<IndexResponseBase> PostHashIndexAsync(PostIndexQuery query,
            PostHashIndexBody body,
            CancellationToken token = default);

        /// <summary>
        /// Creates a new multi-dimensional index
        /// </summary>
        /// <param name="query">Query parameters for the request.</param>
        /// <param name="body">The properties of the new index.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<IndexResponseBase> PostMultiDimensionalIndexAsync(PostIndexQuery query,
            PostMultiDimensionalIndexBody body,
            CancellationToken token = default);

        /// <summary>
        /// Creates a new persistent index
        /// </summary>
        /// <param name="query">Query parameters for the request.</param>
        /// <param name="body">The properties of the new index.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<IndexResponseBase> PostPersistentIndexAsync(PostIndexQuery query,
            PostPersistentIndexBody body,
            CancellationToken token = default);

        /// <summary>
        /// Creates a new skiplist index.
        /// Deprecated in v3.9.
        /// </summary>
        /// <param name="query">Query parameters for the request.</param>
        /// <param name="body">The properties of the new index.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        /// <remarks>
        /// Deprecated in v3.9.
        /// </remarks>
        [Obsolete("A hash index is now an alias for a persistent index. Use PostPersistentIndexAsync().")]
        Task<IndexResponseBase> PostSkiplistIndexAsync(PostIndexQuery query,
            PostSkiplistIndexBody body,
            CancellationToken token = default);

        /// <summary>
        /// Creates a new TTL (time-to-live) index
        /// </summary>
        /// <param name="query">Query parameters for the request.</param>
        /// <param name="body">The properties of the new index.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<IndexResponseBase> PostTTLIndexAsync(PostIndexQuery query,
            PostTTLIndexBody body,
            CancellationToken token = default);

        /// <summary>
        /// Creates a new inverted index
        /// </summary>
        /// <param name="query">Query parameters for the request.</param>
        /// <param name="body">The properties of the new index.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<IndexResponseBase> PostInvertedIndexAsync(PostIndexQuery query,
            PostInvertedIndexBody body,
            CancellationToken token = default);
    }
}