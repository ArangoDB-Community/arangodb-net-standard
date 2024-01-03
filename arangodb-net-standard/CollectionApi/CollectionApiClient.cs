using ArangoDBNetStandard.CollectionApi.Models;
using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
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

        /// <summary>
        /// Creates a new collection
        /// </summary>
        /// <param name="body">Attributes of the new collection</param>
        /// <param name="options">Query string options for the task.</param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<PostCollectionResponse> PostCollectionAsync(
            PostCollectionBody body,
            PostCollectionQuery options = null,
            ApiHeaderProperties headers = null,
            CancellationToken token = default)
        {
            string uriString = _collectionApiPath;
            if (options != null)
            {
                uriString += "?" + options.ToQueryString();
            }
            var content = await GetContentAsync(body, new ApiClientSerializationOptions(true, true)).ConfigureAwait(false);
            using (var response = await _transport.PostAsync(uriString, content, webHeaderCollection: headers?.ToWebHeaderCollection(), token).ConfigureAwait(false))
            {
                var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return await DeserializeJsonFromStreamAsync<PostCollectionResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Deletes a collection.
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<DeleteCollectionResponse> DeleteCollectionAsync(string collectionName,
            ApiHeaderProperties headers = null,
            CancellationToken token = default)
        {
            using (var response = await _transport.DeleteAsync(
                _collectionApiPath + "/" + WebUtility.UrlEncode(collectionName),
                webHeaderCollection: headers?.ToWebHeaderCollection(),
                token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<DeleteCollectionResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Truncates a collection, i.e. removes all documents in the collection.
        /// PUT/_api/collection/{collection-name}/truncate
        /// </summary>
        /// <param name="collectionName">Name of the collection</param>
        /// <param name="headers">Headers (such as transaction id) to use for this operation.</param>        
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<TruncateCollectionResponse> TruncateCollectionAsync(string collectionName, 
                      CollectionHeaderProperties headers = null, 
                      CancellationToken token = default)
        {
            using (var response = await _transport.PutAsync(
                _collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/truncate",
                new byte[0], 
                headers?.ToWebHeaderCollection(),
                token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<TruncateCollectionResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Gets count of documents in a collection.
        /// GET/_api/collection/{collection-name}/count
        /// </summary>
        /// <param name="collectionName">Name of the collection</param>
        /// <param name="headers">Headers (such as transaction id) to use for this operation.</param>        
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<GetCollectionCountResponse> GetCollectionCountAsync(string collectionName, 
                CollectionHeaderProperties headers = null, 
                CancellationToken token = default)
        {
            using (var response = await _transport.GetAsync(
                _collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/count", 
                headers?.ToWebHeaderCollection(),
                token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<GetCollectionCountResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            };
        }

        /// <summary>
        /// Get all collections.
        /// GET/_api/collection
        /// </summary>
        /// <param name="query"></param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<GetCollectionsResponse> GetCollectionsAsync(GetCollectionsQuery query = null,
            ApiHeaderProperties headers = null,
            CancellationToken token = default)
        {
            string uriString = _collectionApiPath;
            if (query != null)
            {
                uriString += "?" + query.ToQueryString();
            }
            using (var response = await _transport.GetAsync(uriString, webHeaderCollection: headers?.ToWebHeaderCollection(), token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<GetCollectionsResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Gets the requested collection.
        /// GET/_api/collection/{collection-name}
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public async Task<GetCollectionResponse> GetCollectionAsync(string collectionName,
            ApiHeaderProperties headers = null,
            CancellationToken token = default)
        {
            using (var response = await _transport.GetAsync(
                _collectionApiPath + "/" + WebUtility.UrlEncode(collectionName),
                webHeaderCollection: headers?.ToWebHeaderCollection(),
                token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    var collection = await DeserializeJsonFromStreamAsync<GetCollectionResponse>(stream).ConfigureAwait(false);
                    return collection;
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Read properties of a collection.
        /// GET /_api/collection/{collection-name}/properties
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<GetCollectionPropertiesResponse> GetCollectionPropertiesAsync(string collectionName,
            ApiHeaderProperties headers = null,
            CancellationToken token = default)
        {
            using (var response = await _transport.GetAsync(
                _collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/properties", 
                webHeaderCollection: headers?.ToWebHeaderCollection(),
                token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<GetCollectionPropertiesResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Rename a collection.
        /// PUT /_api/collection/{collection-name}/rename
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="body"></param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<RenameCollectionResponse> RenameCollectionAsync(string collectionName, RenameCollectionBody body,
            ApiHeaderProperties headers = null,
            CancellationToken token = default)
        {
            var content = await GetContentAsync(body, new ApiClientSerializationOptions(true, false)).ConfigureAwait(false);
            using (var response = await _transport.PutAsync(
                _collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/rename", 
                content,
                webHeaderCollection: headers?.ToWebHeaderCollection(),
                token).ConfigureAwait(false))
           {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    var collection =await DeserializeJsonFromStreamAsync<RenameCollectionResponse>(stream).ConfigureAwait(false);
                    return collection;
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Get a revision of the collection. 
        /// GET /_api/collection/{collection-name}/revision
        /// </summary>
        /// <param name="collectionName">Name of the collection</param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<GetCollectionRevisionResponse> GetCollectionRevisionAsync(string collectionName,
            ApiHeaderProperties headers = null,
            CancellationToken token = default)
        {
            using (var response = await _transport.GetAsync(
                _collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/revision",
                webHeaderCollection: headers?.ToWebHeaderCollection(),
                token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<GetCollectionRevisionResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Changes the properties of a collection
        /// PUT /_api/collection/{collection-name}/properties
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="body"></param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<PutCollectionPropertyResponse> PutCollectionPropertyAsync(
            string collectionName,
            PutCollectionPropertyBody body,
            ApiHeaderProperties headers = null,
            CancellationToken token = default)
        {
            var content = await GetContentAsync(body, new ApiClientSerializationOptions(true, true)).ConfigureAwait(false);
            using (var response = await _transport.PutAsync(
                _collectionApiPath + "/" + collectionName + "/properties", 
                content,
                webHeaderCollection: headers?.ToWebHeaderCollection(),
                token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<PutCollectionPropertyResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Gets the number of documents and additional statistical information about the collection.
        /// GET/_api/collection/{collection-name}/figures
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<GetCollectionFiguresResponse> GetCollectionFiguresAsync(string collectionName,
            ApiHeaderProperties headers = null,
            CancellationToken token = default)
        {
            using (var response = await _transport.GetAsync(
                _collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/figures",
                webHeaderCollection: headers?.ToWebHeaderCollection(),
                token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<GetCollectionFiguresResponse>(stream).ConfigureAwait(false);
                }

                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            };
        }

        /// <summary>
        /// Get the checksum for a specific collection.
        /// GET /_api/collection/{collection-name}/checksum
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="query">Query options.</param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<GetChecksumResponse> GetChecksumAsync(
            string collectionName,
            GetChecksumQuery query = null,
            ApiHeaderProperties headers = null,
            CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
            {
                throw new ArgumentException($"{nameof(collectionName)} is required", nameof(collectionName));
            }
            string uriString = _collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/checksum";
            if (query != null)
            {
                uriString += "?" + query.ToQueryString();
            }
            using (var response = await _transport.GetAsync(uriString, webHeaderCollection: headers?.ToWebHeaderCollection(), token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<GetChecksumResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
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
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<PutLoadIndexesIntoMemoryResponse> PutLoadIndexesIntoMemoryAsync(string collectionName,
            ApiHeaderProperties headers = null,
            CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
            {
                throw new ArgumentException($"{nameof(collectionName)} is required", nameof(collectionName));
            }
            string uriString = _collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/loadIndexesIntoMemory";
            using (var response = await _transport.PutAsync(uriString, new byte[] { }, webHeaderCollection: headers?.ToWebHeaderCollection(), token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<PutLoadIndexesIntoMemoryResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Recalculates the document count of a collection.        
        /// PUT /_api/collection/{collection-name}/recalculateCount
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<PutRecalculateCountResponse> PutRecalculateCountAsync(string collectionName,
            ApiHeaderProperties headers = null,
            CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
            {
                throw new ArgumentException($"{nameof(collectionName)} is required", nameof(collectionName));
            }
            string uriString = _collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/recalculateCount";
            using (var response = await _transport.PutAsync(uriString, new byte[] { }, webHeaderCollection: headers?.ToWebHeaderCollection(), token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<PutRecalculateCountResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
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
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<PutDocumentShardResponse> PutDocumentShardAsync(
            string collectionName, 
            Dictionary<string, object> body,
            ApiHeaderProperties headers = null,
            CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
            {
                throw new ArgumentException($"{nameof(collectionName)} is required", nameof(collectionName));
            }
            if (body == null || body.Count < 1)
            {
                throw new ArgumentException($"{nameof(body)} is required", nameof(body));
            }
            string uriString = _collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/responsibleShard";
            var content = await GetContentAsync(body, new ApiClientSerializationOptions(true, true)).ConfigureAwait(false);
            using (var response = await _transport.PutAsync(uriString, content, webHeaderCollection: headers?.ToWebHeaderCollection(), token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<PutDocumentShardResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Returns the shard ids of a collection.
        /// This method is only available in a cluster Coordinator.
        /// GET /_api/collection/{collection-name}/shards
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<GetCollectionShardsResponse> GetCollectionShardsAsync(string collectionName,
            ApiHeaderProperties headers = null,
            CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
            {
                throw new ArgumentException($"{nameof(collectionName)} is required", nameof(collectionName));
            }
            string uriString = _collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/shards";
            using (var response = await _transport.GetAsync(uriString, webHeaderCollection: headers?.ToWebHeaderCollection(), token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<GetCollectionShardsResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
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
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<GetCollectionShardsDetailedResponse> GetCollectionShardsWithDetailsAsync(string collectionName,
            ApiHeaderProperties headers = null,
            CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
            {
                throw new ArgumentException($"{nameof(collectionName)} is required", nameof(collectionName));
            }
            string uriString = _collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/shards?details=true";
            using (var response = await _transport.GetAsync(uriString, webHeaderCollection: headers?.ToWebHeaderCollection(), token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<GetCollectionShardsDetailedResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Compacts the data of a collection in order to reclaim disk space.
        /// The operation will compact the document and index data by rewriting
        /// the underlying .sst files and only keeping the relevant entries. 
        /// PUT /_api/collection/{collection-name}/compact
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<PutCompactCollectionDataResponse> PutCompactCollectionDataAsync(string collectionName, ApiHeaderProperties headers = null,
            CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
            {
                throw new ArgumentException($"{nameof(collectionName)} is required", nameof(collectionName));
            }
            string uriString = _collectionApiPath + "/" + WebUtility.UrlEncode(collectionName) + "/compact";
            using (var response = await _transport.PutAsync(uriString, new byte[] { }, webHeaderCollection: headers?.ToWebHeaderCollection(), token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<PutCompactCollectionDataResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }
    }
}