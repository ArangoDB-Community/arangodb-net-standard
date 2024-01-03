using ArangoDBNetStandard;
using ArangoDBNetStandard.AuthApi;
using ArangoDBNetStandard.AuthApi.Models;
using ArangoDBNetStandard.DatabaseApi;
using ArangoDBNetStandard.Transport.Http;
using Moq;
using Moq.Protected;
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
        public void Dispose_ShouldDisposeHttpClient_WhenDisposalIsNotSuppressed()
        {
            var mockMessageHandler = new Mock<HttpMessageHandler>();

            var httpClient = new HttpClient(mockMessageHandler.Object);

            var transport = new HttpApiTransport(
                httpClient,
                HttpContentType.Json,
                suppressClientDisposal: false);

            // Act
            transport.Dispose();

            // Assert   
            mockMessageHandler.Protected().Verify("Dispose", Times.Once(), true, true);
        }

        [Fact]
        public void Dispose_ShouldNotDisposeHttpClient_WhenDisposalIsSuppressed()
        {
            var mockMessageHandler = new Mock<HttpMessageHandler>();

            var httpClient = new HttpClient(mockMessageHandler.Object);

            var transport = new HttpApiTransport(
                httpClient,
                HttpContentType.Json,
                suppressClientDisposal: true);

            // Act
            transport.Dispose();

            // Assert   
            mockMessageHandler.Protected().Verify("Dispose", Times.Never(), true, true);
        }
    }
}
