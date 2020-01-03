using System.Net;
using System.Threading.Tasks;

using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using ArangoDBNetStandard.UserApi.Models;

namespace ArangoDBNetStandard.UserApi
{
    public class UserApiClient : ApiClientBase, IUserApiClient
    {
        private IApiClientTransport _client;
        private readonly string _userApiPath = "_api/user";

        public UserApiClient(IApiClientTransport client)
            : base(new JsonNetApiClientSerialization())
        {
            _client = client;
        }

        public UserApiClient(IApiClientTransport client, IApiClientSerialization serializer)
            : base(serializer)
        {
            _client = client;
        }

        public async Task<DeleteUserResponse> DeleteUserAsync(string username)
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
