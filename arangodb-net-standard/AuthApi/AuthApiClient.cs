using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.AuthApi
{
    public class AuthApiClient: ApiClientBase
    {
        private IApiClientTransport _client;

        public AuthApiClient(IApiClientTransport client)
            : base(new JsonNetApiClientSerialization())
        {
            _client = client;
        }

        public AuthApiClient(IApiClientTransport client, IApiClientSerialization serializer)
            : base(serializer)
        {
            _client = client;
        }

        public async Task<JwtTokenResponse> GetJwtToken(JwtTokenRequestBody body)
        {
            byte[] content = GetContent(body, true, false);
            using (var response = await _client.PostAsync("/_open/auth", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return DeserializeJsonFromStream<JwtTokenResponse>(stream);
                }
                throw await GetApiErrorException(response);
            }
        }
    }
}
