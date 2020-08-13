using System.Net;
using System.Threading.Tasks;

using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using ArangoDBNetStandard.UserApi.Models;

namespace ArangoDBNetStandard.UserApi
{
    /// <summary>
    /// A client for interacting with ArangoDB User Management endpoints,
    /// implementing <see cref="IUserApiClient"/>.
    /// </summary>
    public class UserApiClient : ApiClientBase, IUserApiClient
    {
        /// <summary>
        /// The transport client used to communicate with the ArangoDB host.
        /// </summary>
        protected IApiClientTransport _client;

        /// <summary>
        /// The root path of the API.
        /// </summary>
        protected readonly string _userApiPath = "_api/user";

        /// <summary>
        /// Creates an instance of <see cref="UserApiClient"/>
        /// using the provided transport layer and the default JSON serialization.
        /// </summary>
        /// <param name="client"></param>
        public UserApiClient(IApiClientTransport client)
            : base(new JsonNetApiClientSerialization())
        {
            _client = client;
        }

        /// <summary>
        /// Creates an instance of <see cref="UserApiClient"/>
        /// using the provided transport and serialization layers.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="serializer"></param>
        public UserApiClient(IApiClientTransport client, IApiClientSerialization serializer)
            : base(serializer)
        {
            _client = client;
        }

        /// <summary>
        /// Create a new user. You need server access level Administrate
        /// in order to execute this REST call.
        /// </summary>
        /// <exception cref="ApiErrorException">ArangoDB responded with an error.</exception>
        /// <param name="body">The request body containing the user information.</param>
        /// <returns></returns>
        public virtual async Task<PostUserResponse> PostUserAsync(PostUserBody body)
        {
            var content = GetContent(body, true, true);
            using (var response = await _client.PostAsync(_userApiPath, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PostUserResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Replace an existing user.
        /// You need server access level Administrate in order to execute this REST call.
        /// Additionally, a user can change his/her own data.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="body">The user information used for to replace operation.</param>
        /// <returns></returns>
        public virtual async Task<PutUserResponse> PutUserAsync(string username, PutUserBody body)
        {
            string uri = _userApiPath + '/' + username;
            var content = GetContent(body, true, true);
            using (var response = await _client.PutAsync(uri, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PutUserResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Partially update an existing user.
        /// You need server access level Administrate in order to execute this REST call.
        /// Additionally, a user can change his/her own data.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="body">The user information used for to replace operation.</param>
        /// <returns></returns>
        public virtual async Task<PatchUserResponse> PatchUserAsync(string username, PatchUserBody body)
        {
            string uri = _userApiPath + '/' + username;
            var content = GetContent(body, true, true);
            using (var response = await _client.PatchAsync(uri, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<PatchUserResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Fetches data about the specified user.
        /// You can fetch information about yourself or you need the Administrate
        /// server access level in order to execute this REST call.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <returns></returns>
        public virtual async Task<GetUserResponse> GetUserAsync(string username)
        {
            string uri = _userApiPath + '/' + username;
            using (var response = await _client.GetAsync(uri))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<GetUserResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }

        /// <summary>
        /// Delete a user permanently.
        /// You need Administrate for the server access level in order to execute this REST call.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <returns></returns>
        public virtual async Task<DeleteUserResponse> DeleteUserAsync(string username)
        {
            string uri = _userApiPath + "/" + WebUtility.UrlEncode(username);
            using (var response = await _client.DeleteAsync(uri))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<DeleteUserResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }
    }
}
