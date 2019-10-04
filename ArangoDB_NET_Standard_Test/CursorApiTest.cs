using ArangoDB_NET_Standard;
using ArangoDB_NET_Standard.CursorApi;
using ArangoDB_NET_Standard.DatabaseApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ArangoDB_NET_Standard_Test
{
    public class CursorApiTest : ApiTestBase
    {
        private readonly string _dbName = nameof(CursorApiTest);
        private CursorApiClient _cursorApi;

        public class MyModel
        {
            public string MyProperty { get; set; }
        }

        public CursorApiTest()
        {
            CreateDatabase(_dbName);
            _cursorApi = GetArangoDBClient(_dbName).Cursor;
        }
        
        [Fact]
        public async Task PostCursorAsync_ShouldSucceed()
        {
            var response = await _cursorApi.PostCursorAsync<MyModel>(
                "RETURN { MyProperty: CONCAT('This is a ', @testString) }",
                new Dictionary<string, object> { ["testString"] = "robbery" });

            var result = response.Result;
            Assert.Equal(1, result.Count);
            Assert.Equal("This is a robbery", result.First().MyProperty);
        }

        [Fact]
        public async Task PostCursorAsync_ShouldThrow_WhenAqlIsNotValid()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _cursorApi.PostCursorAsync<MyModel>("RETURN blah");
            });

            Assert.NotNull(ex.ApiError.ErrorMessage);
            Assert.Equal(1203, ex.ApiError.ErrorNum);
        }

        [Fact]
        public async Task PutCursorAsync_ShouldSucceed()
        {
            var response = await _cursorApi.PostCursorAsync<long>("FOR i IN 0..1000 RETURN i");
            Assert.True(response.HasMore);

            var nextResponse = await _cursorApi.PutCursorAsync<long>(response.Id);
            Assert.False(nextResponse.HasMore);
            Assert.Single(nextResponse.Result);
            Assert.Equal(1000, nextResponse.Result.First());
        }

        [Fact]
        public async Task PutCursorAsync_ShouldThrow_WhenCursorIsExhausted()
        {
            var response = await _cursorApi.PostCursorAsync<long>("FOR i IN 0..1000 RETURN i");
            Assert.True(response.HasMore);

            var nextResponse = await _cursorApi.PutCursorAsync<long>(response.Id);
            Assert.False(nextResponse.HasMore);

            await Assert.ThrowsAsync<ApiErrorException>(async () => 
                await _cursorApi.PutCursorAsync<long>(response.Id));
        }

        [Fact]
        public async Task PutCursorAsync_ShouldThrow_WhenCursorDoesNotExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () => 
                await _cursorApi.PutCursorAsync<long>("nada"));

            Assert.NotNull(ex.ApiError.ErrorMessage);
            Assert.Equal(1600, ex.ApiError.ErrorNum);
            Assert.Equal(404, ex.ApiError.Code);
        }

        [Fact]
        public async Task DeleteCursorAsync_ShouldThrow_WhenCursorDoesNotExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () => 
                await _cursorApi.DeleteCursorAsync("nada"));

            Assert.NotNull(ex.ApiError.ErrorMessage);
            Assert.Equal(1600, ex.ApiError.ErrorNum);
            Assert.Equal(404, ex.ApiError.Code);
        }

        [Fact]
        public async Task DeleteCursorAsync_ShouldSucceed()
        {
            var response = await _cursorApi.PostCursorAsync<long>("FOR i IN 0..1000 RETURN i");
            Assert.True(response.HasMore);

            var deleteResponse = await _cursorApi.DeleteCursorAsync(response.Id);
        }
    }
}
