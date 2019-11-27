using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using ArangoDBNetStandard.GraphApi;
using System.Collections.Generic;
using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi;
using ArangoDBNetStandard.DocumentApi;
using ArangoDBNetStandardTest.GraphApi.Models;

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
            var graphName = nameof(PostGraphAsync_ShouldSucceed) + "_graph";
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
        public async Task PostGraphAsync_ShouldThrow_WhenGraphNameIsInvalid()
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

        [Fact]
        public async Task PostGraphEdgeDefinitionAsync_ShouldSucceed()
        {
            string tempGraph = nameof(PostGraphEdgeDefinitionAsync_ShouldSucceed);
            var postEdgeGraph = await _client.PostGraphAsync(new PostGraphBody
            {
                Name = tempGraph,
                EdgeDefinitions = new List<EdgeDefinition>()
            });
            var response = await _client.PostGraphEdgeDefinitionAsync(
                tempGraph,
                new PostGraphEdgeDefinitionBody
                {
                    From = new string[] { "fromclxx" },
                    To = new string[] { "toclxx" },
                    Collection = "clxx"
                });
            Assert.Equal(HttpStatusCode.Accepted, response.Code);
            Assert.False(response.Error);
            Assert.Single(response.Graph.EdgeDefinitions);
            Assert.Equal(tempGraph, response.Graph.Name);
            Assert.Equal("_graphs/" + tempGraph, response.Graph._id);
            Assert.NotNull(response.Graph._rev);
            Assert.False(response.Graph.IsSmart);
            Assert.Empty(response.Graph.OrphanCollections);
        }

        [Fact]
        public async Task PostGraphEdgeDefinitionAsync_ShouldThrow_WhenGraphNotFound()
        {
            var exception = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.PostGraphEdgeDefinitionAsync(
                    "boggus_graph",
                    new PostGraphEdgeDefinitionBody
                    {
                        From = new string[] { "fromclxx" },
                        To = new string[] { "toclxx" },
                        Collection = "clxx"
                    });
            });

            Assert.Equal(HttpStatusCode.NotFound, exception.ApiError.Code);
            Assert.Equal(1924, exception.ApiError.ErrorNum); // GRAPH_NOT_FOUND
        }

        [Fact]
        public async Task PostVertexCollectionAsync_ShouldSucceed()
        {
            // Create a new graph

            string graphName = nameof(PostVertexCollectionAsync_ShouldSucceed);

            PostGraphResponse createResponse = await _client.PostGraphAsync(
                new PostGraphBody()
                {
                    Name = graphName
                });

            // Add a vertex collection

            string clxToAdd = nameof(PostVertexCollectionAsync_ShouldSucceed);

            PostVertexCollectionResponse response = await _client.PostVertexCollectionAsync(
                graphName,
                new PostVertexCollectionBody()
                {
                    Collection = clxToAdd
                });

            Assert.Equal(HttpStatusCode.Accepted, response.Code);
            Assert.False(response.Error);

            GraphResult graph = response.Graph;

            Assert.Contains(clxToAdd, graph.OrphanCollections);
        }

        [Fact]
        public async Task PostVertexCollectionAsync_ShouldThrow_WhenGraphIsNotFound()
        {
            string graphName = nameof(PostVertexCollectionAsync_ShouldThrow_WhenGraphIsNotFound);

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.PostVertexCollectionAsync(graphName, new PostVertexCollectionBody()
                {
                    Collection = "VertexCollectionThatShouldNotBeCreated"
                });
            });

            ApiErrorResponse apiError = ex.ApiError;

            Assert.True(apiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, apiError.Code);
            Assert.Equal(1924, apiError.ErrorNum); // ERROR_GRAPH_NOT_FOUND
        }

        [Fact]
        public async Task PostVertexAsync_ShouldSucceed()
        {
            // Create a new graph

            string graphName = nameof(PostVertexAsync_ShouldSucceed);

            PostGraphResponse createResponse = await _client.PostGraphAsync(
                new PostGraphBody()
                {
                    Name = graphName
                });

            // Add a vertex collection

            string clxToAdd = nameof(PostVertexCollectionAsync_ShouldSucceed);

            PostVertexCollectionResponse createvertexClxresponse = await _client.PostVertexCollectionAsync(
                graphName,
                new PostVertexCollectionBody()
                {
                    Collection = clxToAdd
                });

            var response = await _client.PostVertexAsync<object>(graphName, clxToAdd, new
            {
                Name = clxToAdd + "_vtx"
            });

            Assert.Equal(HttpStatusCode.Accepted, response.Code);
            Assert.False(response.Error);
            Assert.NotNull(response.Vertex);
        }

        [Fact]
        public async Task PostVertexAsync_ShouldThrow_WhenGraphIsNotFound()
        {
            string graphName = nameof(PostVertexAsync_ShouldThrow_WhenGraphIsNotFound);
            string vertex = nameof(PostVertexAsync_ShouldThrow_WhenGraphIsNotFound) + "_vtx";

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.PostVertexAsync(graphName, vertex, new { });
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.Equal(1924, ex.ApiError.ErrorNum); // ERROR_GRAPH_NOT_FOUND
        }

        [Fact]
        public async Task PostVertexAsync_ShouldThrow_WhenVertexCollectionIsNotFound()
        {
            // Create a new graph
            string graphName = nameof(PostVertexAsync_ShouldThrow_WhenVertexCollectionIsNotFound);

            PostGraphResponse createResponse = await _client.PostGraphAsync(
                new PostGraphBody()
                {
                    Name = graphName
                });
            string vertex = nameof(PostVertexAsync_ShouldThrow_WhenVertexCollectionIsNotFound) + "_vtx";

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.PostVertexAsync(graphName, vertex, new { });
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.Equal(1203, ex.ApiError.ErrorNum); // ARANGO_DATA_SOURCE_NOT_FOUND
        }

        [Fact]
        public async Task PostVertexAsync_ShouldReturnNewVertex_WhenReturnNewIsTrue()
        {
            // Create a new graph

            string graphName = nameof(PostVertexAsync_ShouldReturnNewVertex_WhenReturnNewIsTrue);

            PostGraphResponse createResponse = await _client.PostGraphAsync(
                new PostGraphBody()
                {
                    Name = graphName
                });

            // Add a vertex collection

            string clxToAdd = nameof(PostVertexAsync_ShouldReturnNewVertex_WhenReturnNewIsTrue);

            PostVertexCollectionResponse createvertexClxresponse = await _client.PostVertexCollectionAsync(
                graphName,
                new PostVertexCollectionBody()
                {
                    Collection = clxToAdd
                });
            var propertyName = clxToAdd + "_vtx";

            var response = await _client.PostVertexAsync(graphName, clxToAdd, new
            {
                Name = propertyName
            }, new PostVertexQuery
            {
                ReturnNew = true,
                WaitForSync = true
            });

            Assert.Equal(HttpStatusCode.Created, response.Code);
            Assert.False(response.Error);
            Assert.NotNull(response.New);
            Assert.Equal(propertyName, response.New.Name);
        }

        [Fact]
        public async Task DeleteEdgeDefinitionAsync_ShouldSucceed()
        {
            string edgeClx = nameof(DeleteEdgeDefinitionAsync_ShouldSucceed) + "_EdgeClx";

            var createClxResponse = await _fixture.ArangoDBClient.Collection.PostCollectionAsync(
                new PostCollectionBody()
                {
                    Name = edgeClx,
                    Type = 3
                });

            Assert.Equal(edgeClx, createClxResponse.Name);

            string graphName = nameof(DeleteEdgeDefinitionAsync_ShouldSucceed);

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

            Assert.Equal(HttpStatusCode.Accepted, createGraphResponse.Code);

            var response = await _client.DeleteEdgeDefinitionAsync(graphName, edgeClx, new DeleteEdgeDefinitionQuery
            {
                WaitForSync = false,
                DropCollections = true
            });

            Assert.Equal(HttpStatusCode.Accepted, response.Code);
            Assert.Empty(response.Graph.EdgeDefinitions);

            var getAfterResponse = await _client.GetGraphEdgeCollectionsAsync(graphName);

            var collectionFound = getAfterResponse.Collections.Where(x => x == edgeClx).FirstOrDefault();

            Assert.Null(collectionFound);
        }

        [Fact]
        public async Task DeleteEdgeDefinitionAsync_ShouldThrow_WhenEdgeDefinitionNameDoesNotExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.DeleteEdgeDefinitionAsync(_fixture.TestGraph, "bogus_edgeclx", new DeleteEdgeDefinitionQuery
                {
                    WaitForSync = false,
                    DropCollections = true
                });
            });

            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.Equal(1930, ex.ApiError.ErrorNum); // GRAPH_EDGE_COLLECTION_NOT_USED
        }

        [Fact]
        public async Task DeleteEdgeDefinitionAsync_ShouldThrow_WhenGraphNameDoesNotExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.DeleteEdgeDefinitionAsync("bogus_graph", _fixture.TestCollection, new DeleteEdgeDefinitionQuery
                {
                    WaitForSync = false,
                    DropCollections = true
                });
            });

            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.Equal(1924, ex.ApiError.ErrorNum); // GRAPH_NOT_FOUND
        }

        [Fact]
        public async Task DeleteVertexCollectionAsync_ShouldSucceed()
        {
            // Create a new graph

            string graphName = nameof(DeleteVertexCollectionAsync_ShouldSucceed);

            PostGraphResponse createResponse = await _client.PostGraphAsync(
                new PostGraphBody()
                {
                    Name = graphName
                });

            // Add a vertex collection

            string clxToDelete = nameof(DeleteVertexCollectionAsync_ShouldSucceed);

            PostVertexCollectionResponse createVertexResponse = await _client.PostVertexCollectionAsync(
                graphName,
                new PostVertexCollectionBody()
                {
                    Collection = clxToDelete
                });

            var response = await _client.DeleteVertexCollectionAsync(graphName, clxToDelete, new DeleteVertexCollectionQuery
            {
                DropCollection = false
            });

            Assert.Equal(HttpStatusCode.Accepted, response.Code);
            Assert.False(response.Error);
            Assert.Empty(response.Graph.OrphanCollections);
        }

        [Fact]
        public async Task DeleteVertexCollectionAsync_ShouldThrow_WhenGraphIsNotFound()
        {
            string graphName = nameof(DeleteVertexCollectionAsync_ShouldThrow_WhenGraphIsNotFound);
            string clxToDelete = nameof(DeleteVertexCollectionAsync_ShouldThrow_WhenGraphIsNotFound);

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.DeleteVertexCollectionAsync(graphName, clxToDelete);
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.Equal(1924, ex.ApiError.ErrorNum); // ERROR_GRAPH_NOT_FOUND
        }

        [Fact]
        public async Task DeleteVertexCollectionAsync_ShouldThrow_WhenVertexIsNotFound()
        {
            string graphName = nameof(DeleteVertexCollectionAsync_ShouldThrow_WhenVertexIsNotFound);

            PostGraphResponse createResponse = await _client.PostGraphAsync(
               new PostGraphBody()
               {
                   Name = graphName
               });

            string clxToDelete = nameof(DeleteVertexCollectionAsync_ShouldThrow_WhenVertexIsNotFound);

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.DeleteVertexCollectionAsync(graphName, clxToDelete);
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.BadRequest, ex.ApiError.Code);
            Assert.Equal(1928, ex.ApiError.ErrorNum); // GRAPH_NOT_IN_ORPHAN_COLLECTION
        }

        [Fact]
        public async Task DeleteVertexCollectionAsync_ShouldDropCollection_WhenDropCollectionIsTrue()
        {
            // Create a new graph

            string graphName = "DeleteVertexCollectionAsync_ShouldThrowNotFound_WhenCollectionDropIsTrue";

            PostGraphResponse createResponse = await _client.PostGraphAsync(
                new PostGraphBody()
                {
                    Name = graphName
                });

            // Add a vertex collection

            string clxToDelete = "DeleteVertexCollectionAsync_ShouldDropCollection";

            PostVertexCollectionResponse createVertexResponse = await _client.PostVertexCollectionAsync(
                graphName,
                new PostVertexCollectionBody()
                {
                    Collection = clxToDelete
                });

            var response = await _client.DeleteVertexCollectionAsync(graphName, clxToDelete, new DeleteVertexCollectionQuery
            {
                DropCollection = true
            });

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _fixture.ArangoDBClient.Collection.GetCollectionAsync(clxToDelete);
            });
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.Equal(1203, ex.ApiError.ErrorNum); // ARANGO_DATA_SOURCE_NOT_FOUND
        }

        [Fact]
        public async Task PostGraphEdgeAsync_ShouldSucceed()
        {
            string graphName = nameof(PostGraphEdgeAsync_ShouldSucceed);
            string fromClx = graphName + "_fromclx";
            string toClx = graphName + "_toclx";
            string edgeClx = graphName + "_edgeclx";

            // Create a new graph

            await _fixture.ArangoDBClient.Graph.PostGraphAsync(new PostGraphBody
            {
                Name = graphName,
                EdgeDefinitions = new List<EdgeDefinition>
                {
                    new EdgeDefinition
                    {
                        From = new string[] { fromClx },
                        To = new string[] { toClx },
                        Collection = edgeClx
                    }
                }
            });

            // Create a document in the vertex collections

            PostDocumentResponse<object> fromResponse = await
                _fixture.ArangoDBClient.Document.PostDocumentAsync<object>(
                fromClx,
                new { myKey = "myValue" });

            PostDocumentResponse<object> toResponse = await
                _fixture.ArangoDBClient.Document.PostDocumentAsync<object>(
                toClx,
                new { myKey = "myValue" });

            // Create the edge

            var response = await _client.PostGraphEdgeAsync(
                graphName,
                edgeClx,
                new
                {
                    _from = fromResponse._id,
                    _to = toResponse._id,
                    myKey = "myValue"
                },
                new PostGraphEdgeQuery
                {
                    ReturnNew = true,
                    WaitForSync = true
                });

            Assert.Equal(HttpStatusCode.Created, response.Code);
            Assert.False(response.Error);
            Assert.NotNull(response.Edge);
            Assert.NotNull(response.Edge._id);
            Assert.NotNull(response.Edge._key);
            Assert.NotNull(response.Edge._rev);
            Assert.NotNull(response.New);
            Assert.Equal("myValue", response.New.myKey);
        }

        [Fact]
        public async Task PostGraphEdgeAsync_ShouldThrow_WhenGraphNotFound()
        {
            string graphName = nameof(PostGraphEdgeAsync_ShouldThrow_WhenGraphNotFound);

            var exception = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.PostGraphEdgeAsync(graphName, "edgeClx", new
                {
                    myKey = "myValue"
                });
            });

            Assert.Equal(HttpStatusCode.NotFound, exception.ApiError.Code);
            Assert.Equal(1924, exception.ApiError.ErrorNum); // GRAPH_NOT_FOUND
        }

        [Fact]
        public async Task GetVertexAsync_ShouldSucceed()
        {
            // Create a new graph

            string graphName = nameof(GetVertexAsync_ShouldSucceed);

            PostGraphResponse createResponse = await _client.PostGraphAsync(
                new PostGraphBody()
                {
                    Name = graphName
                });

            // Add a vertex collection

            string clxToAdd = nameof(GetVertexAsync_ShouldSucceed);

            PostVertexCollectionResponse createvertexClxresponse = await _client.PostVertexCollectionAsync(
                graphName,
                new PostVertexCollectionBody()
                {
                    Collection = clxToAdd
                });

            var createVtxResponse = await _client.PostVertexAsync<object>(graphName, clxToAdd, new
            {
                Name = clxToAdd + "_vtx"
            });

            var response = await _client.GetVertexAsync<GetVertexMockModel>(graphName, clxToAdd, createVtxResponse.Vertex._key);

            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.False(response.Error);
            Assert.NotNull(response.Vertex);
            Assert.Equal(clxToAdd + "_vtx", response.Vertex.Name);
            Assert.Equal(createVtxResponse.Vertex._key, response.Vertex._key);
            Assert.Equal(createVtxResponse.Vertex._id, response.Vertex._id);
        }

        [Fact]
        public async Task GetVertexAsync_ShouldThrow_WhenVertexCollectionIsNotFound()
        {
            // Create a new graph
            string graphName = nameof(GetVertexAsync_ShouldThrow_WhenVertexCollectionIsNotFound);

            PostGraphResponse createResponse = await _client.PostGraphAsync(
                new PostGraphBody()
                {
                    Name = graphName
                });
            string vertex = nameof(GetVertexAsync_ShouldThrow_WhenVertexCollectionIsNotFound) + "_vtx";

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.GetVertexAsync<GetVertexMockModel>(graphName, vertex, "12345");
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.Equal(1203, ex.ApiError.ErrorNum); // ARANGO_DATA_SOURCE_NOT_FOUND
        }

        [Fact]
        public async Task GetVertexAsync_ShouldThrow_WhenVertexIsNotFound()
        {
            // Create a new graph
            string graphName = nameof(GetVertexAsync_ShouldThrow_WhenVertexIsNotFound);

            PostGraphResponse createResponse = await _client.PostGraphAsync(
                new PostGraphBody()
                {
                    Name = graphName
                });
            string vertex = nameof(GetVertexAsync_ShouldThrow_WhenVertexIsNotFound) + "_vtx";

            PostVertexCollectionResponse createvertexClxresponse = await _client.PostVertexCollectionAsync(
                graphName,
                new PostVertexCollectionBody()
                {
                    Collection = vertex
                });

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.GetVertexAsync<GetVertexMockModel>(graphName, vertex, "12345");
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.Equal(1202, ex.ApiError.ErrorNum); // ARANGO_DOCUMENT_NOT_FOUND
        }

        [Fact]
        public async Task GetVertexAsync_ShouldThrow_WhenGraphIsNotFound()
        {
            string graphName = nameof(GetVertexAsync_ShouldThrow_WhenGraphIsNotFound);
            string vertex = nameof(GetVertexAsync_ShouldThrow_WhenGraphIsNotFound) + "_vtx";

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.GetVertexAsync<GetVertexMockModel>(graphName, vertex, "12345");
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.Equal(1924, ex.ApiError.ErrorNum); // ERROR_GRAPH_NOT_FOUND
        }
    }
}
