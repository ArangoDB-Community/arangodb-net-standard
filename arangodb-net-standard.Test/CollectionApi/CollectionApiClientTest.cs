using System;
using System.Collections.Generic;
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

        public CollectionApiClientTest(CollectionApiClientTestFixture fixture)
        {
            _collectionApi = fixture.ArangoDBClient.Collection;
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
    }
}
