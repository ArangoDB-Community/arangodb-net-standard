﻿using System.Net;
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
        /// <param name="client"></param>
        public IndexApiClient(IApiClientTransport client)
            : base(new JsonNetApiClientSerialization())
        {
            _client = client;
        }

        /// <summary>
        /// Creates an instance of <see cref="IndexApiClient"/>
        /// using the provided transport and serialization layers.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="serializer"></param>
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
            string uri = _indexApiPath + '/' + WebUtility.UrlEncode(indexId);
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
            string uri = _indexApiPath + "/" +indexId;
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
        public virtual async Task<GetAllCollectionIndexesResponse> GetAllCollectionIndexesAsync(GetAllCollectionIndexesQuery query)
        {
            string uri = _indexApiPath;
            if (query == null)
                throw new System.Exception("query is required");
            if (string.IsNullOrEmpty(query.CollectionName))
                throw new System.Exception("Collection name is required");

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

        /// <summary>
        /// Creates an index
        /// </summary>
        /// <param name="indexType">Type of index to create</param>
        /// <param name="query">Querystring details</param>
        /// <param name="body">CreateIndexBody object containing the index details</param>
        /// <returns></returns>
        public virtual async Task<IndexResponseBase> PostIndexAsync(IndexType indexType, PostIndexQuery query, PostIndexBody body)
        {
            string uri = _indexApiPath;
            if (query == null)
                throw new System.Exception("query is required");
            if (string.IsNullOrEmpty(query.CollectionName))
                throw new System.Exception("Collection name is required");
            if (body == null)
                throw new System.Exception("body is required");
            switch (indexType)
            {
                case IndexType.Persistent:
                    {
                        //uri += "#persistent";
                        body.Type = "persistent";
                    }
                    break;
                case IndexType.FullText:
                    {
                        //uri += "#fulltext";
                        body.Type = "fulltext";
                    }
                    break;
                case IndexType.TTL:
                    {
                        //uri += "#ttl";
                        body.Type = "ttl";
                    }
                    break;
                case IndexType.Geo:
                    {
                        //uri += "#geo";
                        body.Type = "geo";
                    }
                    break;
                default:
                    throw new System.Exception("Invalid index type");
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