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
        /// Delete a user permanently.
        /// You need Administrate for the server access level in order to execute this REST call.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <returns></returns>
        public virtual async Task<DeleteUserResponse> DeleteUserAsync(string username)
        {
            string uri = _userApiPath + "/" + WebUtility.HtmlEncode(username);
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
