using ArangoDBNetStandard.CollectionApi.Models;
using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using System.Collections.Generic;
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
            using (var response = await _transport.PostAsync(uriString, content).ConfigureAwait(false))
            {
                var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return DeserializeJsonFromStream<PostCollectionResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        public virtual async Task<DeleteCollectionResponse> DeleteCollectionAsync(string collectionName)
        {
            using (var response = await _transport.DeleteAsync(_collectionApiPath + "/" + WebUtility.UrlEncode(collectionName)).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<DeleteCollectionResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
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
                new byte[0]).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<TruncateCollectionResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
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
            using (var response = await _transport.GetAsync(_collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/count").ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<GetCollectionCountResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
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
            using (var response = await _transport.GetAsync(uriString).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<GetCollectionsResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
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
            using (var response = await _transport.GetAsync(_collectionApiPath + "/" + WebUtility.UrlEncode(collectionName)).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    var collection = DeserializeJsonFromStream<GetCollectionResponse>(stream);
                    return collection;
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
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
            using (var response = await _transport.GetAsync(_collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/properties").ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<GetCollectionPropertiesResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
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
            using (var response = await _transport.PutAsync(_collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/rename", content).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    var collection = DeserializeJsonFromStream<RenameCollectionResponse>(stream);
                    return collection;
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
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
            using (var response = await _transport.GetAsync(_collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/revision").ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<GetCollectionRevisionResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
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
            using (var response = await _transport.PutAsync(_collectionApiPath + "/" + collectionName + "/properties", content).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<PutCollectionPropertyResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
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
            using (var response = await _transport.GetAsync(_collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/figures").ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<GetCollectionFiguresResponse>(stream);
                }

                throw await GetApiErrorException(response).ConfigureAwait(false);
            };
        }













        /// <summary>
        /// Get the checksum for a specific collection.
        /// GET /_api/collection/{collection-name}/checksum
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="query">Query options.</param>
        /// <returns></returns>
        public virtual async Task<GetChecksumResponse> GetChecksumAsync(string collectionName, GetChecksumQuery query = null)
        {
            throw new System.NotImplementedException();
        }

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
        public virtual async Task<LoadIndexesIntoMemoryResponse> LoadIndexesIntoMemoryAsync(string collectionName)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Recalculates the document count of a collection.        
        /// PUT /_api/collection/{collection-name}/recalculateCount
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <returns></returns>
        public virtual async Task<RecalculateCountResponse> RecalculateCountAsync(string collectionName)
        {
            throw new System.NotImplementedException();
        }

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
        public virtual async Task<DocumentShardResponse> PutDocumentShardAsync(string collectionName, Dictionary<string, object> body)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns the shard ids of a collection.
        /// This method is only available in a cluster Coordinator.
        /// GET /_api/collection/{collection-name}/shards
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <returns></returns>
        public virtual async Task<CollectionShardsResponse> GetCollectionShardsAsync(string collectionName)
        {
            throw new System.NotImplementedException();
        }

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
        public virtual async Task<CollectionShardsDetailedResponse> GetCollectionShardsWithDetailsAsync(string collectionName)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Compacts the data of a collection in order to reclaim disk space.
        /// The operation will compact the document and index data by rewriting
        /// the underlying .sst files and only keeping the relevant entries. 
        /// PUT /_api/collection/{collection-name}/compact
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <returns></returns>
        public virtual async Task<CompactCollectionDataResponse> CompactCollectionDataAsync(string collectionName)
        {
            throw new System.NotImplementedException();
        }
    }
}
