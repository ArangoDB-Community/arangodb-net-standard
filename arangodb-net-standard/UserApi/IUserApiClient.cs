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

        /// <summary>
        /// Replace an existing user.
        /// You need server access level Administrate in order to execute this REST call.
        /// Additionally, a user can change his/her own data.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="body">The user information used for to replace operation.</param>
        /// <returns></returns>
        Task<PutUserResponse> PutUserAsync(string username, PutUserBody body);

        /// <summary>
        /// Partially update an existing user.
        /// You need server access level Administrate in order to execute this REST call.
        /// Additionally, a user can change his/her own data.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="body">The user information used for to replace operation.</param>
        /// <returns></returns>
        Task<PatchUserResponse> PatchUserAsync(string username, PatchUserBody body);

        /// <summary>
        /// Fetches data about the specified user.
        /// You can fetch information about yourself or you need the Administrate
        /// server access level in order to execute this REST call.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <returns></returns>
        Task<GetUserResponse> GetUserAsync(string username);

        /// <summary>
        /// Delete a user permanently.
        /// You need Administrate for the server access level in order to execute this REST call.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <returns></returns>
        Task<DeleteUserResponse> DeleteUserAsync(string username);
    }
}
