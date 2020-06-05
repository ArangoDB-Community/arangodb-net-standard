using ArangoDBNetStandard;
using ArangoDBNetStandard.CursorApi;
using ArangoDBNetStandard.CursorApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ArangoDBNetStandardTest.CursorApi
{
    public class CursorApiClientTest : IClassFixture<CursorApiClientTestFixture>
    {
        private CursorApiClient _cursorApi;

        public class MyModel
        {
            public string MyProperty { get; set; }
        }

        public CursorApiClientTest(CursorApiClientTestFixture fixture)
        {
            _cursorApi = fixture.ArangoDBClient.Cursor;
        }
        
        [Fact]
        public async Task PostCursorAsync_ShouldSucceed()
        {
            var response = await _cursorApi.PostCursorAsync<MyModel>(
                "RETURN { MyProperty: CONCAT('This is a ', @testString) }",
                new Dictionary<string, object> { ["testString"] = "robbery" });

            var result = response.Result;
            Assert.Single(result);
            Assert.Equal("This is a robbery", result.First().MyProperty);
        }

        [Fact]
        public async Task PostCursorAsync_ShouldSucceed_WhenQueryResultsInWarnings()
        {
            var response = await _cursorApi.PostCursorAsync<object>("RETURN 1 / 0");

            Assert.Single(response.Result);
            Assert.Null(response.Result.First());
            Assert.NotEmpty(response.Extra.Warnings);
            Assert.Equal(1562, response.Extra.Warnings.First().Code);
            Assert.NotNull(response.Extra.Warnings.First().Message);
        }

        [Fact]
        public async Task PostCursorAsync_ShouldSucceed_WhenUsingFullCountOption()
        {
            var response = await _cursorApi.PostCursorAsync<MyModel>(
                "FOR doc IN [{ myProperty: CONCAT('This is a ', @testString) }] LIMIT 1 RETURN doc",
                new Dictionary<string, object> { ["testString"] = "robbery" },
                new PostCursorOptions
                {
                    FullCount = true
                });

            Assert.Single(response.Result);
            Assert.Equal("This is a robbery", response.Result.First().MyProperty);
            Assert.NotNull(response.Extra);
            Assert.Equal(1, response.Extra.Stats.FullCount);
        }

        [Fact]
        public async Task PostCursorAsync_ShouldSucceed_WhenUsingProfileOption1()
        {
            var response = await _cursorApi.PostCursorAsync<MyModel>(
                "FOR doc IN [{ myProperty: CONCAT('This is a ', @testString) }] LIMIT 1 RETURN doc",
                new Dictionary<string, object> { ["testString"] = "robbery" },
                new PostCursorOptions
                {
                    Profile = 1
                });

            Assert.Single(response.Result);
            Assert.Equal("This is a robbery", response.Result.First().MyProperty);
            Assert.NotNull(response.Extra);

            var profile = response.Extra.Profile;
            Assert.NotNull(profile);
            Assert.NotEqual(0, profile["executing"]);
            Assert.NotEqual(0, profile["finalizing"]);
            Assert.NotEqual(0, profile["initializing"]);
            Assert.NotEqual(0, profile["instantiating plan"]);
            Assert.NotEqual(0, profile["loading collections"]);
            Assert.NotEqual(0, profile["optimizing ast"]);
            Assert.NotEqual(0, profile["optimizing plan"]);
            Assert.NotEqual(0, profile["parsing"]);

            Assert.Null(response.Extra.Plan);
        }

        [Fact]
        public async Task PostCursorAsync_ShouldSucceed_WhenUsingProfileOption2()
        {
            var response = await _cursorApi.PostCursorAsync<MyModel>(
                "FOR doc IN [{ myProperty: CONCAT('This is a ', @testString) }] LIMIT 1 RETURN doc",
                new Dictionary<string, object> { ["testString"] = "robbery" },
                new PostCursorOptions
                {
                    Profile = 2
                });

            Assert.Single(response.Result);
            Assert.Equal("This is a robbery", response.Result.First().MyProperty);
            Assert.NotNull(response.Extra);

            var profile = response.Extra.Profile;
            Assert.NotNull(profile);
            Assert.NotEqual(0, profile["executing"]);
            Assert.NotEqual(0, profile["finalizing"]);
            Assert.NotEqual(0, profile["initializing"]);
            Assert.NotEqual(0, profile["instantiating plan"]);
            Assert.NotEqual(0, profile["loading collections"]);
            Assert.NotEqual(0, profile["optimizing ast"]);
            Assert.NotEqual(0, profile["optimizing plan"]);
            Assert.NotEqual(0, profile["parsing"]);

            var plan = response.Extra.Plan;
            Assert.NotNull(plan);
            Assert.NotEmpty(plan.Nodes);
            Assert.Empty(plan.Collections);
            Assert.Empty(plan.Rules);
            Assert.NotEmpty(plan.Variables);
            Assert.NotEqual(0, plan.EstimatedCost);
            Assert.NotEqual(0, plan.EstimatedNrItems);
            Assert.False(plan.IsModificationQuery);

            Assert.NotNull(response.Extra.Stats.Nodes);
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
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
        }

        [Fact]
        public async Task DeleteCursorAsync_ShouldThrow_WhenCursorDoesNotExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () => 
                await _cursorApi.DeleteCursorAsync("nada"));

            Assert.NotNull(ex.ApiError.ErrorMessage);
            Assert.Equal(1600, ex.ApiError.ErrorNum);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
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
