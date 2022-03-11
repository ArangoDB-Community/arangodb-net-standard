using ArangoDBNetStandard.AqlFunctionApi.Models;
using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using System.Net;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.AqlFunctionApi
{
    /// <summary>
    /// A client to interact with ArangoDB HTTP API endpoints
    /// for AQL user functions management.
    /// </summary>
    public class AqlFunctionApiClient : ApiClientBase, IAqlFunctionApiClient
    {
        /// <summary>
        /// The transport client used to communicate with the ArangoDB host.
        /// </summary>
        protected readonly IApiClientTransport _transport;

        /// <summary>
        /// The root path of the API.
        /// </summary>
        protected readonly string _apiPath = "_api/aqlfunction";

        /// <summary>
        /// Create an instance of <see cref="AqlFunctionApiClient"/>
        /// using the provided transport layer and the default JSON serialization.
        /// </summary>
        /// <param name="transport"></param>
        public AqlFunctionApiClient(IApiClientTransport transport)
            : base(new JsonNetApiClientSerialization())
        {
            _transport = transport;
        }

        /// <summary>
        /// Create an instance of <see cref="AqlFunctionApiClient"/>
        /// using the provided transport and serialization layers.
        /// </summary>
        /// <param name="transport"></param>
        /// <param name="serializer"></param>
        public AqlFunctionApiClient(IApiClientTransport transport, IApiClientSerialization serializer)
            : base(serializer)
        {
            _transport = transport;
        }

        /// <summary>
        /// Create a new AQL user function.
        /// POST /_api/aqlfunction
        /// </summary>
        /// <param name="body">The body of the request containing required properties.</param>
        /// <returns></returns>
        public virtual async Task<PostAqlFunctionResponse> PostAqlFunctionAsync(PostAqlFunctionBody body)
        {
            var content = GetContent(body, new ApiClientSerializationOptions(true, true));

            using (var response = await _transport.PostAsync(_apiPath, content).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<PostAqlFunctionResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Removes an existing AQL user function or function group, identified by name.
        /// DELETE /_api/aqlfunction/{name}
        /// </summary>
        /// <param name="name">The name of the function or function group (namespace).</param>
        /// <param name="query">The query parameters of the request.</param>
        /// <returns></returns>
        public virtual async Task<DeleteAqlFunctionResponse> DeleteAqlFunctionAsync(
            string name,
            DeleteAqlFunctionQuery query = null)
        {
            string uri = _apiPath + '/' + WebUtility.UrlEncode(name);

            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }

            using (var response = await _transport.DeleteAsync(uri).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<DeleteAqlFunctionResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Get all registered AQL user functions.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<GetAqlFunctionsResponse> GetAqlFunctionsAsync(GetAqlFunctionsQuery query = null)
        {
            string uri = _apiPath;

            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }

            using (var response = await _transport.GetAsync(uri).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<GetAqlFunctionsResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Explain an AQL query and return information about it
        /// POST /_api/explain
        /// </summary>
        /// <param name="body">The body of the request containing required properties.</param>
        /// <returns></returns>
        public virtual async Task<PostExplainAqlQueryResponse> PostExplainAqlQueryAsync(PostExplainAqlQueryBody body)
        {
            if (body == null)
            {
                throw new System.ArgumentException("body is required", nameof(body));
            }

            string uri = "_api/explain";

            var content = GetContent(body, new ApiClientSerializationOptions(true, true));
            using (var response = await _transport.PostAsync(uri, content).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<PostExplainAqlQueryResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Parse an AQL query and return information about it
        /// POST /_api/query
        /// </summary>
        /// <param name="body">The body of the request containing required properties.</param>
        /// <returns></returns>
        public virtual async Task<PostParseAqlQueryResponse> PostParseAqlQueryAsync(PostParseAqlQueryBody body)
        {
            if (body == null)
            {
                throw new System.ArgumentException("body is required", nameof(body));
            }

            string uri = "_api/query";

            var content = GetContent(body, new ApiClientSerializationOptions(true, true));
            using (var response = await _transport.PostAsync(uri, content).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<PostParseAqlQueryResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Kills an AQL query in the currently selected database 
        /// or in all databases.
        /// DELETE /_api/query/{query-id}
        /// </summary>
        /// <param name="queryId">The id of the query to kill.</param>
        /// <param name="query">The query parameters of the request. 
        /// If All parameter is set to true, it will attempt to kill
        /// the specified query in all databases, not just the 
        /// selected one.
        /// </param>
        /// <remarks>
        /// Kills a running query in the currently selected database. 
        /// The query will be terminated at the next cancelation point.
        /// Using the All parameter is only allowed in the system 
        /// database and with superuser privileges.
        /// </remarks>
        /// <returns></returns>
        public virtual async Task<ResponseBase> DeleteKillRunningAqlQueryAsync(
            string queryId,
            DeleteKillRunningAqlQueryQuery query = null)
        {
            if (string.IsNullOrWhiteSpace(queryId))
            {
                throw new System.ArgumentException("queryId is required", nameof(queryId));
            }

            string uri = "_api/query/" + queryId;

            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }

            using (var response = await _transport.DeleteAsync(uri).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<ResponseBase>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Clears the list of slow AQL queries in the currently 
        /// selected database or in all databases.
        /// DELETE /_api/query/slow
        /// </summary>
        /// <param name="query">The query parameters of the request. 
        /// If All parameter is set to true, it will aclear the slow 
        /// query history in all databases, not just the selected one. 
        /// Using the parameter is only allowed in the system database
        /// and with superuser privileges.
        /// </param>
        /// <returns></returns>
        public virtual async Task<ResponseBase> DeleteClearSlowAqlQueriesAsync(DeleteClearSlowAqlQueriesQuery query = null)
        {
            string uri = "_api/query/slow";

            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }

            using (var response = await _transport.DeleteAsync(uri).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<ResponseBase>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Gets a list of slow running AQL queries in the currently 
        /// selected database or in all databases.
        /// GET /_api/query/slow
        /// </summary>
        /// <param name="query">The query parameters of the request. 
        /// If All parameter is set to true, it will return a list of
        /// slow running AQL queries in all databases, not just the selected one. 
        /// Using the parameter is only allowed in the system database
        /// and with superuser privileges.
        /// </param>
        /// <remarks>
        /// Returns an array containing the last AQL queries that are 
        /// finished and have exceeded the slow query threshold in the 
        /// selected database. The maximum amount of queries in the list
        /// can be controlled by setting the query tracking property maxSlowQueries. 
        /// The threshold for treating a query as slow can be adjusted by 
        /// setting the query tracking property slowQueryThreshold.
        /// </remarks>
        /// <returns></returns>
        public virtual async Task<GetSlowAqlQueriesResponse> GetSlowAqlQueriesAsync(GetSlowAqlQueriesQuery query = null)
        {
            string uri = "_api/query/slow";

            if (query != null)
            {
                uri += "?" + query.ToQueryString();
            }

            using (var response = await _transport.GetAsync(uri).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<GetSlowAqlQueriesResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }



        /// <summary>
        /// Clears the query results cache for the current database
        /// DELETE /_api/query-cache
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ResponseBase> DeleteClearAqlQueryCacheAsync()
        {
            string uri = "_api/query-cache";

            using (var response = await _transport.DeleteAsync(uri).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<ResponseBase>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Gets a list of the stored results in the AQL query results cache.
        /// GET /_api/query-cache/entries
        /// </summary>
        /// <remarks>
        /// Returns an array containing the AQL query results currently 
        /// stored in the query results cache of the selected database.
        /// </remarks>
        /// <returns></returns>
        public virtual async Task<GetCachedAqlQueryResultsResponse> GetCachedAqlQueryResultsAsync()
        {
            string uri = "_api/query-cache/entries";
            using (var response = await _transport.GetAsync(uri).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<GetCachedAqlQueryResultsResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Gets the global configuration for the AQL query results cache.
        /// </summary>
        /// <remarks>
        /// Returns the global AQL query results cache configuration.
        /// </remarks>
        /// <returns></returns>
        public virtual async Task<QueryCacheGlobalProperties> GetQueryCacheGlobalPropertiesAsync()
        {
            string uri = "_api/query-cache/properties";
            using (var response = await _transport.GetAsync(uri).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<QueryCacheGlobalProperties>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Changes the configuration for the AQL query results cache
        /// PUT /_api/query-cache/properties
        /// </summary>
        /// <remarks>
        /// After the properties have been changed, the current set of properties 
        /// will be returned in the HTTP response.
        /// Note: changing the properties may invalidate all results in the cache.
        /// </remarks>
        /// <param name="body">The body of the request containing required properties.</param>
        /// <returns></returns>
        public virtual async Task<QueryCacheGlobalProperties> PutAdjustQueryCacheGlobalPropertiesAsync(PutAdjustQueryCacheGlobalPropertiesBody body)
        {
            if (body == null)
            {
                throw new System.ArgumentException("body is required", nameof(body));
            }

            string uri = "_api/query-cache/properties";

            var content = GetContent(body, new ApiClientSerializationOptions(true, true));
            using (var response = await _transport.PutAsync(uri, content).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return DeserializeJsonFromStream<QueryCacheGlobalProperties>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }


    }
}
