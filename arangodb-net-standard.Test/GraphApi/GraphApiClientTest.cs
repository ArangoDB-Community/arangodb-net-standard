using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using ArangoDBNetStandard.GraphApi;
using System.Collections.Generic;
using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi;

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
            var graphsResult = await _fixture.ArangoDBClient.Graph.GetGraphsAsync();

            // test result
            Assert.Equal(HttpStatusCode.OK, graphsResult.Code);
            Assert.NotEmpty(graphsResult.Graphs);

            var graph = graphsResult.Graphs.First(x => x._key == _fixture.TestGraph);
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
            await _fixture.ArangoDBClient.Graph.PostGraphAsync(new PostGraphBody
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
                DropCollections = false
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

        [Fact]
        public async Task GetGraphAsync_ShouldSucceed()
        {
            var response = await _client.GetGraphAsync(_fixture.TestGraph);
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.Equal("_graphs/" + _fixture.TestGraph, response.Graph._id);
            Assert.Single(response.Graph.EdgeDefinitions);
            Assert.Empty(response.Graph.OrphanCollections);
            Assert.Equal(1, response.Graph.NumberOfShards);
            Assert.Equal(1, response.Graph.ReplicationFactor);
            Assert.False(response.Graph.IsSmart);
            Assert.Equal(_fixture.TestGraph, response.Graph._key);
            Assert.Equal("_graphs/" + _fixture.TestGraph, response.Graph._id);
            Assert.NotNull(response.Graph._rev);
        }

        [Fact]
        public async Task GetGraphAsync_ShouldThrow_WhenNotFound()
        {
            var exception = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.GetGraphAsync("bogus_graph");
            });
            Assert.Equal(HttpStatusCode.NotFound, exception.ApiError.Code);
            Assert.Equal(1924, exception.ApiError.ErrorNum); // GRAPH_NOT_FOUND
        }

        [Fact]
        public async Task GetVertexCollectionsAsync_ShouldSucceed()
        {
            // Create an edge collection

            string edgeClx = nameof(GetGraphsAsync_ShouldSucceed) + "_EdgeClx";

            var createClxResponse = await _fixture.ArangoDBClient.Collection.PostCollectionAsync(
                new PostCollectionBody()
                {
                    Name = edgeClx,
                    Type = 3
                });

            Assert.Equal(edgeClx, createClxResponse.Name);

            // Create a Graph

            string graphName = nameof(GetVertexCollectionsAsync_ShouldSucceed);

            PostGraphResponse createGraphResponse = await _client.PostGraphAsync(new PostGraphBody()
            {
                Name = graphName,
                EdgeDefinitions = new List<EdgeDefinition>()
                {
                    new EdgeDefinition()
                    {
                        Collection = edgeClx,
                        From = new string[] { "FromCollection" },
                        To = new string[] { "ToCollection" }
                    }
                }
            });

            // List the vertex collections

            GetVertexCollectionsResponse response = await _client.GetVertexCollections(graphName);

            Assert.Equal(2, response.Collections.Count());
            Assert.Contains("FromCollection", response.Collections);
            Assert.Contains("ToCollection", response.Collections);
        }

        [Fact]
        public async Task GetVertexCollectionsAsync_ShouldThrow_WhenGraphDoesNotExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.GetVertexCollections("GraphThatDoesNotExist");
            });

            ApiErrorResponse apiError = ex.ApiError;

            Assert.Equal(HttpStatusCode.NotFound, apiError.Code);
            Assert.Equal(1924, apiError.ErrorNum);
        }

        [Fact]
        public async Task GetGraphEdgeCollectionsAsync_ShouldSucceed()
        {
            var response = await _client.GetGraphEdgeCollectionsAsync(_fixture.TestGraph);
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.NotEmpty(response.Collections);
            Assert.False(response.Error);
        }

        [Fact]
        public async Task GetGraphEdgeCollectionsAsync_ShouldThrow_WhenGraphIsNotFound()
        {
            var exception = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.GetGraphEdgeCollectionsAsync("bogus_graph");
            });
            Assert.Equal(HttpStatusCode.NotFound, exception.ApiError.Code);
            Assert.Equal(1924, exception.ApiError.ErrorNum); // GRAPH_NOT_FOUND
        }

        [Fact]
        public async Task PostGraphAsync_ShouldSucceed()
        {
            var graphName = nameof(PostGraphAsync_ShouldSucceed) + "_clx";
            var response = await _client.PostGraphAsync(new PostGraphBody
            {
                Name = graphName,
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

            Assert.Equal(HttpStatusCode.Accepted, response.Code);
            Assert.Single(response.Graph.EdgeDefinitions);
            Assert.Equal(graphName, response.Graph.Name);
        }

        [Fact]
        public async Task PostGraphAsync_ShouldThrow_WhenInvalidName()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.PostGraphAsync(new PostGraphBody
                {
                    Name = "Bad Graph Name",
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
            });

            Assert.Equal(HttpStatusCode.BadRequest, ex.ApiError.Code);
            Assert.Equal(1221, ex.ApiError.ErrorNum); // ARANGO_DOCUMENT_KEY_BAD
        }
    }
}
