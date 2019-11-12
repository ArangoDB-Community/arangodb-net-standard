using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using ArangoDBNetStandard.GraphApi;

namespace ArangoDBNetStandardTest.GraphApi
{
    public class GraphApiClientTest : IClassFixture<GraphApiClientTestFixture>
    {
        private readonly GraphApiClientTestFixture _fixture;
        private readonly GraphApiClient _client;

        public GraphApiClientTest(GraphApiClientTestFixture fixture)
        {
            _fixture = fixture;
            _client = fixture.ArangoDBClient.Graph;
        }

        [Fact]
        public async Task GetGraphsAsync_ShouldSucceed()
        {
            // get the list of graphs
            var graphsResult = await _fixture.ArangoDBClient.Graph.GetGraphs();

            // test result
            Assert.Equal(HttpStatusCode.OK, graphsResult.Code);
            Assert.Single(graphsResult.Graphs);

            var graph = graphsResult.Graphs.First();
            Assert.Single(graph.EdgeDefinitions);
            Assert.Empty(graph.OrphanCollections);
            Assert.Equal(1, graph.NumberOfShards);
            Assert.Equal(1, graph.ReplicationFactor);
            Assert.False(graph.IsSmart);
            Assert.Equal("MyGraph", graph._key);
            Assert.Equal("_graphs/MyGraph", graph._id);
            Assert.NotNull(graph._rev);
        }
    }
}
