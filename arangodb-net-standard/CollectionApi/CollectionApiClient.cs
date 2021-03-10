using ArangoDBNetStandard.CollectionApi.Models;
using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using System.Net;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.CollectionApi
{
    /// <summary>
    /// A client for interacting with ArangoDB Collections endpoints,
    /// implementing <see cref="ICollectionApiClient"/>.
    /// </summary>
    public class CollectionApiClient : ApiClientBase, ICollectionApiClient
    {
        /// <summary>
        /// The transport client used to communicate with the ArangoDB host.
        /// </summary>
        protected IApiClientTransport _transport;

        /// <summary>
        /// The root path of the API.
        /// </summary>
        protected string _collectionApiPath = "_api/collection";

        /// <summary>
        /// Creates an instance of <see cref="CollectionApiClient"/>
        /// using the provided transport layer and the default JSON serialization.
        /// </summary>
        /// <param name="transport"></param>
        public CollectionApiClient(IApiClientTransport transport)
            : base(new JsonNetApiClientSerialization())
        {
            _transport = transport;
        }

        /// <summary>
        /// Creates an instance of <see cref="CollectionApiClient"/>
        /// using the provided transport and serialization layers.
        /// </summary>
        /// <param name="transport"></param>
        /// <param name="serializer"></param>
        public CollectionApiClient(IApiClientTransport transport, IApiClientSerialization serializer)
            : base(serializer)
        {
            _transport = transport;
        }

        public virtual async Task<PostCollectionResponse> PostCollectionAsync(
            PostCollectionBody body,
            PostCollectionQuery options = null)
        {
            string uriString = _collectionApiPath;
            if (options != null)
            {
                uriString += "?" + options.ToQueryString();
            }
            var content = GetContent(body, new ApiClientSerializationOptions(true, true));
            using (var response = await _transport.PostAsync(uriString, content))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                if (response.IsSuccessStatusCode)
                {
                    return DeserializeJsonFromStream<PostCollectionResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        public virtual async Task<DeleteCollectionResponse> DeleteCollectionAsync(string collectionName)
        {
            using (var response = await _transport.DeleteAsync(_collectionApiPath + "/" + WebUtility.UrlEncode(collectionName)))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<DeleteCollectionResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Truncates a collection, i.e. removes all documents in the collection.
        /// PUT/_api/collection/{collection-name}/truncate
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public virtual async Task<TruncateCollectionResponse> TruncateCollectionAsync(string collectionName)
        {
            using (var response = await _transport.PutAsync(
                _collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/truncate",
                new byte[0]))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<TruncateCollectionResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Gets count of documents in a collection.
        /// GET/_api/collection/{collection-name}/count
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public virtual async Task<GetCollectionCountResponse> GetCollectionCountAsync(string collectionName)
        {
            using (var response = await _transport.GetAsync(_collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/count"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<GetCollectionCountResponse>(stream);
                }
                throw await GetApiErrorException(response);
            };
        }

        /// <summary>
        /// Get all collections.
        /// GET/_api/collection
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual async Task<GetCollectionsResponse> GetCollectionsAsync(GetCollectionsQuery query = null)
        {
            string uriString = _collectionApiPath;
            if (query != null)
            {
                uriString += "?" + query.ToQueryString();
            }
            using (var response = await _transport.GetAsync(uriString))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<GetCollectionsResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Return information about the requested collection.
        /// GET/_api/collection/{collection-name}
        /// </summary>
        /// <param name="collectionName">The name of the collection.</param>
        /// <returns>
        /// The result is an object describing the collection.
        /// </returns>
        public async Task<GetCollectionResponse> GetCollectionAsync(string collectionName)
        {
            using (var response = await _transport.GetAsync(_collectionApiPath + "/" + WebUtility.UrlEncode(collectionName)))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var collection = DeserializeJsonFromStream<GetCollectionResponse>(stream);
                    return collection;
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Read properties of a collection.
        /// GET /_api/collection/{collection-name}/properties
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public virtual async Task<GetCollectionPropertiesResponse> GetCollectionPropertiesAsync(string collectionName)
        {
            using (var response = await _transport.GetAsync(_collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/properties"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<GetCollectionPropertiesResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Rename a collection.
        /// PUT /_api/collection/{collection-name}/rename
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public virtual async Task<RenameCollectionResponse> RenameCollectionAsync(string collectionName, RenameCollectionBody body)
        {
            var content = GetContent(body, new ApiClientSerializationOptions(true, false));
            using (var response = await _transport.PutAsync(_collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/rename", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var collection = DeserializeJsonFromStream<RenameCollectionResponse>(stream);
                    return collection;
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Get a revision of the collection. 
        /// GET /_api/collection/{collection-name}/revision
        /// </summary>
        /// <param name="collectionName">Name of the collection</param>
        /// <returns></returns>
        public virtual async Task<GetCollectionRevisionResponse> GetCollectionRevisionAsync(string collectionName)
        {
            using (var response = await _transport.GetAsync(_collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/revision"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<GetCollectionRevisionResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Changes the properties of a collection
        /// PUT /_api/collection/{collection-name}/properties
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public virtual async Task<PutCollectionPropertyResponse> PutCollectionPropertyAsync(
            string collectionName,
            PutCollectionPropertyBody body)
        {
            var content = GetContent(body, new ApiClientSerializationOptions(true, true));
            using (var response = await _transport.PutAsync(_collectionApiPath + "/" + collectionName + "/properties", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PutCollectionPropertyResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Contains the number of documents and additional statistical information about the collection.
        /// GET/_api/collection/{collection-name}/figures
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public virtual async Task<GetCollectionFiguresResponse> GetCollectionFiguresAsync(string collectionName)
        {
            using (var response = await _transport.GetAsync(_collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/figures"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<GetCollectionFiguresResponse>(stream);
                }

                throw await GetApiErrorException(response);
            };
        }
    }
}
