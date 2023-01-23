using System.Net;
using System.Threading.Tasks;

using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using ArangoDBNetStandard.IndexApi.Models;
using System.Threading;
using System;

namespace ArangoDBNetStandard.IndexApi
{
    /// <summary>
    /// A client for interacting with ArangoDB Index endpoints,
    /// implementing <see cref="IIndexApiClient"/>.
    /// </summary>
    public class IndexApiClient : ApiClientBase, IIndexApiClient
    {
        /// <summary>
        /// The transport client used to communicate with the ArangoDB host.
        /// </summary>
        protected IApiClientTransport _client;

        /// <summary>
        /// The root path of the API.
        /// </summary>
        protected readonly string _indexApiPath = "_api/index";

        /// <summary>
        /// Creates an instance of <see cref="IndexApiClient"/>
        /// using the provided transport layer and the default JSON serialization.
        /// </summary>
        /// <param name="client">Transport client that the API client will use to communicate with ArangoDB</param>
        public IndexApiClient(IApiClientTransport client)
            : base(new JsonNetApiClientSerialization())
        {
            _client = client;
        }

        /// <summary>
        /// Creates an instance of <see cref="IndexApiClient"/>
        /// using the provided transport and serialization layers.
        /// </summary>
        /// <param name="client">Transport client that the API client will use to communicate with ArangoDB.</param>
        /// <param name="serializer">Serializer to be used.</param>
        public IndexApiClient(IApiClientTransport client, IApiClientSerialization serializer)
            : base(serializer)
        {
            _client = client;
        }

        /// <summary>
        /// Fetches data about the specified index.
        /// </summary>
        /// <param name="indexId">The identifier of the index.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<GetIndexResponse> GetIndexAsync(string indexId,
            CancellationToken token = default)
        {
            string uri = _indexApiPath + '/' + indexId;
            using (var response = await _client.GetAsync(uri, token: token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<GetIndexResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Delete an index permanently.
        /// </summary>
        /// <param name="indexId">The identifier of the index.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<DeleteIndexResponse> DeleteIndexAsync(string indexId,
            CancellationToken token = default)
        {
            string uri = _indexApiPath + "/" + indexId;
            using (var response = await _client.DeleteAsync(uri, token: token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<DeleteIndexResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }


        /// <summary>
        /// Fetch the list of indexes for a collection.
        /// </summary>
        /// <param name="query">Query parameters for the request.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Required parameters not provided or invalid.</exception>
        public virtual async Task<GetAllCollectionIndexesResponse> GetAllCollectionIndexesAsync(GetAllCollectionIndexesQuery query,
            CancellationToken token = default)
        {
            string uri = _indexApiPath;

            if (query == null)
            {
                throw new System.ArgumentException("query is required", nameof(query));
            }

            if (string.IsNullOrEmpty(query.CollectionName))
            {
                throw new System.ArgumentException("Collection name is required", nameof(query.CollectionName));
            }

            uri += '?' + query.ToQueryString();
            using (var response = await _client.GetAsync(uri, token: token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<GetAllCollectionIndexesResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

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
        public virtual async Task<IndexResponseBase> PostIndexAsync(PostIndexQuery query, 
            PostIndexBody body,
            CancellationToken token = default)
        {
            string uri = _indexApiPath;

            if (query == null)
            {
                throw new System.ArgumentException("query is required", nameof(query));
            }

            if (string.IsNullOrEmpty(query.CollectionName))
            {
                throw new System.ArgumentException("Collection name is required", nameof(query.CollectionName));
            }

            if (body == null)
            {
                throw new System.ArgumentException("body is required", nameof(body));
            }

            uri += '?' + query.ToQueryString();
            var content = await GetContentAsync(body, new ApiClientSerializationOptions(true, true)).ConfigureAwait(false);
            using (var response = await _client.PostAsync(uri, content,token:token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<IndexResponseBase>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

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
        public virtual async Task<IndexResponseBase> PostFulltextIndexAsync(PostIndexQuery query,
            PostFulltextIndexBody body,
            CancellationToken token = default)
        {
            return await PostIndexAsync(query, body, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a new geo-spatial index
        /// </summary>
        /// <param name="query">Query parameters for the request.</param>
        /// <param name="body">The properties of the new index.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<IndexResponseBase> PostGeoSpatialIndexAsync(PostIndexQuery query,
            PostGeoSpatialIndexBody body,
            CancellationToken token = default)
        {
            return await PostIndexAsync(query, body, token).ConfigureAwait(false);
        }

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
        public virtual async Task<IndexResponseBase> PostHashIndexAsync(PostIndexQuery query,
            PostHashIndexBody body,
            CancellationToken token = default)
        {
            return await PostIndexAsync(query, body, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a new multi-dimensional index
        /// </summary>
        /// <param name="query">Query parameters for the request.</param>
        /// <param name="body">The properties of the new index.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<IndexResponseBase> PostMultiDimensionalIndexAsync(PostIndexQuery query,
            PostMultiDimensionalIndexBody body,
            CancellationToken token = default)
        {
            return await PostIndexAsync(query, body, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a new persistent index
        /// </summary>
        /// <param name="query">Query parameters for the request.</param>
        /// <param name="body">The properties of the new index.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<IndexResponseBase> PostPersistentIndexAsync(PostIndexQuery query,
            PostPersistentIndexBody body,
            CancellationToken token = default)
        {
            return await PostIndexAsync(query, body, token).ConfigureAwait(false);
        }

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
        public virtual async Task<IndexResponseBase> PostSkiplistIndexAsync(PostIndexQuery query,
            PostSkiplistIndexBody body,
            CancellationToken token = default)
        {
            return await PostIndexAsync(query, body, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a new TTL (time-to-live) index
        /// </summary>
        /// <param name="query">Query parameters for the request.</param>
        /// <param name="body">The properties of the new index.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<IndexResponseBase> PostTTLIndexAsync(PostIndexQuery query,
            PostTTLIndexBody body,
            CancellationToken token = default)
        {
            return await PostIndexAsync(query, body, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a new inverted index
        /// </summary>
        /// <param name="query">Query parameters for the request.</param>
        /// <param name="body">The properties of the new index.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<InvertedIndexResponse> PostInvertedIndexAsync(PostIndexQuery query, PostInvertedIndexBody body, CancellationToken token = default)
        {
            string uri = _indexApiPath;

            if (query == null)
            {
                throw new ArgumentException("query is required", nameof(query));
            }

            if (string.IsNullOrEmpty(query.CollectionName))
            {
                throw new ArgumentException("Collection name is required", nameof(query.CollectionName));
            }

            if (body == null)
            {
                throw new ArgumentException("body is required", nameof(body));
            }

            uri += '?' + query.ToQueryString();
            var content = GetContent(body, new ApiClientSerializationOptions(true, true));
            using (var response = await _client.PostAsync(uri, content, token: token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<InvertedIndexResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }
    }
}