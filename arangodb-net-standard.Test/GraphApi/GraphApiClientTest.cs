using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi.Models;
using ArangoDBNetStandard.DocumentApi.Models;
using ArangoDBNetStandard.GraphApi;
using ArangoDBNetStandard.GraphApi.Models;
using ArangoDBNetStandardTest.GraphApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

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
            Assert.NotEmpty(graph.EdgeDefinitions);
            Assert.Empty(graph.OrphanCollections);
            Assert.Equal(_fixture.TestGraph, graph._key);
            Assert.Equal("_graphs/" + _fixture.TestGraph, graph._id);
            Assert.NotNull(graph._rev);

            // check smart properties once tests can run on enterprise edition
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
            Assert.NotEmpty(response.Graph.EdgeDefinitions);
            Assert.Empty(response.Graph.OrphanCollections);
            Assert.Equal(_fixture.TestGraph, response.Graph._key);
            Assert.Equal("_graphs/" + _fixture.TestGraph, response.Graph._id);
            Assert.NotNull(response.Graph._rev);

            // check smart properties once tests can run on enterprise edition
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
                    Type = CollectionType.Edge
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

            GetVertexCollectionsResponse response = await _client.GetVertexCollectionsAsync(graphName);

            Assert.Equal(2, response.Collections.Count());
            Assert.Contains("FromCollection", response.Collections);
            Assert.Contains("ToCollection", response.Collections);
        }

        [Fact]
        public async Task GetVertexCollectionsAsync_ShouldThrow_WhenGraphDoesNotExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.GetVertexCollectionsAsync("GraphThatDoesNotExist");
            });

            ApiErrorResponse apiError = ex.ApiError;

            Assert.Equal(HttpStatusCode.NotFound, apiError.Code);
            Assert.Equal(1924, apiError.ErrorNum);
        }

        [Fact]
        public async Task GetEdgeCollectionsAsync_ShouldSucceed()
        {
            var response = await _client.GetEdgeCollectionsAsync(_fixture.TestGraph);
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.NotEmpty(response.Collections);
            Assert.False(response.Error);
        }

        [Fact]
        public async Task GetEdgeCollectionsAsync_ShouldThrow_WhenGraphIsNotFound()
        {
            var exception = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.GetEdgeCollectionsAsync("bogus_graph");
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
                },
                OrphanCollections = new List<string>()
                {
                    "myclx"
                }
            });

            Assert.Equal(HttpStatusCode.Accepted, response.Code);
            Assert.Single(response.Graph.EdgeDefinitions);
            Assert.Contains(response.Graph.OrphanCollections, x => x == "myclx");
            Assert.Equal(graphName, response.Graph.Name);
        }

        [Fact]
        public async Task PostGraphAsync_ShouldSucceed_WhenWaitForSyncIsTrue()
        {
            string graphName = nameof(PostGraphAsync_ShouldSucceed_WhenWaitForSyncIsTrue);

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
            },
            new PostGraphQuery()
            {
                WaitForSync = true
            });

            Assert.Equal(HttpStatusCode.Created, response.Code);
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
        public async Task PostEdgeDefinitionAsync_ShouldSucceed()
        {
            string tempGraph = nameof(PostEdgeDefinitionAsync_ShouldSucceed);
            var postEdgeGraph = await _client.PostGraphAsync(new PostGraphBody
            {
                Name = tempGraph,
                EdgeDefinitions = new List<EdgeDefinition>()
            });
            var response = await _client.PostEdgeDefinitionAsync(
                tempGraph,
                new PostEdgeDefinitionBody
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
            Assert.Empty(response.Graph.OrphanCollections);
        }

        [Fact]
        public async Task PostEdgeDefinitionAsync_ShouldThrow_WhenGraphNotFound()
        {
            var exception = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.PostEdgeDefinitionAsync(
                    "boggus_graph",
                    new PostEdgeDefinitionBody
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
                    Type = CollectionType.Edge
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
                        From = new string[] { "FromPutCollection" },
                        To = new string[] { "ToPutCollection" }
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

            var getAfterResponse = await _client.GetEdgeCollectionsAsync(graphName);

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
        public async Task PostEdgeAsync_ShouldSucceed()
        {
            string graphName = nameof(PostEdgeAsync_ShouldSucceed);
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

            var response = await _client.PostEdgeAsync(
                graphName,
                edgeClx,
                new
                {
                    _from = fromResponse._id,
                    _to = toResponse._id,
                    myKey = "myValue"
                },
                new PostEdgeQuery
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
        public async Task PostEdgeAsync_ShouldThrow_WhenGraphNotFound()
        {
            string graphName = nameof(PostEdgeAsync_ShouldThrow_WhenGraphNotFound);

            var exception = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.PostEdgeAsync(graphName, "edgeClx", new
                {
                    myKey = "myValue"
                });
            });

            Assert.Equal(HttpStatusCode.NotFound, exception.ApiError.Code);
            Assert.Equal(1924, exception.ApiError.ErrorNum); // GRAPH_NOT_FOUND
        }

        [Fact]
        public async Task DeleteEdgeAsync_ShouldSucceed()
        {
            string graphName = nameof(DeleteEdgeAsync_ShouldSucceed);
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

            // Create the edges

            var createEdgeResponse = await _client.PostEdgeAsync(
                graphName,
                edgeClx,
                new
                {
                    _from = fromResponse._id,
                    _to = toResponse._id,
                    myKey = "myValue"
                },
                new PostEdgeQuery
                {
                    ReturnNew = true,
                    WaitForSync = true
                });

            var createEdgeResponse2 = await _client.PostEdgeAsync(
                graphName,
                edgeClx,
                new
                {
                    _from = fromResponse._id,
                    _to = toResponse._id,
                    myKey = "myValue"
                },
                new PostEdgeQuery
                {
                    ReturnNew = true,
                    WaitForSync = true
                });

            // Delete edge with document ID

            DeleteEdgeResponse<DeleteGraphEdgeMockModel> response =
                await _client.DeleteEdgeAsync<DeleteGraphEdgeMockModel>(
                    graphName,
                    createEdgeResponse.Edge._id,
                    new DeleteEdgeQuery
                    {
                        ReturnOld = true,
                        WaitForSync = true
                    });

            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.Equal(createEdgeResponse.New.myKey, response.Old.myKey);
            Assert.True(response.Removed);
            Assert.False(response.Error);

            // Delete edge with collection name and key

            response = await _client.DeleteEdgeAsync<DeleteGraphEdgeMockModel>(
                   graphName,
                   edgeClx,
                   createEdgeResponse2.Edge._key,
                   new DeleteEdgeQuery
                   {
                       ReturnOld = true,
                       WaitForSync = true
                   });

            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.Equal(createEdgeResponse2.New.myKey, response.Old.myKey);
            Assert.True(response.Removed);
            Assert.False(response.Error);
        }

        [Fact]
        public async Task DeleteEdgeAsync_ShouldThrow_WhenGraphNotFound()
        {
            string graphName = nameof(DeleteEdgeAsync_ShouldThrow_WhenGraphNotFound);

            var exception = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.DeleteEdgeAsync<object>(graphName, "edgeClx", "");
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

            var response = await _client.GetVertexAsync<GetVertexMockModel>(
                graphName,
                createVtxResponse.Vertex._id);

            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.False(response.Error);
            Assert.NotNull(response.Vertex);
            Assert.Equal(clxToAdd + "_vtx", response.Vertex.Name);
            Assert.Equal(createVtxResponse.Vertex._key, response.Vertex._key);
            Assert.Equal(createVtxResponse.Vertex._id, response.Vertex._id);

            response = await _client.GetVertexAsync<GetVertexMockModel>(
                graphName,
                clxToAdd,
                createVtxResponse.Vertex._key);

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
                await _client.GetVertexAsync<GetVertexMockModel>(graphName, vertex, "12456");
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

        [Fact]
        public async Task DeleteVertexAsync_ShouldSucceed()
        {
            // Create a new graph

            string graphName = nameof(DeleteVertexAsync_ShouldSucceed);

            PostGraphResponse createResponse = await _client.PostGraphAsync(
                new PostGraphBody()
                {
                    Name = graphName
                });

            // Add a vertex collection

            string clxToAdd = nameof(DeleteVertexAsync_ShouldSucceed);

            PostVertexCollectionResponse createvertexClxresponse = await _client.PostVertexCollectionAsync(
                graphName,
                new PostVertexCollectionBody()
                {
                    Collection = clxToAdd
                });

            // Create vertex

            var vertexProperty = clxToAdd + "_vtx";

            var createVtxResponse = await _client.PostVertexAsync(graphName, clxToAdd, new
            {
                Name = vertexProperty
            });

            Assert.Equal(HttpStatusCode.Accepted, createVtxResponse.Code);

            var createVtxResponse2 = await _client.PostVertexAsync(graphName, clxToAdd, new
            {
                Name = vertexProperty
            });

            Assert.Equal(HttpStatusCode.Accepted, createVtxResponse2.Code);

            var response = await _client.DeleteVertexAsync<DeleteVertexMockModel>(
                graphName,
                createVtxResponse.Vertex._id,
                new DeleteVertexQuery
                {
                    ReturnOld = true,
                    WaitForSync = true
                });

            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.False(response.Error);
            Assert.True(response.Removed);
            Assert.Equal(vertexProperty, response.Old.Name);

            response = await _client.DeleteVertexAsync<DeleteVertexMockModel>(
                graphName,
                clxToAdd,
                createVtxResponse2.Vertex._key,
                new DeleteVertexQuery
                {
                    ReturnOld = true,
                    WaitForSync = true
                });

            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.False(response.Error);
            Assert.True(response.Removed);
            Assert.Equal(vertexProperty, response.Old.Name);
        }

        [Fact]
        public async Task DeleteVertexAsync_ShouldThrow_WhenGraphIsNotFound()
        {
            string graphName = nameof(DeleteVertexAsync_ShouldThrow_WhenGraphIsNotFound);
            string vertex = nameof(DeleteVertexAsync_ShouldThrow_WhenGraphIsNotFound) + "_vtx";

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.DeleteVertexAsync<object>(graphName, vertex, "12345");
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.Equal(1924, ex.ApiError.ErrorNum); // ERROR_GRAPH_NOT_FOUND
        }

        [Fact]
        public async Task DeleteVertexAsync_ShouldThrow_WhenVertexCollectionIsNotFound()
        {
            string graphName = nameof(DeleteVertexAsync_ShouldThrow_WhenVertexCollectionIsNotFound);

            PostGraphResponse createResponse = await _client.PostGraphAsync(
                new PostGraphBody()
                {
                    Name = graphName
                });

            string vertex = nameof(DeleteVertexAsync_ShouldThrow_WhenVertexCollectionIsNotFound) + "_vtx";

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.DeleteVertexAsync<object>(graphName, vertex, "12345");
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.Equal(1203, ex.ApiError.ErrorNum); // ARANGO_DATA_SOURCE_NOT_FOUND
        }

        [Fact]
        public async Task DeleteVertexAsync_ShouldThrow_WhenVertexIsNotFound()
        {
            string graphName = nameof(DeleteVertexAsync_ShouldThrow_WhenVertexIsNotFound);

            PostGraphResponse createResponse = await _client.PostGraphAsync(
                new PostGraphBody()
                {
                    Name = graphName
                });

            string vertexClx = nameof(DeleteVertexAsync_ShouldThrow_WhenVertexIsNotFound) + "_vtxClx";

            PostVertexCollectionResponse createvertexClxresponse = await _client.PostVertexCollectionAsync(
                graphName,
                new PostVertexCollectionBody()
                {
                    Collection = vertexClx
                });

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.DeleteVertexAsync<object>(graphName, vertexClx, "12345");
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.Equal(1202, ex.ApiError.ErrorNum); // ARANGO_DOCUMENT_NOT_FOUND
        }

        [Fact]
        public async Task PatchVertexAsync_ShouldSucceed()
        {
            // Create a new graph

            string graphName = nameof(PatchVertexAsync_ShouldSucceed);

            var createGraphResponse = await _client.PostGraphAsync(
                new PostGraphBody()
                {
                    Name = graphName
                });

            // Add a vertex collection

            string clxToAdd = nameof(PatchVertexAsync_ShouldSucceed);

            var createVtxClxResponse = await _client.PostVertexCollectionAsync(
                graphName,
                new PostVertexCollectionBody()
                {
                    Collection = clxToAdd
                });

            var createVtxResponse = await _client.PostVertexAsync(graphName, clxToAdd, new
            {
                Name = clxToAdd + "_vtx",
                Value = "myValue"
            }, new PostVertexQuery
            {
                ReturnNew = true,
                WaitForSync = true
            });

            // Patch with document ID

            var response = await _client.PatchVertexAsync<dynamic, PatchVertexMockModel>(
                graphName,
                createVtxResponse.Vertex._id,
                new
                {
                    Name = clxToAdd + "_vtx_2"
                },
                new PatchVertexQuery
                {
                    ReturnNew = true,
                    ReturnOld = true,
                    WaitForSync = true
                });

            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.False(response.Error);
            Assert.NotNull(response.Vertex);
            Assert.NotEqual(createVtxResponse.Vertex._rev, response.Vertex._rev);
            Assert.NotEqual(createVtxResponse.Vertex._rev, response.New._rev);
            Assert.NotEqual(createVtxResponse.New.Name, response.New.Name);
            Assert.Equal(createVtxResponse.New.Value, response.New.Value);

            // Patch with collection name and document key

            response = await _client.PatchVertexAsync<dynamic, PatchVertexMockModel>(
                graphName,
                clxToAdd,
                createVtxResponse.Vertex._key,
                new
                {
                    Name = clxToAdd + "_vtx_3"
                },
                new PatchVertexQuery
                {
                    ReturnNew = true,
                    ReturnOld = true,
                    WaitForSync = true
                });

            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.False(response.Error);
            Assert.NotNull(response.Vertex);
            Assert.NotEqual(createVtxResponse.Vertex._rev, response.Vertex._rev);
            Assert.NotEqual(createVtxResponse.Vertex._rev, response.New._rev);
            Assert.NotEqual(createVtxResponse.New.Name, response.New.Name);
            Assert.Equal(createVtxResponse.New.Value, response.New.Value);
        }

        [Fact]
        public async Task PatchVertexAsync_ShouldThrow_WhenGraphIsNotFound()
        {
            string graphName = nameof(PatchVertexAsync_ShouldThrow_WhenGraphIsNotFound);
            string vertex = nameof(PatchVertexAsync_ShouldThrow_WhenGraphIsNotFound) + "_vtx";

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.PatchVertexAsync<dynamic, PatchVertexMockModel>(graphName, vertex, "12345", new { });
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.Equal(1924, ex.ApiError.ErrorNum); // ERROR_GRAPH_NOT_FOUND
        }

        [Fact]
        public async Task PatchVertexAsync_ShouldThrow_WhenVertexCollectionIsNotFound()
        {
            // Create a new graph
            string graphName = nameof(PatchVertexAsync_ShouldThrow_WhenVertexCollectionIsNotFound);

            PostGraphResponse createResponse = await _client.PostGraphAsync(
                new PostGraphBody()
                {
                    Name = graphName
                });
            string vertex = nameof(PatchVertexAsync_ShouldThrow_WhenVertexCollectionIsNotFound) + "_vtx";

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.PatchVertexAsync<dynamic, PatchVertexMockModel>(graphName, vertex, "12345", new { });
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.Equal(1203, ex.ApiError.ErrorNum); // ARANGO_DATA_SOURCE_NOT_FOUND
        }

        [Fact]
        public async Task PatchVertexAsync_ShouldThrow_WhenVertexIsNotFound()
        {
            // Create a new graph
            string graphName = nameof(PatchVertexAsync_ShouldThrow_WhenVertexIsNotFound);

            PostGraphResponse createResponse = await _client.PostGraphAsync(
                new PostGraphBody()
                {
                    Name = graphName
                });
            string vertexClx = nameof(PatchVertexAsync_ShouldThrow_WhenVertexIsNotFound) + "_vtxClx";

            PostVertexCollectionResponse createvertexClxresponse = await _client.PostVertexCollectionAsync(
                graphName,
                new PostVertexCollectionBody()
                {
                    Collection = vertexClx
                });

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.PatchVertexAsync<dynamic, PatchVertexMockModel>(graphName, vertexClx, "12345", new { });
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.Equal(1202, ex.ApiError.ErrorNum); // ARANGO_DOCUMENT_NOT_FOUND
        }

        [Fact]
        public async Task PutGraphEdgeAsync_ShouldSucceed()
        {
            string graphName = nameof(PutGraphEdgeAsync_ShouldSucceed);
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

            var createEdgeResponse = await _client.PostEdgeAsync(
                graphName,
                edgeClx,
                new
                {
                    _from = fromResponse._id,
                    _to = toResponse._id,
                    myKey = "myValue"
                },
                new PostEdgeQuery
                {
                    ReturnNew = true,
                    WaitForSync = true
                });

            // Put with document ID

            var response = await _client.PutEdgeAsync(
                graphName,
                createEdgeResponse.Edge._id,
                new
                {
                    _from = fromResponse._id,
                    _to = toResponse._id,
                    myKey = "newValue"
                },
                new PutEdgeQuery
                {
                    ReturnNew = true,
                    ReturnOld = true,
                    WaitForSync = true
                });

            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.Equal(createEdgeResponse.New.myKey, response.Old.myKey);
            Assert.NotEqual(createEdgeResponse.New.myKey, response.New.myKey);
            Assert.False(response.Error);
            Assert.NotEqual(response.Edge._rev, createEdgeResponse.Edge._rev);

            string previousValue = response.New.myKey;

            // Put with collection name and document key

            response = await _client.PutEdgeAsync(
                graphName,
                edgeClx,
                createEdgeResponse.Edge._key,
                new
                {
                    _from = fromResponse._id,
                    _to = toResponse._id,
                    myKey = "newValue2"
                },
                new PutEdgeQuery
                {
                    ReturnNew = true,
                    ReturnOld = true,
                    WaitForSync = true
                });

            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.Equal(previousValue, response.Old.myKey);
            Assert.NotEqual(createEdgeResponse.New.myKey, response.New.myKey);
            Assert.False(response.Error);
            Assert.NotEqual(response.Edge._rev, createEdgeResponse.Edge._rev);
        }

        [Fact]
        public async Task PuGraphEdgeAsync_ShouldThrow_WhenGraphNotFound()
        {
            string graphName = nameof(PuGraphEdgeAsync_ShouldThrow_WhenGraphNotFound);

            var exception = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.PutEdgeAsync(graphName, "edgeClx", "", new
                {
                    myKey = "myValue"
                });
            });

            Assert.Equal(HttpStatusCode.NotFound, exception.ApiError.Code);
            Assert.Equal(1924, exception.ApiError.ErrorNum); // GRAPH_NOT_FOUND
        }

        [Fact]
        public async Task PutEdgeDefinitionAsync_ShouldSucceed()
        {
            var graphClient = _fixture.PutEdgeDefinitionAsync_ShouldSucceed_ArangoDBClient.Graph;
            string edgeClx = nameof(PutEdgeDefinitionAsync_ShouldSucceed);
            var response = await graphClient.PutEdgeDefinitionAsync(
                _fixture.TestGraph,
                edgeClx,
                new PutEdgeDefinitionBody
                {
                    Collection = edgeClx,
                    // (update is to swap the direction of from and to)
                    To = new string[] { "fromclx" },
                    From = new string[] { "toclx" }
                });

            Assert.Equal(HttpStatusCode.Accepted, response.Code);
            Assert.False(response.Error);

            var newEdgeDef = response.Graph.EdgeDefinitions.FirstOrDefault();
            string afterFromDefinition = newEdgeDef.From.FirstOrDefault();
            string afterToDefinition = newEdgeDef.To.FirstOrDefault();
            Assert.NotEqual("fromclx", afterFromDefinition);
            Assert.NotEqual("toclx", afterToDefinition);
        }

        [Fact]
        public async Task PutEdgeDefinitionAsync_ShouldThrow_WhenGraphNameDoesNotExist()
        {
            var edgeClx = nameof(PutEdgeDefinitionAsync_ShouldThrow_WhenGraphNameDoesNotExist) + "_edgeClx";
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.PutEdgeDefinitionAsync("bogus_collection", edgeClx, new PutEdgeDefinitionBody
                {
                    Collection = edgeClx,
                    To = new string[] { "ToClx" },
                    From = new string[] { "FromClx" }
                }, new PutEdgeDefinitionQuery
                {
                    WaitForSync = false
                });
            });

            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.Equal(1924, ex.ApiError.ErrorNum); // GRAPH_NOT_FOUND
        }

        [Fact]
        public async Task PutEdgeDefinitionAsync_ShouldThrow_WhenEdgeCollectionNameDoesNotExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.PutEdgeDefinitionAsync(_fixture.TestGraph, "bogus_edgeclx", new PutEdgeDefinitionBody
                {
                    Collection = "bogus_edgeclx",
                    To = new string[] { "ToClx" },
                    From = new string[] { "FromClx" }
                }, new PutEdgeDefinitionQuery
                {
                    WaitForSync = false
                });
            });

            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.Equal(1930, ex.ApiError.ErrorNum); // GRAPH_EDGE_COLLECTION_NOT_USED
        }

        [Fact]
        public async Task GetEdgeAsync_ShouldSucceed()
        {
            string graphName = nameof(GetEdgeAsync_ShouldSucceed);
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

            var createdEdgeResponse = await _client.PostEdgeAsync(
                graphName,
                edgeClx,
                new
                {
                    _from = fromResponse._id,
                    _to = toResponse._id,
                    myKey = "myValue"
                },
                new PostEdgeQuery
                {
                    ReturnNew = true,
                    WaitForSync = true
                });

            // Get the edge with collection name and _key

            var response = await _client.GetEdgeAsync<Newtonsoft.Json.Linq.JObject>(
                graphName,
                edgeClx,
                createdEdgeResponse.Edge._key);

            Assert.NotNull(response.Edge);
            Assert.Equal("myValue", response.Edge["myKey"].ToString());

            // Get the edge with document-handle

            response = await _client.GetEdgeAsync<Newtonsoft.Json.Linq.JObject>(
                graphName,
                createdEdgeResponse.Edge._id);

            Assert.NotNull(response.Edge);
            Assert.Equal("myValue", response.Edge["myKey"].ToString());
        }

        [Fact]
        public async Task GetEdgeAsync_ShouldThrow_WhenEdgeWithRevisionIsNotFound()
        {
            string graphName = nameof(GetEdgeAsync_ShouldThrow_WhenEdgeWithRevisionIsNotFound);
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

            var createdEdgeResponse = await _client.PostEdgeAsync(
                graphName,
                edgeClx,
                new
                {
                    _from = fromResponse._id,
                    _to = toResponse._id,
                    myKey = "myValue"
                },
                new PostEdgeQuery
                {
                    ReturnNew = true,
                    WaitForSync = true
                });

            // Get the edge with a non-existing revision

            var exception = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.GetEdgeAsync<Newtonsoft.Json.Linq.JObject>(
                graphName,
                edgeClx,
                createdEdgeResponse.Edge._key,
                new GetEdgeQuery()
                {
                    Rev = "RevisionThatDoesNotExist"
                });
            });

            Assert.Equal(HttpStatusCode.PreconditionFailed, exception.ApiError.Code);
            Assert.Equal(1200, exception.ApiError.ErrorNum); // ERROR_ARANGO_CONFLICT
        }

        [Fact]
        public async Task GetEdgeAsync_ShouldThrow_WhenGraphIsNotFound()
        {
            var exception = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.GetEdgeAsync<Newtonsoft.Json.Linq.JObject>(
                    nameof(GetEdgeAsync_ShouldThrow_WhenGraphIsNotFound),
                    "edgeClx",
                    "0123456789");
            });

            Assert.Equal(HttpStatusCode.NotFound, exception.ApiError.Code);
            Assert.Equal(1924, exception.ApiError.ErrorNum); // GRAPH_NOT_FOUND
        }

        [Fact]
        public async Task PatchEdgeAsync_ShouldSucceed()
        {
            string graphName = nameof(PatchEdgeAsync_ShouldSucceed);
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

            var createEdgeResponse = await _client.PostEdgeAsync(
                graphName,
                edgeClx,
                new
                {
                    _from = fromResponse._id,
                    _to = toResponse._id,
                    myKey = "myValue",
                    value = 1
                },
                new PostEdgeQuery
                {
                    ReturnNew = true,
                    WaitForSync = true
                });

            // Patch with document ID

            var response = await _client.PatchEdgeAsync<object, PatchEdgeMockModel>(
                graphName,
                createEdgeResponse.Edge._id,
                new
                {
                    _from = fromResponse._id,
                    _to = toResponse._id,
                    myKey = "newValue"
                }, new PatchEdgeQuery
                {
                    ReturnNew = true,
                    ReturnOld = true,
                    WaitForSync = true
                });

            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.Equal(createEdgeResponse.New.myKey, response.Old.myKey);
            Assert.NotEqual(createEdgeResponse.New.myKey, response.New.myKey);
            Assert.False(response.Error);
            Assert.NotEqual(createEdgeResponse.Edge._rev, response.Edge._rev);
            Assert.Equal(createEdgeResponse.New.value, response.New.value);
            Assert.Equal(createEdgeResponse.New.value, response.Old.value);

            string previousValue = response.New.myKey;

            // Patch with collection name and document key

            response = await _client.PatchEdgeAsync<object, PatchEdgeMockModel>(
                graphName,
                edgeClx,
                createEdgeResponse.Edge._key,
                new
                {
                    _from = fromResponse._id,
                    _to = toResponse._id,
                    myKey = "newValue2"
                }, new PatchEdgeQuery
                {
                    ReturnNew = true,
                    ReturnOld = true,
                    WaitForSync = true
                });

            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.Equal(previousValue, response.Old.myKey);
            Assert.NotEqual(createEdgeResponse.New.myKey, response.New.myKey);
            Assert.False(response.Error);
            Assert.NotEqual(createEdgeResponse.Edge._rev, response.Edge._rev);
            Assert.Equal(createEdgeResponse.New.value, response.New.value);
            Assert.Equal(createEdgeResponse.New.value, response.Old.value);
        }

        [Fact]
        public async Task PatchEdgeAsync_ShouldThrow_WhenGraphNotFound()
        {
            string graphName = nameof(PatchEdgeAsync_ShouldThrow_WhenGraphNotFound);

            var exception = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.PatchEdgeAsync<object, PatchEdgeMockModel>(graphName, "edgeClx", "", new { });
            });

            Assert.Equal(HttpStatusCode.NotFound, exception.ApiError.Code);
            Assert.Equal(1924, exception.ApiError.ErrorNum); // GRAPH_NOT_FOUND
        }

        [Fact]
        public async Task PutVertexAsync_ShouldSuceed()
        {
            string graphName = nameof(PutVertexAsync_ShouldSuceed) + "_graph";

            PostGraphResponse createGraphResponse = await _client.PostGraphAsync(
                new PostGraphBody()
                {
                    Name = graphName
                });

            Assert.Equal(HttpStatusCode.Accepted, createGraphResponse.Code);

            string vertexClx = nameof(PutVertexAsync_ShouldSuceed) + "_clx";

            PostVertexCollectionResponse createVertexClxResponse = await _client.PostVertexCollectionAsync(
                graphName,
                new PostVertexCollectionBody()
                {
                    Collection = vertexClx
                });

            string initialValue = vertexClx + "_vtx";
            string putValue = vertexClx + "_vtx_2";

            var createVertexResponse = await _client.PostVertexAsync(graphName, vertexClx, new
            {
                Name = initialValue
            });

            // Put with document ID

            var response = await _client.PutVertexAsync(
                graphName,
                createVertexResponse.Vertex._id,
                new PutVertexMockModel
                {
                    Name = putValue
                },
                new PutVertexQuery
                {
                    ReturnNew = true,
                    ReturnOld = true,
                    WaitForSync = true
                });

            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.False(response.Error);
            Assert.Equal(putValue, response.New.Name);
            Assert.NotEqual(response.New.Name, response.Old.Name);
            Assert.NotEqual(createVertexResponse.Vertex._rev, response.Vertex._rev);

            // Put with collection name and document key

            putValue = vertexClx + "_vtx_3";

            response = await _client.PutVertexAsync(
                graphName,
                vertexClx,
                createVertexResponse.Vertex._key,
                new PutVertexMockModel
                {
                    Name = putValue
                },
                new PutVertexQuery
                {
                    ReturnNew = true,
                    ReturnOld = true,
                    WaitForSync = true
                });

            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.False(response.Error);
            Assert.Equal(putValue, response.New.Name);
            Assert.NotEqual(response.New.Name, response.Old.Name);
            Assert.NotEqual(createVertexResponse.Vertex._rev, response.Vertex._rev);
        }

        [Fact]
        public async Task PutVertexAsync_ShouldThrow_WhenGraphIsNotFound()
        {
            string vertexClx = nameof(PutVertexAsync_ShouldThrow_WhenGraphIsNotFound);
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.PutVertexAsync("bogusGraph", vertexClx, "", new PutVertexMockModel
                {
                    Name = "Bogus_Name"
                });
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.Equal(1924, ex.ApiError.ErrorNum); // ERROR_GRAPH_NOT_FOUND
        }

        [Fact]
        public async Task PutVertexAsync_ShouldThrow_WhenVertexCollectionIsNotFound()
        {
            string graphName = nameof(PutVertexAsync_ShouldThrow_WhenVertexCollectionIsNotFound);
            string vertexClx = nameof(PutVertexAsync_ShouldThrow_WhenVertexCollectionIsNotFound);

            await _client.PostGraphAsync(new PostGraphBody { Name = graphName });

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.PutVertexAsync(graphName, vertexClx, "12345", new PutVertexMockModel
                {
                    Name = "Bogus_Name"
                });
            });
            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.Equal(1203, ex.ApiError.ErrorNum); // ARANGO_DATA_SOURCE_NOT_FOUND
        }

        [Fact]
        public async Task PutVertexAsync_ShouldThrow_WhenVertexIsNotFound()
        {
            string graphName = nameof(PutVertexAsync_ShouldThrow_WhenGraphIsNotFound);
            string vertexClx = nameof(PutVertexAsync_ShouldThrow_WhenGraphIsNotFound);

            await _client.PostGraphAsync(new PostGraphBody { Name = graphName });
            await _client.PostVertexCollectionAsync(graphName, new PostVertexCollectionBody
            {
                Collection = vertexClx
            });


            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _client.PutVertexAsync(graphName, vertexClx, "123456", new PutVertexMockModel
                {
                    Name = "Bogus_Name"
                });
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.Equal(1202, ex.ApiError.ErrorNum); // ARANGO_DOCUMENT_NOT_FOUND
        }
    }
}
