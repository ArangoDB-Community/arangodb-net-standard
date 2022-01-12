using System.Collections.Generic;
using System.Threading.Tasks;
using ArangoDBNetStandard.CursorApi.Models;

namespace ArangoDBNetStandard.CursorApi
{
    /// <summary>
    /// Defines a client to access the ArangoDB Cursor API.
    /// </summary>
    public interface ICursorApiClient
    {
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
        /// <returns></returns>
        Task<CursorResponse<T>> PostCursorAsync<T>(
                string query,
                Dictionary<string, object> bindVars = null,
                PostCursorOptions options = null,
                bool? count = null,
                long? batchSize = null,
                bool? cache = null,
                long? memoryLimit = null,
                int? ttl = null,
                string transactionId = null);

        /// <summary>
        /// Execute an AQL query, creating a cursor which can be used to page query results.
        /// </summary>
        /// <param name="postCursorBody">Object encapsulating options and parameters of the query.</param>
        /// <param name="headerProperties">Optional. Additional Header properties.</param>
        /// <returns></returns>
        Task<CursorResponse<T>> PostCursorAsync<T>(
            PostCursorBody postCursorBody, CursorHeaderProperties headerProperties = null);

        /// <summary>
        /// Deletes an existing cursor and frees the resources associated with it.
        /// DELETE /_api/cursor/{cursor-identifier}
        /// </summary>
        /// <param name="cursorId">The id of the cursor to delete.</param>
        /// <returns></returns>
        Task<DeleteCursorResponse> DeleteCursorAsync(string cursorId);

        /// <summary>
        /// Advances an existing query cursor and gets the next set of results.
        /// </summary>
        /// <typeparam name="T">Result type to deserialize to</typeparam>
        /// <param name="cursorId">ID of the existing query cursor.</param>
        /// <returns></returns>
        Task<PutCursorResponse<T>> PutCursorAsync<T>(string cursorId);
    }
}
