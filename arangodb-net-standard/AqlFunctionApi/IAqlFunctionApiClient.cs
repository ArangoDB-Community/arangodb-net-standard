using ArangoDBNetStandard.AqlFunctionApi.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.AqlFunctionApi
{
    /// <summary>
    /// Defines a client to access the ArangoDB API for
    /// AQL User Functions Management.
    /// </summary>
    public interface IAqlFunctionApiClient
    {
        /// <summary>
        /// Create a new AQL user function.
        /// POST /_api/aqlfunction
        /// </summary>
        /// <param name="body">The body of the request containing required properties.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<PostAqlFunctionResponse> PostAqlFunctionAsync(PostAqlFunctionBody body,
            CancellationToken token = default);

        /// <summary>
        /// Removes an existing AQL user function or function group, identified by name.
        /// DELETE /_api/aqlfunction/{name}
        /// </summary>
        /// <param name="name">The name of the function or function group (namespace).</param>
        /// <param name="query">The query parameters of the request.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<DeleteAqlFunctionResponse> DeleteAqlFunctionAsync(
            string name,
            DeleteAqlFunctionQuery query = null,
            CancellationToken token = default);

        /// <summary>
        /// Get all registered AQL user functions.
        /// </summary>
        /// <param name="query">Query string options for the task.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<GetAqlFunctionsResponse> GetAqlFunctionsAsync(
            GetAqlFunctionsQuery query = null,
            CancellationToken token = default);

        /// <summary>
        /// Explain an AQL query and return information about it
        /// POST /_api/explain
        /// </summary>
        /// <param name="body">The body of the request containing required properties.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<PostExplainAqlQueryResponse> PostExplainAqlQueryAsync(
            PostExplainAqlQueryBody body,
            CancellationToken token = default);

        /// <summary>
        /// Parse an AQL query and return information about it
        /// POST /_api/query
        /// </summary>
        /// <param name="body">The body of the request containing required properties.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<PostParseAqlQueryResponse> PostParseAqlQueryAsync(
            PostParseAqlQueryBody body,
            CancellationToken token = default);

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
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <remarks>
        /// Kills a running query in the currently selected database. 
        /// The query will be terminated at the next cancelation point.
        /// Using the All parameter is only allowed in the system 
        /// database and with superuser privileges.
        /// </remarks>
        /// <returns></returns>
        Task<ResponseBase> DeleteKillRunningAqlQueryAsync(
            string queryId,
         DeleteKillRunningAqlQueryQuery query = null,
            CancellationToken token = default);

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
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<ResponseBase> DeleteClearSlowAqlQueriesAsync(
           DeleteClearSlowAqlQueriesQuery query = null,
            CancellationToken token = default);

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
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <remarks>
        /// Returns an array containing the last AQL queries that are 
        /// finished and have exceeded the slow query threshold in the 
        /// selected database. The maximum amount of queries in the list
        /// can be controlled by setting the query tracking property maxSlowQueries. 
        /// The threshold for treating a query as slow can be adjusted by 
        /// setting the query tracking property slowQueryThreshold.
        /// </remarks>
        /// <returns></returns>
        Task<List<SlowAqlQuery>> GetSlowAqlQueriesAsync(
           GetSlowAqlQueriesQuery query = null,
            CancellationToken token = default);

        /// <summary>
        /// Clears the query results cache for the current database
        /// DELETE /_api/query-cache
        /// </summary>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<ResponseBase> DeleteClearAqlQueryCacheAsync(
            CancellationToken token = default);

        /// <summary>
        /// Gets a list of the stored results in the AQL query results cache.
        /// GET /_api/query-cache/entries
        /// </summary>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <remarks>
        /// Returns an array containing the AQL query results currently 
        /// stored in the query results cache of the selected database.
        /// </remarks>
        /// <returns></returns>
        Task<List<CachedAqlQueryResult>> GetCachedAqlQueryResultsAsync(
            CancellationToken token = default);

        /// <summary>
        /// Gets the global configuration for the AQL query results cache.
        /// </summary>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <remarks>
        /// Returns the global AQL query results cache configuration.
        /// </remarks>
        /// <returns></returns>
        Task<QueryCacheGlobalProperties> GetQueryCacheGlobalPropertiesAsync(
            CancellationToken token = default);

        /// <summary>
        /// Changes the configuration for the AQL query results cache
        /// PUT /_api/query-cache/properties
        /// </summary>
        /// <param name="body">The body of the request containing required properties.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <remarks>
        /// After the properties have been changed, the current set of properties 
        /// will be returned in the HTTP response.
        /// Note: changing the properties may invalidate all results in the cache.
        /// </remarks>
        /// <returns></returns>
        Task<QueryCacheGlobalProperties> PutAdjustQueryCacheGlobalPropertiesAsync(
          PutAdjustQueryCacheGlobalPropertiesBody body,
            CancellationToken token = default);


        /// <summary>
        /// Gets the current query tracking configuration. 
        /// GET /_api/query/properties
        /// </summary>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<QueryTrackingConfiguration> GetQueryTrackingConfigurationAsync(
            CancellationToken token = default);

        /// <summary>
        /// Changes the configuration for the AQL query tracking.
        /// PUT /_api/query/properties
        /// </summary>
        /// <param name="body">The body of the request containing required configuration properties.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <remarks>
        /// After the configuration properties have been changed, 
        /// the current set of properties will be returned.
        /// </remarks>
        /// <returns></returns>
        Task<QueryTrackingConfiguration> PutChangeQueryTrackingConfigurationAsync(
          PutChangeQueryTrackingConfigurationBody body,
            CancellationToken token = default);

        /// <summary>
        /// Gets a list of currently running AQL queries.
        /// GET /_api/query/current
        /// </summary>
        /// <param name="query">Query string options for the task.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <remarks>
        /// Returns the global AQL query results cache configuration.
        /// </remarks>
        /// <returns></returns>
        Task<List<RunningAqlQuery>> GetCurrentlyRunningAqlQueriesAsync(
            GetCurrentlyRunningAqlQueriesQuery query = null,
            CancellationToken token = default);

        /// <summary>
        /// Gets the available optimizer rules for AQL queries
        /// GET /_api/query/rules
        /// </summary>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <remarks>
        /// Returns an array of objects that contain the name of each available 
        /// rule and its respective flags.
        /// </remarks>
        /// <returns></returns>
        Task<List<GetQueryRulesResponseItem>> GetQueryRulesAsync(
            CancellationToken token = default);
    }
}