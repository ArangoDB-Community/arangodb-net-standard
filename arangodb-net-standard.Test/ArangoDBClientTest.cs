using ArangoDBNetStandard;
using ArangoDBNetStandard.Transport;
using Moq;
using Moq.Protected;
using System.Net.Http;
using Xunit;

namespace ArangoDBNetStandardTest
{
    public class ArangoDBClientTest
    {
        public ArangoDBClientTest()
        {
        }

        [Fact]
        public void Dispose_ShouldDisposeTransport_WhenTransportDisposalIsNotSuppressed()
        {
            var mockTransport = new Mock<IApiClientTransport>();

            var dbClient = new ArangoDBClient(
                mockTransport.Object,
                suppressTransportDisposal: false);

            // Act
            dbClient.Dispose();

            // Assert            
            mockTransport.Verify(client => client.Dispose(), Times.Once);
        }

        [Fact]
        public void Dispose_ShouldNotDisposeTransport_WhenTransportDisposalIsSuppressed()
        {
            var mockTransport = new Mock<IApiClientTransport>();

            var dbClient = new ArangoDBClient(
                mockTransport.Object,
                suppressTransportDisposal: true);

            // Act
            dbClient.Dispose();

            // Assert
            mockTransport.Verify(transport => transport.Dispose(), Times.Never);
        }

        [Fact]
        public void Dispose_ShouldDisposeHttpClient_WhenClientDisposalIsNotSuppressed()
        {
            var mockMessageHandler = new Mock<HttpMessageHandler>();

            var httpClient = new HttpClient(mockMessageHandler.Object);

            var dbClient = new ArangoDBClient(
                httpClient,
                suppressClientDisposal: false);

            // Act
            dbClient.Dispose();

            // Assert               
            mockMessageHandler.Protected().Verify("Dispose", Times.Once(), true, true);
        }

        [Fact]
        public void Dispose_ShouldNotDisposeHttpClient_WhenClientDisposalIsSuppressed()
        {
            var mockMessageHandler = new Mock<HttpMessageHandler>();

            var httpClient = new HttpClient(mockMessageHandler.Object);

            var dbClient = new ArangoDBClient(
                httpClient,
                suppressClientDisposal: true);

            // Act
            dbClient.Dispose();

            // Assert   
            mockMessageHandler.Protected().Verify("Dispose", Times.Never(), true, true);
        }
    }
}
