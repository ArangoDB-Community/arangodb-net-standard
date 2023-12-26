using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ArangoDBNetStandard.CursorApi.Models;
using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;

namespace ArangoDBNetStandard.CursorApi
{
    /// <summary>
    /// ArangoDB Cursor API.
    /// </summary>
    public class CursorApiClient : ApiClientBase, ICursorApiClient
    {
        /// <summary>
        /// The root path of the API.
        /// </summary>
        protected readonly string _cursorApiPath = "_api/cursor";

        /// <summary>
        /// The transport client used to communicate with the ArangoDB host.
        /// </summary>
        protected IApiClientTransport _client;

        /// <summary>
        /// Creates an instance of <see cref="CursorApiClient"/>
        /// using the provided transport layer and the default JSON serialization.
        /// </summary>
        /// <param name="client"></param>
        public CursorApiClient(IApiClientTransport client)
            : base(new JsonNetApiClientSerialization())
        {
            _client = client;
        }

        /// <summary>
        /// Creates an instance of <see cref="CursorApiClient"/>
        /// using the provided transport and serialization layers.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="serializer"></param>
        public CursorApiClient(IApiClientTransport client, IApiClientSerialization serializer)
            : base(serializer)
        {
            _client = client;
        }

        /// <summary>
        /// Method to get the header collection.
        /// </summary>
        /// <param name="headerProperties">The <see cref="CursorHeaderProperties"/> values.</param>
        /// <returns><see cref="WebHeaderCollection"/> values.</returns>
        protected virtual WebHeaderCollection GetHeaderCollection(CursorHeaderProperties headerProperties)
        {
            return headerProperties?.ToWebHeaderCollection(); 
        }

        /// <summary>
        /// Execute an AQL query, creating a cursor which can be used to page query results.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="bindVars"></param>
        /// <param name="options"></param>
        /// <param name="count"></param>
        /// <param name="batchSize"></param>
        /// <param name="cache"></param>
        /// <param name="memoryLimit"></param>
        /// <param name="ttl"></param>
        /// <param name="transactionId">Optional. The stream transaction Id.</param>      
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<CursorResponse<T>> PostCursorAsync<T>(
                string query,
                Dictionary<string, object> bindVars = null,
                PostCursorOptions options = null,
                bool? count = null,
                long? batchSize = null,
                bool? cache = null,
                long? memoryLimit = null,
                int? ttl = null,
                string transactionId = null,
            CancellationToken token = default)
        {
            var headerProperties = new CursorHeaderProperties();
            if (!string.IsNullOrWhiteSpace(transactionId))
            {
                headerProperties.TransactionId = transactionId;
            }

            return await PostCursorAsync<T>(
                new PostCursorBody
                {
                    Query = query,
                    BindVars = bindVars,
                    Options = options,
                    Count = count,
                    BatchSize = batchSize,
                    Cache = cache,
                    MemoryLimit = memoryLimit,
                    Ttl = ttl
                },
                headerProperties,
                token).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute an AQL query, creating a cursor which can be used to page query results.
        /// </summary>
        /// <remarks>
        /// This method supports Read from Followers (dirty-reads) introduced in ArangoDB 3.10. 
        /// To enable it, set the <see cref="ApiHeaderProperties.AllowReadFromFollowers"/> header property to true.
        /// </remarks>
        /// <param name="postCursorBody">Object encapsulating options and parameters of the query.</param>
        /// <param name="headerProperties">Optional. Additional Header properties.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<CursorResponse<T>> PostCursorAsync<T>(
            PostCursorBody postCursorBody, 
            CursorHeaderProperties headerProperties = null,
            CancellationToken token = default)
        {
            var content = await GetContentAsync(postCursorBody, new ApiClientSerializationOptions(true, true)).ConfigureAwait(false);
            var headerCollection = GetHeaderCollection(headerProperties);
            using (var response = await _client.PostAsync(_cursorApiPath,
                content,
                headerCollection,
                token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<CursorResponse<T>>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Execute an AQL query and return basic statistics.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="bindVars"></param>
        /// <param name="options"></param>
        /// <param name="count"></param>
        /// <param name="batchSize"></param>
        /// <param name="cache"></param>
        /// <param name="memoryLimit"></param>
        /// <param name="ttl"></param>
        /// <param name="transactionId">Optional. The stream transaction Id.</param>      
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>

        public virtual async Task<CursorResponseBase> PostCursorAsync(
            string query,
            Dictionary<string, object> bindVars = null,
            PostCursorOptions options = null,
            bool? count = null,
            long? batchSize = null,
            bool? cache = null,
            long? memoryLimit = null,
            int? ttl = null,
            string transactionId = null,
            CancellationToken token = default)
        {
            var headerProperties = new CursorHeaderProperties();
            if (!string.IsNullOrWhiteSpace(transactionId))
            {
                headerProperties.TransactionId = transactionId;
            }

            var postCursorBody = new PostCursorBody
            {
                Query = query,
                BindVars = bindVars,
                Options = options,
                Count = count,
                BatchSize = batchSize,
                Cache = cache,
                MemoryLimit = memoryLimit,
                Ttl = ttl
            };

            var content = await GetContentAsync(postCursorBody, new ApiClientSerializationOptions(true, true)).ConfigureAwait(false);
            var headerCollection = GetHeaderCollection(headerProperties);
            using (var response = await _client.PostAsync(_cursorApiPath,
                content,
                headerCollection,
                token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<CursorResponseBase>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            };
        }

        /// <summary>
        /// Deletes an existing cursor and frees the resources associated with it.
        /// DELETE /_api/cursor/{cursor-identifier}
        /// </summary>
        /// <param name="cursorId">The id of the cursor to delete.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<DeleteCursorResponse> DeleteCursorAsync(string cursorId,
            CancellationToken token = default)
        {
            using (var response = await _client.DeleteAsync(
                _cursorApiPath + "/" + WebUtility.UrlEncode(cursorId),
                null,
                token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<DeleteCursorResponse>(stream).ConfigureAwait(false);
                }

                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Advances an existing query cursor and gets the next set of results.
        /// </summary>
        /// <typeparam name="T">Result type to deserialize to</typeparam>
        /// <param name="cursorId">ID of the existing query cursor.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        [Obsolete("Use PostAdvanceCursorAsync.")]
        public virtual async Task<PutCursorResponse<T>> PutCursorAsync<T>(string cursorId,
            CancellationToken token = default)
        {
            string uri = _cursorApiPath + "/" + WebUtility.UrlEncode(cursorId);
            using (var response = await _client.PutAsync(uri, new byte[0], null, token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<PutCursorResponse<T>>(stream).ConfigureAwait(false);
                }

                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Advances an existing query cursor and gets the next set of results.
        /// Replaces <see cref="PutCursorAsync{T}(string, CancellationToken)"/>
        /// </summary>
        /// <param name="cursorIdentifier">The name / identifier of the existing cursor.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<CursorResponse<T>> PostAdvanceCursorAsync<T>(string cursorIdentifier, CancellationToken token = default)
        {
            using (var response = await _client.PostAsync(
                requestUri: _cursorApiPath + $"/{WebUtility.UrlEncode(cursorIdentifier)}",
                content: new byte[] { },
                token: token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<CursorResponse<T>>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }
    }
}