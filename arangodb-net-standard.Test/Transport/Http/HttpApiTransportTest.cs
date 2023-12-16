using ArangoDBNetStandard;
using ArangoDBNetStandard.AuthApi;
using ArangoDBNetStandard.AuthApi.Models;
using ArangoDBNetStandard.DatabaseApi;
using ArangoDBNetStandard.Transport.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ArangoDBNetStandardTest.Transport.Http
{
    public class HttpApiTransportTest : IClassFixture<HttpApiTransportTestFixture>
    {
        private readonly HttpApiTransportTestFixture _fixture;

        private readonly Uri _hostUri;

        public HttpApiTransportTest(HttpApiTransportTestFixture fixture)
        {
            _fixture = fixture;
            _hostUri = new Uri($"http://{fixture.ArangoDbHost}:{fixture.ArangoDbPort}/");
        }

        [Fact]
        public async Task UseVpack_ShouldSucceed()
        {
            using (var transport = HttpApiTransport.UsingBasicAuth(
                _hostUri,
                _fixture.DatabaseName,
                _fixture.Username,
                _fixture.Password))
            {
                transport.UseVPackContentType();
                using (var response = await transport.GetAsync("_admin/echo"))
                {
                    Assert.True(
                        response.IsSuccessStatusCode,
                        $"Error response. Status code: {response.StatusCode}");

                    Assert.Equal("application/x-velocypack", response.Content.Headers.ContentType.MediaType);
                }
            }
        }

        [Fact]
        public async Task SetJwt_ShouldSucceed()
        {
            string jwtToken = null;
            using (var transport = HttpApiTransport.UsingNoAuth(
                    _hostUri,
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
            using (var transport = HttpApiTransport.UsingNoAuth(
                    _hostUri,
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
                _hostUri,
                nameof(HttpApiTransportTest),
                jwtToken))
            {
                var databaseApi = new DatabaseApiClient(transport);
                var userDatabasesResponse = await databaseApi.GetUserDatabasesAsync();
                Assert.NotEmpty(userDatabasesResponse.Result);
            }
        }

        [Fact]
        public async Task Dispose_ShouldDisposeHttpClient_WhenDisposalIsNotSuppressed()
        {
            var client = new HttpClient()
            {
                BaseAddress = _hostUri
            };

            var transport = new HttpApiTransport(
                client,
                HttpContentType.Json,
                suppressClientDisposal: false);

            transport.SetBasicAuth(_fixture.Username, _fixture.Password);

            // Act

            transport.Dispose();

            // Assert

            await Assert.ThrowsAsync<ObjectDisposedException>(
                () => client.GetAsync($"_db/{_fixture.DatabaseName}/_admin/echo"));
        }

        [Fact]
        public async Task Dispose_ShouldNotDisposeHttpClient_WhenDisposalIsSuppressed()
        {
            var client = new HttpClient()
            {
                BaseAddress = _hostUri
            };

            var transport = new HttpApiTransport(
                client,
                HttpContentType.Json,
                suppressClientDisposal: true);

            transport.SetBasicAuth(_fixture.Username, _fixture.Password);

            // Act

            transport.Dispose();

            // Assert

            using (HttpResponseMessage response = await client.GetAsync(
                $"_db/{_fixture.DatabaseName}/_admin/echo"))
            {
                Assert.True(
                    response.IsSuccessStatusCode,
                    $"Error response. Status code: {response.StatusCode}");
            }

            client.Dispose();
        }
    }
}
