using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ArangoDBNetStandard;
using ArangoDBNetStandard.CursorApi;
using ArangoDBNetStandard.CursorApi.Models;
using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using Moq;
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
        public async Task PostCursorAsync_ShouldSucceed_WhenUsingOtherOptions()
        {
            var response = await _cursorApi.PostCursorAsync<MyModel>(
                "FOR doc IN [{ myProperty: CONCAT('This is a ', @testString) }] LIMIT 1 RETURN doc",
                new Dictionary<string, object> { ["testString"] = "robbery" },
                new PostCursorOptions
                {
                    MaxRuntime = 10
                });

            Assert.Single(response.Result);
            Assert.Equal("This is a robbery", response.Result.First().MyProperty);
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
        public async Task PostCursorAsync_ShouldThrow_WhenErrorDeserializationFailed()
        {
            var mockTransport = new Mock<IApiClientTransport>();

            var mockResponse = new Mock<IApiClientResponse>();

            var mockResponseContent = new Mock<IApiClientResponseContent>();

            string mockJsonError = "{ errorNum: \"some_error\" }";

            mockResponseContent.Setup(x => x.ReadAsStreamAsync())
                .Returns(Task.FromResult<Stream>(
                    new MemoryStream(Encoding.UTF8.GetBytes(mockJsonError))));

            mockResponse.Setup(x => x.Content)
                .Returns(mockResponseContent.Object);

            mockResponse.Setup(x => x.IsSuccessStatusCode)
                .Returns(false);

            mockTransport.Setup(x => x.PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<byte[]>(),
                    It.IsAny<WebHeaderCollection>()))
                .Returns(Task.FromResult(mockResponse.Object));

            var cursorApi = new CursorApiClient(mockTransport.Object);

            var ex = await Assert.ThrowsAsync<SerializationException>(async () =>
            {
                await cursorApi.PostCursorAsync<object>("RETURN true");
            });

            Assert.NotNull(ex.Message);
            Assert.Contains("while Deserializing an error response", ex.Message);
            Assert.NotNull(ex.InnerException);
        }

        [Fact]
        public async Task PostCursorAsync_ShouldThrowException_WhenResponseDeserializationFailed()
        {
            var ex = await Assert.ThrowsAsync<SerializationException>(async () =>
            {
                await _cursorApi.PostCursorAsync<int>("RETURN null");
            });

            Assert.NotNull(ex.Message);
            Assert.Contains("while Deserializing the data response", ex.Message);
            Assert.NotNull(ex.InnerException);
        }

        [Fact]
        public async Task PostCursorAsync_ShouldUseHeaderProperties()
        {
            // Mock the IApiClientTransport.
            var mockTransport = new Mock<IApiClientTransport>();

            // Mock the IApiClientResponse.
            var mockResponse = new Mock<IApiClientResponse>();

            // Mock the IApiClientResponseContent.
            var mockResponseContent = new Mock<IApiClientResponseContent>();

            // Setup the mocked api client response.
            mockResponse.Setup(x => x.Content)
                .Returns(mockResponseContent.Object);
            mockResponse.Setup(x => x.IsSuccessStatusCode)
                .Returns(true);

            // Setup the mocked api client transport.
            WebHeaderCollection requestHeader = null;
            mockTransport.Setup(x => x.PostAsync(
                It.IsAny<string>(),
                It.IsAny<byte[]>(),
                It.IsAny<WebHeaderCollection>()))
                .Returns((string uri, byte[] content, WebHeaderCollection webHeaderCollection) =>
                {
                    requestHeader = webHeaderCollection;
                    return Task.FromResult(mockResponse.Object);
                });

            string dummyTransactionId = "dummy transaction Id";

            // Call the method to create the cursor.
            var apiClient = new CursorApiClient(mockTransport.Object);
            await apiClient.PostCursorAsync<MyModel>(
                 new PostCursorBody
                 {
                     Query = "FOR doc IN [{ myProperty: CONCAT('This is a ', @testString) }] LIMIT 1 RETURN doc",
                     BindVars = new Dictionary<string, object> { ["testString"] = "robbery" }
                 },
                 new CursorHeaderProperties
                 {
                     TransactionId = dummyTransactionId
                 });

            // Check that the header and values are there.
            Assert.NotNull(requestHeader);
            Assert.Contains(CustomHttpHeaders.StreamTransactionHeader, requestHeader.AllKeys);
            Assert.Equal(dummyTransactionId, requestHeader.Get(CustomHttpHeaders.StreamTransactionHeader));
        }

        [Fact]
        public async Task PostCursorAsync_ShouldReturnResponseModelWithInterface()
        {
            ICursorResponse<MyModel> response =
                await _cursorApi.PostCursorAsync<MyModel>("RETURN {}");

            Assert.NotNull(response);
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
            Assert.Equal(HttpStatusCode.OK, nextResponse.Code);
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
        public async Task PutCursorAsync_ShouldReturnResponseModelWithInterface()
        {
            PostCursorResponse<int> postResponse =
                await _cursorApi.PostCursorAsync<int>("FOR i IN 0..1500 RETURN i");

            ICursorResponse<int> putResult =
                await _cursorApi.PutCursorAsync<int>(postResponse.Id);

            Assert.NotNull(putResult);
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
