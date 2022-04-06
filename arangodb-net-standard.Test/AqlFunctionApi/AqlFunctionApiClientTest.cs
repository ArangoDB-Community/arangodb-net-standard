using ArangoDBNetStandard;
using ArangoDBNetStandard.AqlFunctionApi.Models;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ArangoDBNetStandardTest.AqlFunctionApi
{
    /// <summary>
    /// Test class for <see cref="AqlFunctionApiClient"/>.
    /// </summary>
    public class AqlFunctionApiClientTest : IClassFixture<AqlFunctionApiClientTestFixture>
    {
        private readonly AqlFunctionApiClientTestFixture _fixture;

        public AqlFunctionApiClientTest(AqlFunctionApiClientTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task PostAqlFunctionAsync_ShouldSucceed()
        {
            string fullName = string.Join(
                "::",
                System.Environment.TickCount.ToString(),
                nameof(PostAqlFunctionAsync_ShouldSucceed));

            PostAqlFunctionResponse response = await _fixture.AqlFunctionClient.PostAqlFunctionAsync(
                new PostAqlFunctionBody()
                {
                    Name = fullName,
                    Code = "function (celsius) { return celsius * 1.8 + 32; }",
                    IsDeterministic = true
                });

            Assert.False(response.Error);
            Assert.Equal(HttpStatusCode.Created, response.Code);
            Assert.True(response.IsNewlyCreated);
        }

        [Fact]
        public async Task PostAqlFunctionAsync_ShouldThrow_WhenFunctionNameIsInvalid()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _fixture.AqlFunctionClient.PostAqlFunctionAsync(
                    new PostAqlFunctionBody()
                    {
                        // A non-fully qualified name will give an error
                        Name = nameof(PostAqlFunctionAsync_ShouldSucceed),
                        Code = "function (celsius) { return celsius * 1.8 + 32; }",
                        IsDeterministic = true
                    });
            });

            ApiErrorResponse apiError = ex.ApiError;

            Assert.Equal(HttpStatusCode.BadRequest, apiError.Code);
            Assert.Equal(1580, apiError.ErrorNum); // ERROR_QUERY_FUNCTION_INVALID_NAME
        }

        [Fact]
        public async Task DeleteAqlFunctionAsync_ShouldSucceed()
        {
            string groupName = System.Environment.TickCount.ToString();

            string fullName = string.Join(
                "::",
                groupName,
                nameof(DeleteAqlFunctionAsync_ShouldSucceed));

            PostAqlFunctionResponse postResponse =
                await _fixture.AqlFunctionClient.PostAqlFunctionAsync(
                    new PostAqlFunctionBody()
                    {
                        Name = fullName,
                        Code = "function (celsius) { return celsius * 1.8 + 32; }",
                        IsDeterministic = true
                    });

            DeleteAqlFunctionResponse deleteResponse =
                await _fixture.AqlFunctionClient.DeleteAqlFunctionAsync(
                    groupName,
                    new DeleteAqlFunctionQuery()
                    {
                        Group = true
                    });

            Assert.False(deleteResponse.Error);
            Assert.Equal(HttpStatusCode.OK, deleteResponse.Code);
            Assert.Equal(1, deleteResponse.DeletedCount);
        }

        [Fact]
        public async Task DeleteAqlFunctionAsync_ShouldThrow_WhenFunctionNameIsInvalid()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _fixture.AqlFunctionClient.DeleteAqlFunctionAsync(
                    "你好",
                    new DeleteAqlFunctionQuery()
                    {
                        Group = true
                    });
            });

            ApiErrorResponse apiError = ex.ApiError;

            Assert.Equal(HttpStatusCode.BadRequest, apiError.Code);
            Assert.Equal(1580, apiError.ErrorNum); // ERROR_QUERY_FUNCTION_INVALID_NAME
        }

        [Fact]
        public async Task GetAqlFunctionsAsync_ShouldSucceed()
        {
            string groupName = System.Environment.TickCount.ToString();

            string fullName = string.Join(
                "::",
                groupName,
                nameof(DeleteAqlFunctionAsync_ShouldSucceed));

            PostAqlFunctionResponse postResponse =
                await _fixture.AqlFunctionClient.PostAqlFunctionAsync(
                    new PostAqlFunctionBody()
                    {
                        Name = fullName,
                        Code = "function (celsius) { return celsius * 1.8 + 32; }",
                        IsDeterministic = true
                    });

            GetAqlFunctionsResponse getResponse =
                await _fixture.AqlFunctionClient.GetAqlFunctionsAsync(
                    new GetAqlFunctionsQuery()
                    {
                        Namespace = groupName
                    });

            Assert.False(getResponse.Error);
            Assert.Equal(HttpStatusCode.OK, getResponse.Code);
            Assert.Single(getResponse.Result);

            AqlFunctionResult firstResult = getResponse.Result[0];

            Assert.Equal(fullName, firstResult.Name);
            Assert.Equal("function (celsius) { return celsius * 1.8 + 32; }", firstResult.Code);
            Assert.True(firstResult.IsDeterministic);
        }


        [Fact]
        public async Task GetSlowAqlQueriesAsync_ShouldSucceed()
        {
            var getResponse =
                  await _fixture.AqlFunctionClient.GetSlowAqlQueriesAsync();

            //Assert.False(getResponse.Error);
            //Assert.Equal(HttpStatusCode.OK, getResponse.Code);
        }

        [Fact]
        public async Task DeleteClearSlowAqlQueriesAsync_ShouldSucceed()
        {
            ResponseBase deleteResponse =
                await _fixture.AqlFunctionClient.DeleteClearSlowAqlQueriesAsync();

            Assert.False(deleteResponse.Error);
            Assert.Equal(HttpStatusCode.OK, deleteResponse.Code);
        }


        [Fact]
        public async Task PostExplainAqlQueryAsync_ShouldSucceed()
        {
            PostExplainAqlQueryResponse response =
                await _fixture.AqlFunctionClient.PostExplainAqlQueryAsync(
                new PostExplainAqlQueryBody()
                {
                     Query = _fixture.TestAqlQuery
                });
            Assert.False(response.Error);
            Assert.Equal(HttpStatusCode.OK, response.Code);
        }


        [Fact]
        public async Task PostParseAqlQueryAsync_ShouldSucceed()
        {
            PostParseAqlQueryResponse response =
                await _fixture.AqlFunctionClient.PostParseAqlQueryAsync(
                new PostParseAqlQueryBody()
                {  
                    Query = _fixture.TestAqlQuery
                });
            Assert.False(response.Error);
            Assert.Equal(HttpStatusCode.OK, response.Code);
        }

        [Fact]
        public async Task GetQueryTrackingConfigurationAsync_ShouldSucceed()
        {
            QueryTrackingConfiguration getResponse =
                await _fixture.AqlFunctionClient.GetQueryTrackingConfigurationAsync();

            Assert.False(getResponse.Error);
            Assert.Equal(HttpStatusCode.OK, getResponse.Code);
        }


        [Fact]
        public async Task PutChangeQueryTrackingConfigurationAsync_ShouldSucceed()
        {
            var getResponse =
                await _fixture.AqlFunctionClient.GetQueryTrackingConfigurationAsync();

            QueryTrackingConfiguration putResponse =
                await _fixture.AqlFunctionClient.PutChangeQueryTrackingConfigurationAsync(
                    new PutChangeQueryTrackingConfigurationBody()
                    {
                        Properties = getResponse
                    }
                );

            Assert.False(putResponse.Error);
            Assert.Equal(HttpStatusCode.OK, putResponse.Code);
        }
    }
}