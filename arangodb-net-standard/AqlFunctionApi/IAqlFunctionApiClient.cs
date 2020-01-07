using ArangoDBNetStandard.AqlFunctionApi.Models;
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
    }
}
