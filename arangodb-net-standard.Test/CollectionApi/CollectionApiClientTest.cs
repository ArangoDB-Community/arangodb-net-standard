using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi;
using ArangoDBNetStandard.CollectionApi.Models;
using ArangoDBNetStandard.DocumentApi.Models;
using ArangoDBNetStandard.Transport;
using Moq;
using Xunit;

namespace ArangoDBNetStandardTest.CollectionApi
{
    public class CollectionApiClientTest : IClassFixture<CollectionApiClientTestFixture>, IAsyncLifetime
    {
        private CollectionApiClient _collectionApi;
        private ArangoDBClient _adb;
        private readonly string _testCollection;

        public CollectionApiClientTest(CollectionApiClientTestFixture fixture)
        {
            _adb = fixture.ArangoDBClient;
            _collectionApi = _adb.Collection;
            _testCollection = fixture.TestCollection;
        }

        public async Task InitializeAsync()
        {
            // Truncate TestCollection before each test
            await _collectionApi.TruncateCollectionAsync(_testCollection);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        [Fact]
        public async Task DeleteCollectionAsync_ShouldSucceed()
        {
            string clx = "DeleteCollectionAsync_ShouldSucceed";

            // create a collection so we can delete it
            var createResponse = await _collectionApi.PostCollectionAsync(
                new PostCollectionBody
                {
                    Name = clx
                });
            string clxId = createResponse.Id;
            Assert.False(createResponse.Error);
            Assert.NotNull(clxId);

            var deleteResponse = await _collectionApi.DeleteCollectionAsync(clx);
            Assert.False(deleteResponse.Error);
            Assert.Equal(clxId, deleteResponse.Id);
        }

        [Fact]
        public async Task DeleteCollectionAsync_ShouldThrow_WhenCollectionDoesNotExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                await _collectionApi.DeleteCollectionAsync("NotACollection"));
            Assert.Equal(1203, ex.ApiError.ErrorNum);
        }

        [Fact]
        public async Task PostCollectionAsync_ShouldSucceed()
        {
            var response = await _collectionApi.PostCollectionAsync(
                new PostCollectionBody
                {
                    Name = "MyCollection"
                });

            Assert.False(response.Error);
            Assert.NotNull(response.Id);
            Assert.Equal("MyCollection", response.Name);
            Assert.Equal("traditional", response.KeyOptions.Type);
            Assert.Equal(CollectionType.Document, response.Type);
        }

        [Fact]
        public async Task PostCollectionAsync_ShouldSucceed_WhenUsingKeyOptions()
        {
            var response = await _collectionApi.PostCollectionAsync(
                new PostCollectionBody
                {
                    Name = "MyCollectionWithKeyOptions",
                    KeyOptions = new CollectionKeyOptions
                    {
                        AllowUserKeys = false,
                        Increment = 5,
                        Offset = 1,
                        Type = "autoincrement"
                    }
                });

            Assert.False(response.Error);
            Assert.NotNull(response.Id);
            Assert.Equal("MyCollectionWithKeyOptions", response.Name);
            Assert.Equal("autoincrement", response.KeyOptions.Type);
            Assert.False(response.KeyOptions.AllowUserKeys);
            Assert.Equal(CollectionType.Document, response.Type); // 2 is document collection, 3 is edge collection
        }

        [Fact]
        public async Task PostCollectionAsync_ShouldSucceed_WhenEdgeCollection()
        {
            var response = await _collectionApi.PostCollectionAsync(
                new PostCollectionBody
                {
                    Name = nameof(PostCollectionAsync_ShouldSucceed_WhenEdgeCollection),
                    Type = CollectionType.Edge
                });

            Assert.False(response.Error);
            Assert.NotNull(response.Id);
            Assert.Equal(
                nameof(PostCollectionAsync_ShouldSucceed_WhenEdgeCollection),
                response.Name);
            Assert.Equal("traditional", response.KeyOptions.Type);
            Assert.Equal(CollectionType.Edge, response.Type); // 2 is document collection, 3 is edge collection
        }

        [Fact]
        public async Task PostCollectionAsync_ShouldSucceed_WhenSharding()
        {
            var response = await _collectionApi.PostCollectionAsync(
                new PostCollectionBody
                {
                    Name = nameof(PostCollectionAsync_ShouldSucceed_WhenSharding),
                    NumberOfShards = 4,
                    ShardKeys = new string[] { "country" },
                    ReplicationFactor = 2
                });

            Assert.False(response.Error);
            Assert.NotNull(response.Id);
        }

        [Fact]
        public async Task PostCollectionAsync_ShouldUseQueryParameter()
        {
            var mockTransport = new Mock<IApiClientTransport>();

            var mockResponse = new Mock<IApiClientResponse>();

            var mockResponseContent = new Mock<IApiClientResponseContent>();

            mockResponse.Setup(x => x.Content)
                .Returns(mockResponseContent.Object);

            mockResponse.Setup(x => x.IsSuccessStatusCode)
                .Returns(true);

            string requestUri = null;

            mockTransport.Setup(x => x.PostAsync(
                It.IsAny<string>(),
                It.IsAny<byte[]>(),
                It.IsAny<WebHeaderCollection>()))
                .Returns((string uri, byte[] content, WebHeaderCollection webHeaderCollection) =>
                {
                    requestUri = uri;
                    return Task.FromResult(mockResponse.Object);
                });

            var apiClient = new CollectionApiClient(mockTransport.Object);

            var response = await apiClient.PostCollectionAsync(
                new PostCollectionBody
                {
                    Name = "MyCollection"
                },
                new PostCollectionQuery()
                {
                    EnforceReplicationFactor = true,
                    WaitForSyncReplication = true
                });

            Assert.NotNull(requestUri);
            Assert.Contains("enforceReplicationFactor=1", requestUri);
            Assert.Contains("waitForSyncReplication=1", requestUri);
        }

        [Fact]
        public async Task PostCollectionAsync_ShouldThrow_WhenCollectionNameExists()
        {
            var request = new PostCollectionBody
            {
                Name = "MyOneAndOnlyCollection"
            };
            await _collectionApi.PostCollectionAsync(request);
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _collectionApi.PostCollectionAsync(request);
            });
            Assert.Equal(1207, ex.ApiError.ErrorNum);
        }

        [Fact]
        public async Task PostCollectionAsync_ShouldThrow_WhenCollectionNameInvalid()
        {
            var request = new PostCollectionBody
            {
                Name = "My collection name with spaces"
            };
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _collectionApi.PostCollectionAsync(request);
            });
            Assert.Equal(1208, ex.ApiError.ErrorNum);
        }

        [Fact]
        public async Task TruncateCollectionAsync_ShouldSucceed()
        {
            // add a document
            var response = await _adb.Document.PostDocumentAsync<object>(
                _testCollection,
                new { test = 123 });
            Assert.NotNull(response._id);

            // truncate collection
            var result = await _collectionApi.TruncateCollectionAsync(_testCollection);

            // count documents in collection, should be zero
            int count = (await _adb.Cursor.PostCursorAsync<int>(
                query: "RETURN COUNT(@@clx)",
                bindVars: new Dictionary<string, object> { ["@clx"] = _testCollection }))
                .Result
                .First();

            Assert.Equal(0, count);

            Assert.Equal(HttpStatusCode.OK, result.Code);
            Assert.False(result.Error);
            Assert.NotNull(result.Id);
            Assert.NotNull(result.GloballyUniqueId);
            Assert.Equal(CollectionType.Document, result.Type);
            Assert.Equal(3, result.Status);
            Assert.False(result.IsSystem);
            Assert.Equal(_testCollection, result.Name);
        }

        [Fact]
        public async Task TruncateCollectionAsync_ShouldThrow_WhenCollectionDoesNotExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                await _collectionApi.TruncateCollectionAsync("NotACollection"));

            Assert.Equal(1203, ex.ApiError.ErrorNum);
        }

        [Fact]
        public async Task GetCollectionCountAsync_ShouldSucceed()
        {
            var newDoc = await _adb.Document.PostDocumentAsync(_testCollection, new PostDocumentsQuery());
            var response = await _collectionApi.GetCollectionCountAsync(_testCollection);

            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.False(response.Error);
            Assert.False(response.IsSystem);
            Assert.Equal(3, response.Status);
            Assert.Equal("loaded", response.StatusString);
            Assert.Equal(CollectionType.Document, response.Type);
            Assert.False(response.WaitForSync);
            Assert.NotNull(response.GloballyUniqueId);
            Assert.NotNull(response.Id);
            Assert.NotNull(response.KeyOptions);
            Assert.False(response.WaitForSync);
            Assert.Equal(1, response.Count);
            await _adb.Document.DeleteDocumentAsync(newDoc._id);
        }

        [Fact]
        public async Task GetCollectionCountAsync_ShouldThrow_WhenCollectionDoesNotExist()
        {
            var exception = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                await _collectionApi.GetCollectionCountAsync("bogusCollection"));
            Assert.Equal(HttpStatusCode.NotFound, exception.ApiError.Code);
        }

        [Fact]
        public async Task GetCollectionsAsync_ShouldSucceed()
        {
            var response = await _collectionApi.GetCollectionsAsync(new GetCollectionsQuery
            {
                ExcludeSystem = true // System adds 9 collections that we don't need to test
            });
            Assert.NotEmpty(response.Result);
            var collectionExists = response.Result.Where(x => x.Name == _testCollection);

            Assert.False(response.Error);
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.NotNull(collectionExists);
        }

        [Fact]
        public async Task GetCollectionAsync_ShouldSucceed()
        {
            var collection = await _collectionApi.GetCollectionAsync(_testCollection);

            Assert.Equal(_testCollection, collection.Name);
        }

        [Fact]
        public async Task GetCollectionAsync_ShouldThrow_WhenNotFound()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _collectionApi.GetCollectionAsync("MyWrongCollection");
            });

            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
        }

        [Fact]
        public async Task GetCollectionPropertiesAsync_ShouldSucceed()
        {
            var response = await _collectionApi.GetCollectionPropertiesAsync(_testCollection);

            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.NotNull(response.KeyOptions);
            Assert.False(response.WaitForSync);
            Assert.Equal(_testCollection, response.Name);
            // ObjectId is null, using ArangoDB 3.4.6 Enterprise Edition for Windows
            // Assert.NotNull(response.ObjectId);
            Assert.False(response.IsSystem);
            Assert.Equal(3, response.Status);
            Assert.Equal(CollectionType.Document, response.Type);
        }

        [Fact]
        public async Task RenameCollectionAsync_ShouldSucceed()
        {
            string initialClx = nameof(RenameCollectionAsync_ShouldSucceed);
            string renamedClx = initialClx + "_Renamed";

            await _adb.Collection.PostCollectionAsync(
                    new PostCollectionBody
                    {
                        Name = initialClx
                    });
            var response = await _collectionApi.RenameCollectionAsync(initialClx, new RenameCollectionBody
            {
                Name = renamedClx
            });
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.Equal(renamedClx, response.Name);
            Assert.False(response.IsSystem);
            Assert.NotNull(response.Id);
            Assert.False(response.Error);
        }

        [Fact]
        public async Task RenameCollectionAsync_ShouldThrow_WhenCollectionNotFound()
        {
            var exception = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _collectionApi.RenameCollectionAsync("bogusCollection", new RenameCollectionBody
                {
                    Name = "testingCollection"
                });
            });
            Assert.Equal(HttpStatusCode.NotFound, exception.ApiError.Code);
            Assert.Equal(1203, exception.ApiError.ErrorNum); // ARANGO_DATA_SOURCE_NOT_FOUND
        }

        [Fact]
        public async Task RenameCollectionAsync_ShouldThrow_WhenNameInvalid()
        {
            var exception = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _collectionApi.RenameCollectionAsync(_testCollection, new RenameCollectionBody
                {
                    Name = "Bad Collection Name"
                });
            });
            Assert.Equal(1208, exception.ApiError.ErrorNum); // Arango Illegal Name
        }

        [Fact]
        public async Task RenameCollectionAsync_ShouldThrow_WhenCollectionInvalid()
        {
            var exception = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _collectionApi.RenameCollectionAsync("Bad Collection Name", new RenameCollectionBody
                {
                    Name = "testingCollection"
                });
            });
            Assert.Equal(1203, exception.ApiError.ErrorNum); // Arango Data Source Not Found
        }

        [Fact]
        public async Task GetCollectionRevisionAsync_ShouldSucceed()
        {
            var response = await _collectionApi.GetCollectionRevisionAsync(_testCollection);
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.Equal(_testCollection, response.Name);
            Assert.NotNull(response.Id);
            Assert.NotNull(response.KeyOptions);
            Assert.NotNull(response.Revision);
            Assert.NotNull(response.StatusString);
        }

        [Fact]
        public async Task GetCollectionRevisionAsync_ShouldThrow_WhenCollectionNotFound()
        {
            var exception = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _collectionApi.GetCollectionRevisionAsync("bogusCollection");
            });
            Assert.Equal(HttpStatusCode.NotFound, exception.ApiError.Code);
        }

        [Fact]
        public async Task PutCollectionPropertyAsync_ShouldSucceed()
        {
            var putCollection = await _adb.Collection.PostCollectionAsync(new PostCollectionBody
            {
                Name = nameof(PutCollectionPropertyAsync_ShouldSucceed)
            });
            var beforeResponse = await _collectionApi.GetCollectionPropertiesAsync(putCollection.Name);

            var body = new PutCollectionPropertyBody
            {
                WaitForSync = !beforeResponse.WaitForSync
            };
            var response = await _collectionApi.PutCollectionPropertyAsync(putCollection.Name, body);

            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.NotEqual(beforeResponse.WaitForSync, response.WaitForSync);
        }

        [Fact]
        public async Task PutCollectionPropertyAsync_ShouldThrow_WhenCollectionDoesNotExist()
        {
            var body = new PutCollectionPropertyBody
            {
                //JournalSize = 313136,
                WaitForSync = false
            };
            var exception = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _collectionApi.PutCollectionPropertyAsync("bogusCollection", body);
            });

            Assert.Equal(HttpStatusCode.NotFound, exception.ApiError.Code);
            Assert.Equal(1203, exception.ApiError.ErrorNum); // ARANGO_DATA_SOURCE_NOT_FOUND
        }

        [Fact]
        public async Task GetCollectionFiguresAsync_ShouldSucceed()
        {
            var response = await _collectionApi.GetCollectionFiguresAsync(_testCollection);
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.NotNull(response.Figures);
        }

        [Fact]
        public async Task GetCollectionFiguresAsync_ShouldThrow_WhenCollectionNotFound()
        {
            var exception = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _collectionApi.GetCollectionFiguresAsync("bogusCollection");
            });
            Assert.Equal(HttpStatusCode.NotFound, exception.ApiError.Code);
        }




        [Fact]
        public async Task GetChecksumAsync_ShouldSucceed()
        {
            var res = await _collectionApi.GetChecksumAsync(_testCollection);
            Assert.NotNull(res.Checksum);
        }

        [Fact]
        public async Task LoadIndexesIntoMemoryAsync_ShouldSucceed()
        {
            var res = await _collectionApi.PutLoadIndexesIntoMemoryAsync(_testCollection);
            Assert.True(res.Result);
        }

        [Fact]
        public async Task RecalculateCountAsync_ShouldSucceed()
        {
            var res = await _collectionApi.PutRecalculateCountAsync(_testCollection);
            Assert.True(res.Result);
        }

        /// <summary>
        /// This test will run only in a cluster
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("RunningMode", "Cluster")]
        public async Task GetCollectionShardsAsync_ShouldSucceed()
        {
            var res = await _collectionApi.GetCollectionShardsAsync(_testCollection);
            Assert.Equal(HttpStatusCode.OK, res.Code);
        }

        /// <summary>
        /// This test will run only in a cluster
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("RunningMode", "Cluster")]
        public async Task GetCollectionShardsWithDetailsAsync_ShouldSucceed()
        {
            var res = await _collectionApi.GetCollectionShardsWithDetailsAsync(_testCollection);
            Assert.Equal(HttpStatusCode.OK, res.Code);
        }

        [Fact]
        public async Task CompactCollectionDataAsync_ShouldSucceed()
        {
            var res = await _collectionApi.PutCompactCollectionDataAsync(_testCollection);
            Assert.Equal(HttpStatusCode.OK, res.Code);
            Assert.NotNull(res.GloballyUniqueId);
        }




    }
}
