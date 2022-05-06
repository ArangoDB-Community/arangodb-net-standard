using System.Net;
using System.Threading.Tasks;

using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using ArangoDBNetStandard.IndexApi.Models;

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
        /// <returns></returns>
        public virtual async Task<GetIndexResponse> GetIndexAsync(string indexId)
        {
            string uri = _indexApiPath + '/' + indexId;
            using (var response = await _client.GetAsync(uri).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<GetIndexResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Delete an index permanently.
        /// </summary>
        /// <param name="indexId">The identifier of the index.</param>
        /// <returns></returns>
        public virtual async Task<DeleteIndexResponse> DeleteIndexAsync(string indexId)
        {
            string uri = _indexApiPath + "/" + indexId;
            using (var response = await _client.DeleteAsync(uri).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<DeleteIndexResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }


        /// <summary>
        /// Fetch the list of indexes for a collection.
        /// </summary>
        /// <param name="query">Query parameters for the request.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Required parameters not provided or invalid.</exception>
        public virtual async Task<GetAllCollectionIndexesResponse> GetAllCollectionIndexesAsync(GetAllCollectionIndexesQuery query)
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
            using (var response = await _client.GetAsync(uri).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<GetAllCollectionIndexesResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="System.ArgumentException">Required parameters not provided or invalid.</exception>
        public virtual async Task<IndexResponseBase> PostIndexAsync(PostIndexQuery query, PostIndexBody body)
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
            var content = GetContent(body, new ApiClientSerializationOptions(true, true));
            using (var response = await _client.PostAsync(uri, content).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<IndexResponseBase>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }
    }
}
