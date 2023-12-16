using ArangoDBNetStandard;
using ArangoDBNetStandard.Transport;
using ArangoDBNetStandard.Transport.Http;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ArangoDBNetStandardTest
{
    public class ArangoDBClientTest : IClassFixture<ArangoDBClientTestFixture>
    {
        private readonly ArangoDBClientTestFixture _fixture;

        private readonly Uri _hostUri;

        public ArangoDBClientTest(ArangoDBClientTestFixture fixture)
        {
            _fixture = fixture;
            _hostUri = new Uri($"http://{fixture.ArangoDbHost}:{fixture.ArangoDbPort}/");
        }

        [Fact]
        public async Task Dispose_ShouldDisposeTransport_WhenTransportDisposalIsNotSuppressed()
        {
            var transport = HttpApiTransport.UsingBasicAuth(
                _hostUri,
                _fixture.DatabaseName,
                _fixture.Username,
                _fixture.Password);

            var dbClient = new ArangoDBClient(
                transport,
                suppressTransportDisposal: false);

            // Act

            dbClient.Dispose();

            // Assert

            await Assert.ThrowsAsync<ObjectDisposedException>(
                () => transport.GetAsync($"_db/{_fixture.DatabaseName}/_admin/echo"));
        }

        [Fact]
        public async Task Dispose_ShouldNotDisposeTransport_WhenTransportDisposalIsSuppressed()
        {
            var transport = HttpApiTransport.UsingBasicAuth(
                _hostUri,
                _fixture.DatabaseName,
                _fixture.Username,
                _fixture.Password);

            var dbClient = new ArangoDBClient(
                transport,
                suppressTransportDisposal: true);

            // Act

            dbClient.Dispose();

            // Assert

            IApiClientResponse response = await transport.GetAsync($"_admin/echo");

            Assert.True(response.IsSuccessStatusCode);

            transport.Dispose();
        }

        [Fact]
        public async Task Dispose_ShouldDisposeHttpClient_WhenClientDisposalIsNotSuppressed()
        {
            var client = new HttpClient()
            {
                BaseAddress = _hostUri
            };

            var dbClient = new ArangoDBClient(
                client,
                suppressClientDisposal: false);

            // Act

            dbClient.Dispose();

            // Assert

            await Assert.ThrowsAsync<ObjectDisposedException>(
                () => client.GetAsync($"_db/{_fixture.DatabaseName}/_admin/echo"));
        }

        [Fact]
        public async Task Dispose_ShouldNotDisposeHttpClient_WhenClientDisposalIsSuppressed()
        {
            var client = new HttpClient()
            {
                BaseAddress = _hostUri
            };

            string credentials = Convert.ToBase64String(
                Encoding.ASCII.GetBytes($"{_fixture.Username}:{_fixture.Password}"));

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", credentials);

            var dbClient = new ArangoDBClient(
                client,
                suppressClientDisposal: true);

            // Act

            dbClient.Dispose();

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
