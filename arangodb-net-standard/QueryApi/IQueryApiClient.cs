using System.Threading.Tasks;
using ArangoDBNetStandard.QueryApi.Models;

namespace ArangoDBNetStandard.QueryApi
{
    /// <summary>
    /// Defines a client to access the ArangoDB Query API.
    /// </summary>
    public interface IQueryApiClient
    {
        /// <summary>
        /// Posts a single query that returns basic statistics about the result.
        /// </summary>
        /// <param name="postQueryBody">Object encapsulating options and parameters of the query.</param>
        /// <returns></returns>
        Task<ExecuteNonQueryResponse> PostExecuteNonQueryAsync(PostQueryBody postQueryBody);
    }
}