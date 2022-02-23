using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ArangoDBNetStandard;
using ArangoDBNetStandard.IndexApi;
using ArangoDBNetStandard.IndexApi.Models;
using ArangoDBNetStandard.Transport;
using Moq;
using Xunit;

namespace ArangoDBNetStandardTest.IndexApi
{
    public class IndexApiClientTest : IClassFixture<IndexApiClientTestFixture>, IAsyncLifetime
    {
        private IndexApiClient _indexApi;
        private ArangoDBClient _adb;
        private readonly string _testIndexName;
        private readonly string _testIndexId;
        private readonly string _testCollection;

        public IndexApiClientTest(IndexApiClientTestFixture fixture)
        {
            _adb = fixture.ArangoDBClient;
            _indexApi = _adb.Index;
            _testIndexName = fixture.TestIndexName;
            _testIndexId = fixture.TestIndexId;
            _testCollection = fixture.TestCollectionName;
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        [Fact]
        public async Task PostIndexAsync_ShouldSucceed_WhenPersistentIndex()
        {
            string indexName = "NewPersistentIndex";
            var createResponse = await _indexApi.PostIndexAsync(
                 IndexType.Persistent,
                 new PostIndexQuery()
                 {
                     CollectionName = _testCollection,
                 },
                 new PostIndexBody()
                 {
                     Name = indexName,
                     Fields = new string[]
                     {
                          "field1",
                          "field2"
                     },
                     Unique = true
                 });
            string indexId = createResponse.Id;
            Assert.False(createResponse.Error);
            Assert.NotNull(indexId);
            Assert.True(createResponse.IsNewlyCreated);
        }

        [Fact]
        public async Task PostIndexAsync_ShouldSucceed_WhenTTLIndex()
        {
            string indexName = "NewTTLIndex";
            var createResponse = await _indexApi.PostIndexAsync(
                 IndexType.TTL,
                 new PostIndexQuery()
                 {
                     CollectionName = _testCollection,
                 },
                 new PostIndexBody()
                 {
                     Name = indexName,
                     Fields = new string[]
                     {
                          "field1"
                     },
                     ExpireAfter = 3600
                 });
            string indexId = createResponse.Id;
            Assert.False(createResponse.Error);
            Assert.NotNull(indexId);
            Assert.True(createResponse.IsNewlyCreated);
        }

        [Fact]
        public async Task PostIndexAsync_ShouldSucceed_WhenFullTextIndex()
        {
            string indexName = "NewFullTextIndex";
            var createResponse = await _indexApi.PostIndexAsync(
                 IndexType.FullText,
                 new PostIndexQuery()
                 {
                     CollectionName = _testCollection,
                 },
                 new PostIndexBody()
                 {
                     Name = indexName,
                     Fields = new string[]
                     {
                          "field1"
                     }
                 });
            string indexId = createResponse.Id;
            Assert.False(createResponse.Error);
            Assert.NotNull(indexId);
            Assert.True(createResponse.IsNewlyCreated);
        }

        [Fact]
        public async Task PostIndexAsync_ShouldSucceed_WhenGeoIndex()
        {
            string indexName = "NewGeoIndex";
            var createResponse = await _indexApi.PostIndexAsync(
                 IndexType.Geo,
                 new PostIndexQuery()
                 {
                     CollectionName = _testCollection,
                 },
                 new PostIndexBody()
                 {
                     Name = indexName,
                     Fields = new string[]
                     {
                          "field1"
                     },
                     GeoJson = false
                 });
            string indexId = createResponse.Id;
            Assert.False(createResponse.Error);
            Assert.NotNull(indexId);
            Assert.True(createResponse.IsNewlyCreated);
        }

        [Fact]
        public async Task PostIndexAsync_ShouldThrow_WhenIndexNameExists()
        {
            var request = new PostIndexBody()
            {
                Name = "MyOneAndOnlyIndex",
                Fields = new string[]
                     {
                          "field1",
                          "field2"
                     },
                Unique = true
            };
            await _indexApi.PostIndexAsync(IndexType.Persistent, new PostIndexQuery() { CollectionName = _testCollection }, request);
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _indexApi.PostIndexAsync(IndexType.Persistent, new PostIndexQuery() { CollectionName = _testCollection }, request);
            });
            Assert.Equal(1207, ex.ApiError.ErrorNum);
        }

        [Fact]
        public async Task PostIndexAsync_ShouldThrow_WhenIndexNameInvalid()
        {
            var request = new PostIndexBody()
            {
                Name = "My Invalid Index Name",
                Fields = new string[]
                     {
                          "field1",
                          "field2"
                     },
                Unique = true
            };
            await _indexApi.PostIndexAsync(IndexType.Persistent, new PostIndexQuery() { CollectionName = _testCollection }, request);
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _indexApi.PostIndexAsync(IndexType.Persistent, new PostIndexQuery() { CollectionName = _testCollection }, request);
            });
            Assert.Equal(1208, ex.ApiError.ErrorNum);
        }

        [Fact]
        public async Task GetIndexAsync_ShouldSucceed()
        {
            var index = await _indexApi.GetIndexAsync(_testIndexId);
            Assert.NotNull(index);
            Assert.Equal(_testIndexName, index.Name);
        }

        [Fact]
        public async Task GetIndexAsync_ShouldThrow_WhenNotFound()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _indexApi.GetIndexAsync("MyWrongIndexId");
            });
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
        }

        [Fact]
        public async Task GetAllCollectionIndexesAsync_ShouldSucceed()
        {
            var indexRes = await _indexApi.GetAllCollectionIndexesAsync(
                new GetAllCollectionIndexesQuery()
                {
                    CollectionName = _testCollection
                });
            Assert.NotNull(indexRes);
            Assert.False(indexRes.Error);
            Assert.NotEmpty(indexRes.Indexes);
        }

        [Fact]
        public async Task GetAllCollectionIndexesAsync_ShouldThrow_WhenCollectionNotFound()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _indexApi.GetAllCollectionIndexesAsync(
                    new GetAllCollectionIndexesQuery()
                    {
                        CollectionName = "MyNonExistentCollection"
                    });
            });
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
        }

        [Fact]
        public async Task DeleteIndexAsync_ShouldSucceed()
        {
            string indexName = "IndexNameToDelete";
            //Create the index first
            var createResponse = await _indexApi.PostIndexAsync(
                 IndexType.Persistent,
                 new PostIndexQuery()
                 {
                     CollectionName = _testCollection,
                 },
                 new PostIndexBody()
                 {
                     Name = indexName,
                     Fields = new string[]
                     {
                          "field1",
                          "field2"
                     },
                     Unique = true
                 });
            string indexId = createResponse.Id;
            Assert.False(createResponse.Error);
            Assert.NotNull(indexId);

            //delete the new index
            var deleteResponse = await _indexApi.DeleteIndexAsync(indexId);
            Assert.False(deleteResponse.Error);
            Assert.Equal(indexId, deleteResponse.Id);
        }

        [Fact]
        public async Task DeleteIndexAsync_ShouldThrow_WhenIndexDoesNotExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(
                async () => await _indexApi.DeleteIndexAsync("NonExistentIndexId")
                );
            Assert.Equal(1212, ex.ApiError.ErrorNum);
        }



    }
}
