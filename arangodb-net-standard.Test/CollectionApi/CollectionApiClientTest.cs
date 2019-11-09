using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi;
using Xunit;

namespace ArangoDBNetStandardTest.CollectionApi
{
    public class CollectionApiClientTest : IClassFixture<CollectionApiClientTestFixture>
    {
        private CollectionApiClient _collectionApi;
        private ArangoDBClient _adb;
        private string _testCollection;

        public CollectionApiClientTest(CollectionApiClientTestFixture fixture)
        {
            _adb = fixture.ArangoDBClient;
            _collectionApi = fixture.ArangoDBClient.Collection;
            _testCollection = fixture.TestCollection;

            // Truncate TestCollection before each test
            _collectionApi.TruncateCollectionAsync(fixture.TestCollection)
                .GetAwaiter()
                .GetResult();
        }

        [Fact]
        public async Task PostCollectionAsync_ShouldSucceed()
        {
            var response = await _collectionApi.PostCollectionAsync(
                new PostCollectionRequest
                {
                    Name = "MyCollection"
                });

            Assert.False(response.Error);
            Assert.NotNull(response.Id);
            Assert.Equal("MyCollection", response.Name);
            Assert.Equal("traditional", response.KeyOptions.Type);
            Assert.Equal(2, response.Type); // 2 is document collection, 3 is edge collection
        }

        [Fact]
        public async Task PostCollectionAsync_ShouldSucceed_WhenUsingKeyOptions()
        {
            var response = await _collectionApi.PostCollectionAsync(
                new PostCollectionRequest
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
            Assert.Equal(2, response.Type); // 2 is document collection, 3 is edge collection
        }


        [Fact]
        public async Task PostCollectionAsync_ShouldSucceed_WhenEdgeCollection()
        {
            var response = await _collectionApi.PostCollectionAsync(
                new PostCollectionRequest
                {
                    Name = "MyEdgeCollection",
                    Type = 3
                });

            Assert.False(response.Error);
            Assert.NotNull(response.Id);
            Assert.Equal("MyEdgeCollection", response.Name);
            Assert.Equal("traditional", response.KeyOptions.Type);
            Assert.Equal(3, response.Type); // 2 is document collection, 3 is edge collection
        }

        [Fact]
        public async Task PostCollectionAsync_ShouldThrow_WhenCollectionNameExists()
        {
            var request = new PostCollectionRequest
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
            var request = new PostCollectionRequest
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
            Assert.Equal(2, result.Type);
            Assert.Equal(3, result.Status);
            Assert.False(result.IsSystem);
            Assert.Equal(_testCollection, result.Name);
        }

        [Fact]
        public async Task TruncateCollectionAsync_ShouldThrow_WhenCollectionDoesNotExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                await _collectionApi.TruncateCollectionAsync("NotACollection"));

            Assert.Equal(ex.ApiError.ErrorNum, 1203);
        }
    }
}
