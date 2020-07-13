using ArangoDBNetStandard.UserApi.Models;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.UserApi
{
    /// <summary>
    /// Defines a client to access the ArangoDB User API.
    /// </summary>
    public interface IUserApiClient
    {
        /// <summary>
        /// Create a new user. You need server access level Administrate
        /// in order to execute this REST call.
        /// </summary>
        /// <param name="body">The request body containing the user information.</param>
        /// <returns></returns>
        Task<PostUserResponse> PostUserAsync(PostUserBody body);

        Task<DeleteUserResponse> DeleteUserAsync(string username);
    }
}
