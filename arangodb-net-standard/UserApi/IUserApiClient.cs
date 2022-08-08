using ArangoDBNetStandard.UserApi.Models;
using System.Threading;
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
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<PostUserResponse> PostUserAsync(PostUserBody body,
            CancellationToken token = default);

        /// <summary>
        /// Replace an existing user.
        /// You need server access level Administrate in order to execute this REST call.
        /// Additionally, a user can change his/her own data.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="body">The user information used for to replace operation.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<PutUserResponse> PutUserAsync(string username, PutUserBody body,
            CancellationToken token = default);

        /// <summary>
        /// Partially update an existing user.
        /// You need server access level Administrate in order to execute this REST call.
        /// Additionally, a user can change his/her own data.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="body">The user information used for to replace operation.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<PatchUserResponse> PatchUserAsync(string username, PatchUserBody body,
            CancellationToken token = default);

        /// <summary>
        /// Fetches data about the specified user.
        /// You can fetch information about yourself or you need the Administrate
        /// server access level in order to execute this REST call.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<GetUserResponse> GetUserAsync(string username,
            CancellationToken token = default);

        /// <summary>
        /// Delete a user permanently.
        /// You need Administrate for the server access level in order to execute this REST call.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<DeleteUserResponse> DeleteUserAsync(string username,
            CancellationToken token = default);

        /// <summary>
        /// Fetches data about all users.
        /// You need the Administrate server access level in order to execute this REST call.
        /// Otherwise, you will only get information about yourself.
        /// </summary>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<GetUsersResponse> GetUsersAsync(
            CancellationToken token = default);

        /// <summary>
        /// Sets the database access levels of a user for a given database.
        /// You need the Administrate server access level in order to execute this REST call.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="dbName">The name of the database.</param>
        /// <param name="body">The body of the request containing the access level.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<PutAccessLevelResponse> PutDatabaseAccessLevelAsync(
            string username,
            string dbName,
            PutAccessLevelBody body,
            CancellationToken token = default);

        /// <summary>
        /// Gets specific database access level for a user.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="dbName">The name of the database to query.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<GetAccessLevelResponse> GetDatabaseAccessLevelAsync(
            string username,
            string dbName,
            CancellationToken token = default);

        /// <summary>
        /// Clears the database access levels of a user for a given database.
        /// As consequence the default database access level is used.
        /// If there is no defined default database access level, it defaults to 'No access'.
        /// You need permission to the '_system' database in order to execute this REST call.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="dbName">The name of the database.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<DeleteAccessLevelResponse> DeleteDatabaseAccessLevelAsync(
            string username,
            string dbName,
            CancellationToken token = default);

        /// <summary>
        /// Fetch the list of databases available to the specified user.
        /// You need Administrate for the server access level in order to execute this REST call.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="query">Optional query parameters for the request.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<GetAccessibleDatabasesResponse> GetAccessibleDatabasesAsync(
            string username,
            GetAccessibleDatabasesQuery query = null,
            CancellationToken token = default);

        /// <summary>
        /// Sets the collection access levels of a user for a given database.
        /// You need the Administrate server access level in order to execute this REST call.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="dbName">The name of the database.</param>
        /// <param name="collectionName">The name of the collection.</param>
        /// <param name="body">The body of the request containing the access level.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<PutAccessLevelResponse> PutCollectionAccessLevelAsync(
            string username,
            string dbName,
            string collectionName,
            PutAccessLevelBody body,
            CancellationToken token = default);

        /// <summary>
        /// Gets specific collection access level of a user for a given database.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="dbName">The name of the database.</param>
        /// <param name="collectionName">The name of the collection.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<GetAccessLevelResponse> GetCollectionAccessLevelAsync(
            string username,
            string dbName,
            string collectionName,
            CancellationToken token = default);

        /// <summary>
        /// Clears the collection access levels of a user for a given database.
        /// As consequence the default collection access level is used.
        /// If there is no defined default database access level, it defaults to 'No access'.
        /// You need permission to the '_system' database in order to execute this REST call.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="dbName">The name of the database.</param>
        /// <param name="collectionName">The name of the collection.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<DeleteAccessLevelResponse> DeleteCollectionAccessLevelAsync(
            string username,
            string dbName,
            string collectionName,
            CancellationToken token = default);
    }
}
