﻿using ArangoDBNetStandard.AqlFunctionApi.Models;
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
        /// <returns></returns>
        Task<PostAqlFunctionResponse> PostAqlFunctionAsync(PostAqlFunctionBody body);

        /// <summary>
        /// Removes an existing AQL user function or function group, identified by name.
        /// DELETE /_api/aqlfunction/{name}
        /// </summary>
        /// <param name="name">The name of the function or function group (namespace).</param>
        /// <param name="query">The query parameters of the request.</param>
        /// <returns></returns>
        Task<DeleteAqlFunctionResponse> DeleteAqlFunctionAsync(
            string name,
            DeleteAqlFunctionQuery query = null);

        /// <summary>
        /// Get all registered AQL user functions.
        /// </summary>
        /// <returns></returns>
        Task<GetAqlFunctionsResponse> GetAqlFunctionsAsync(
            GetAqlFunctionsQuery query = null);



        /// <summary>
        /// Explain an AQL query and return information about it
        /// POST /_api/explain
        /// </summary>
        /// <param name="body">The body of the request containing required properties.</param>
        /// <returns></returns>
        Task<PostExplainAqlQueryResponse> PostExplainAqlQueryAsync(
            PostExplainAqlQueryBody body);

        /// <summary>
        /// Parse an AQL query and return information about it
        /// POST /_api/query
        /// </summary>
        /// <param name="body">The body of the request containing required properties.</param>
        /// <returns></returns>
        Task<PostParseAqlQueryResponse> PostParseAqlQueryAsync(
            PostParseAqlQueryBody body);

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
        Task<ResponseBase> DeleteKillRunningAqlQueryAsync(
            string queryId,
         DeleteKillRunningAqlQueryQuery query = null);

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
        Task<ResponseBase> DeleteClearSlowAqlQueriesAsync(
           DeleteClearSlowAqlQueriesQuery query = null);

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
        Task<GetSlowAqlQueriesResponse> GetSlowAqlQueriesAsync(
           GetSlowAqlQueriesQuery query = null);

        /// <summary>
        /// Clears the query results cache for the current database
        /// DELETE /_api/query-cache
        /// </summary>
        /// <returns></returns>
        Task<ResponseBase> DeleteClearAqlQueryCacheAsync();

    }
}
