using System;
using System.Threading.Tasks;

using ArangoDBNetStandard;
using ArangoDBNetStandard.AuthApi;
using ArangoDBNetStandard.CollectionApi;
using ArangoDBNetStandard.DatabaseApi;
using ArangoDBNetStandard.Transport.Http;

using Xunit;

namespace ArangoDBNetStandardTest.Transport.Http
{
    public class HttpApiTransportTest: IClassFixture<HttpApiTransportTestFixture>
    {
        private HttpApiTransportTestFixture _fixture;

        public HttpApiTransportTest(HttpApiTransportTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task UseVpack_ShouldSucceed()
        {
            string arangodbBaseUrl = $"http://{_fixture.ArangoDbHost}:8529/";
            using (var transport = HttpApiTransport.UsingBasicAuth(
                new Uri(arangodbBaseUrl),
                nameof(HttpApiTransportTest),
                "root",
                "root"))
            {
                transport.UseVPackContentType();
                string docUrl = arangodbBaseUrl + "_db/" + nameof(HttpApiTransportTest) + $"/_admin/echo";
                using (var response = await transport.GetAsync(docUrl))
                {
                    Assert.Equal("application/x-velocypack", response.Content.Headers.ContentType.MediaType);
                }
            }
        }

        [Fact]
        public async Task SetJwt_ShouldSucceed()
        {
            string jwtToken = null;
            string arangodbBaseUrl = $"http://{_fixture.ArangoDbHost}:8529/";
            using (var transport = HttpApiTransport.UsingNoAuth(
                    new Uri(arangodbBaseUrl),
                    nameof(HttpApiTransportTest)))
            {
                var authClient = new AuthApiClient(transport);

                var jwtTokenResponse = await authClient.GetJwtTokenAsync(
                    new JwtTokenRequestBody
                    {
                        Username = _fixture.Username,
                        Password = _fixture.Password
                    });

                jwtToken = jwtTokenResponse.Jwt;

                // Use token in current transport
                transport.SetJwtToken(jwtToken);
                var databaseApi = new DatabaseApiClient(transport);

                var userDatabasesResponse = await databaseApi.GetUserDatabasesAsync();
                Assert.NotEmpty(userDatabasesResponse.Result);
            }
        }

        [Fact]
        public async Task UsingJwtAuth_ShouldSucceed()
        {
            string jwtToken = null;
            string arangodbBaseUrl = $"http://{_fixture.ArangoDbHost}:8529/";
            using (var transport = HttpApiTransport.UsingNoAuth(
                    new Uri(arangodbBaseUrl),
                    nameof(HttpApiTransportTest)))
            {
                var authClient = new AuthApiClient(transport);

                var jwtTokenResponse = await authClient.GetJwtTokenAsync(
                    new JwtTokenRequestBody
                    {
                        Username = _fixture.Username,
                        Password = _fixture.Password
                    });

                jwtToken = jwtTokenResponse.Jwt;

                DatabaseApiClient databaseApi = new DatabaseApiClient(transport);

                // Not authorized, should throw.
                var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                    await databaseApi.GetCurrentDatabaseInfoAsync());
            }

            // Use token in a new transport created via `UsingJwtAuth`.
            using (var transport = HttpApiTransport.UsingJwtAuth(
                new Uri(arangodbBaseUrl),
                nameof(HttpApiTransportTest),
                jwtToken))
            {
                var databaseApi = new DatabaseApiClient(transport);
                var userDatabasesResponse = await databaseApi.GetUserDatabasesAsync();
                Assert.NotEmpty(userDatabasesResponse.Result);
            }
        }
    }
}
