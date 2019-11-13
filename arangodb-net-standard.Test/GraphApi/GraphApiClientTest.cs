using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using ArangoDBNetStandard.GraphApi;
using System.Collections.Generic;
using ArangoDBNetStandard;

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
            Assert.Equal(_fixture.TestGraph, graph._key);
            Assert.Equal("_graphs/" + _fixture.TestGraph, graph._id);
            Assert.NotNull(graph._rev);
        }

        [Fact]
        public async Task DeleteGraphAsync_ShouldSucceed()
        {
            await _fixture.ArangoDBClient.Graph.PostGraph(new PostGraphBody
            {
                Name = "temp_graph",
                EdgeDefinitions = new List<EdgeDefinition>
                {
                    new EdgeDefinition
                    {
                        From = new string[] { "fromclx" },
                        To = new string[] { "toclx" },
                        Collection = "clx"
                    }
                }
            });
            var query = new DeleteGraphQuery
            {
                DropCollections = 12
            };
            var response = await _client.DeleteGraphAsync("temp_graph", query);
            Assert.Equal(HttpStatusCode.Accepted, response.Code);
            Assert.True(response.Removed);
            Assert.False(response.Error);
        }

        [Fact]
        public async Task DeleteGraphAsync_ShouldThrow_WhenNotFound()
        {
            var exception = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.DeleteGraphAsync("boggus_graph", new DeleteGraphQuery
                {
                    DropCollections = false
                });
            });

            Assert.Equal(HttpStatusCode.NotFound, exception.ApiError.Code);
            Assert.Equal(1924, exception.ApiError.ErrorNum); // GRAPH_NOT_FOUND
        }
    }
}
