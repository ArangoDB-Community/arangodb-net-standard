using ArangoDBNetStandard;
using ArangoDBNetStandard.Transport;
using ArangoDBNetStandard.UserApi;
using ArangoDBNetStandard.UserApi.Models;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ArangoDBNetStandardTest.UserApi
{
    public class UserApiClientTest : IClassFixture<UserApiClientTestFixture>
    {
        private readonly IUserApiClient _userClient;

        private readonly UserApiClientTestFixture _fixture;

        public UserApiClientTest(UserApiClientTestFixture fixture)
        {
            _userClient = fixture.ArangoClient.User;
            _fixture = fixture;
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldSucceed()
        {
            var deleteResponse = await _userClient.DeleteUserAsync(
                _fixture.UsernameToDelete);

            Assert.False(deleteResponse.Error);
            Assert.Equal(HttpStatusCode.Accepted, deleteResponse.Code);
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldThrow_WhenUserDoesNotExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                await _userClient.DeleteUserAsync(
                    nameof(DeleteUserAsync_ShouldThrow_WhenUserDoesNotExist)));

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.NotNull(ex.ApiError.ErrorMessage);
            Assert.Equal(1703, ex.ApiError.ErrorNum); // ERROR_USER_NOT_FOUND
        }

        [Fact]
        public async Task PostUserAsync_ShouldSucceed()
        {
            PostUserResponse postResponse = await _userClient.PostUserAsync(
                new PostUserBody()
                {
                    User = _fixture.UsernameToCreate,
                    Passwd = "password",
                    Extra = new Dictionary<string, object>()
                    {
                        ["somedata"] = "here"
                    }
                });

            Assert.False(postResponse.Error);
            Assert.Equal(HttpStatusCode.Created, postResponse.Code);
            Assert.Equal(_fixture.UsernameToCreate, postResponse.User);
            Assert.True(postResponse.Active);
            Assert.True(postResponse.Extra.ContainsKey("somedata"));
            Assert.Equal("here", postResponse.Extra["somedata"].ToString());
        }

        [Fact]
        public async Task PostUserAsync_ShouldThrow_WhenUserAlreadyExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                await _userClient.PostUserAsync(new PostUserBody()
                {
                    User = _fixture.UsernameExisting,
                    Passwd = "password",
                    Extra = new Dictionary<string, object>()
                    {
                        ["somedata"] = "here"
                    }
                }));

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.Conflict, ex.ApiError.Code);
            Assert.NotNull(ex.ApiError.ErrorMessage);
            Assert.Equal(1702, ex.ApiError.ErrorNum); // ERROR_USER_DUPLICATE
        }

        [Fact]
        public async Task PutUserAsync_ShouldSucceed()
        {
            PutUserResponse response = await _userClient.PutUserAsync(
                _fixture.UsernameExisting,
                new PutUserBody()
                {
                    Extra = new Dictionary<string, object>()
                    {
                        ["somedata"] = nameof(PutUserAsync_ShouldSucceed)
                    }
                });

            Assert.False(response.Error);
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.Equal(_fixture.UsernameExisting, response.User);
            Assert.True(response.Active);
            Assert.True(response.Extra.ContainsKey("somedata"));
            Assert.Equal(nameof(PutUserAsync_ShouldSucceed), response.Extra["somedata"].ToString());
        }

        [Fact]
        public async Task PutUserAsync_ShouldThrow_WhenUserDoesNotExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _userClient.PutUserAsync(
                    nameof(PutUserAsync_ShouldThrow_WhenUserDoesNotExist),
                    new PutUserBody()
                    {
                        Extra = new Dictionary<string, object>()
                    });
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.NotNull(ex.ApiError.ErrorMessage);
            Assert.Equal(1703, ex.ApiError.ErrorNum); // ERROR_USER_NOT_FOUND
        }

        [Fact]
        public async Task PatchUserAsync_ShouldSucceed()
        {
            PatchUserResponse response = await _userClient.PatchUserAsync(
                _fixture.UsernameExisting,
                new PatchUserBody()
                {
                    Extra = new Dictionary<string, object>()
                    {
                        ["somedata"] = nameof(PatchUserAsync_ShouldSucceed)
                    }
                });

            Assert.False(response.Error);
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.Equal(_fixture.UsernameExisting, response.User);
            Assert.True(response.Active);
            Assert.True(response.Extra.ContainsKey("somedata"));
            Assert.Equal(nameof(PatchUserAsync_ShouldSucceed), response.Extra["somedata"].ToString());
        }

        [Fact]
        public async Task PatchUserAsync_ShouldThrow_WhenUserDoesNotExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _userClient.PatchUserAsync(
                    nameof(PatchUserAsync_ShouldThrow_WhenUserDoesNotExist),
                    new PatchUserBody()
                    {
                        Extra = new Dictionary<string, object>()
                    });
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.NotNull(ex.ApiError.ErrorMessage);
            Assert.Equal(1703, ex.ApiError.ErrorNum); // ERROR_USER_NOT_FOUND
        }

        [Fact]
        public async Task GetUserAsync_ShouldSucceed()
        {
            GetUserResponse response = await _userClient.GetUserAsync(
                _fixture.UsernameExisting);

            Assert.False(response.Error);
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.Equal(_fixture.UsernameExisting, response.User);
            Assert.True(response.Active);
            Assert.NotNull(response.Extra);
        }

        [Fact]
        public async Task GetUserAsync_ShouldThrow_WhenUserDoesNotExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _userClient.GetUserAsync(
                    nameof(GetUserAsync_ShouldThrow_WhenUserDoesNotExist));
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.NotNull(ex.ApiError.ErrorMessage);
            Assert.Equal(1703, ex.ApiError.ErrorNum); // ERROR_USER_NOT_FOUND
        }

        [Fact]
        public async Task GetUsersAsync_ShouldSucceed()
        {
            GetUsersResponse response = await _userClient.GetUsersAsync();

            Assert.False(response.Error);
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.NotNull(response.Result);
            Assert.True(response.Result.Count() > 0);

            foreach (AvailableUser user in response.Result)
            {
                Assert.False(string.IsNullOrEmpty(user.User));
            }
        }

        [Fact]
        public async Task PutDatabaseAccessLevelAsync_ShouldSucceed()
        {
            PutAccessLevelResponse response =
                await _userClient.PutDatabaseAccessLevelAsync(
                    _fixture.UsernameExisting,
                    "_system",
                    new PutAccessLevelBody()
                    {
                        Grant = "rw"
                    });

            Assert.False(response.Error);
            Assert.Equal(HttpStatusCode.OK, response.Code);
        }

        [Fact]
        public async Task PutDatabaseAccessLevelAsync_ShouldThrow_WhenUserDoesNotExist()
        {
            string username = nameof(PutDatabaseAccessLevelAsync_ShouldThrow_WhenUserDoesNotExist);

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _userClient.PutDatabaseAccessLevelAsync(
                    username,
                    "_system",
                    new PutAccessLevelBody()
                    {
                        Grant = "rw"
                    });
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.NotNull(ex.ApiError.ErrorMessage);
            Assert.Equal(1703, ex.ApiError.ErrorNum); // ERROR_USER_NOT_FOUND
        }

        [Fact]
        public async Task GetDatabaseAccessLevelAsync_ShouldSucceed()
        {
            GetAccessLevelResponse response =
                await _userClient.GetDatabaseAccessLevelAsync(
                    _fixture.UsernameExisting,
                    _fixture.TestDbName);

            Assert.False(response.Error);
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.False(string.IsNullOrEmpty(response.Result));
        }

        [Fact]
        public async Task GetDatabaseAccessLevelAsync_ShouldThrow_WhenErrorResponseReturned()
        {
            // Arrange

            var mockTransport = new Mock<IApiClientTransport>();

            var mockResponse = new Mock<IApiClientResponse>();

            var mockResponseContent = new Mock<IApiClientResponseContent>();

            string mockJsonResponse = "{\"error\":true,\"errorMessage\":\"user not found\",\"errorNum\":1703,\"code\":404}";

            mockResponseContent.Setup(x => x.ReadAsStreamAsync())
                .Returns(Task.FromResult<Stream>(
                    new MemoryStream(Encoding.UTF8.GetBytes(mockJsonResponse))));

            mockResponse.Setup(x => x.Content)
                .Returns(mockResponseContent.Object);

            mockResponse.Setup(x => x.IsSuccessStatusCode)
                .Returns(false);

            string requestUri = null;

            mockTransport.Setup(x => x.GetAsync(It.IsAny<string>()))
                .Returns((string uri) =>
                {
                    requestUri = uri;
                    return Task.FromResult(mockResponse.Object);
                });

            var client = new UserApiClient(mockTransport.Object);

            // Act

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await client.GetDatabaseAccessLevelAsync("", "");
            });

            // Assert

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.NotNull(ex.ApiError.ErrorMessage);
            Assert.Equal(1703, ex.ApiError.ErrorNum); // ERROR_USER_NOT_FOUND
        }

        [Fact]
        public async Task DeleteDatabaseAccessLevelAsync_ShouldSucceed()
        {
            DeleteAccessLevelResponse response =
                await _userClient.DeleteDatabaseAccessLevelAsync(
                    _fixture.UsernameToRemoveAccess,
                    _fixture.TestDbName);

            Assert.False(response.Error);
            Assert.Equal(HttpStatusCode.Accepted, response.Code);
        }

        [Fact]
        public async Task DeleteDatabaseAccessLevelAsync_ShouldThrow_WhenUserDoesNotExist()
        {
            string username = nameof(DeleteDatabaseAccessLevelAsync_ShouldThrow_WhenUserDoesNotExist);

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _userClient.DeleteDatabaseAccessLevelAsync(username, "_system");
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.NotNull(ex.ApiError.ErrorMessage);
            Assert.Equal(1703, ex.ApiError.ErrorNum); // ERROR_USER_NOT_FOUND
        }

        [Fact]
        public async Task GetAccessibleDatabasesAsync_ShouldSucceed()
        {
            GetAccessibleDatabasesResponse response =
                await _userClient.GetAccessibleDatabasesAsync(_fixture.UsernameExisting);

            Assert.False(response.Error);
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.NotNull(response.Result);
            Assert.True(response.Result.Keys.Count > 0);

            string accessLevel = response.Result[response.Result.Keys.First()].ToString();
            Assert.False(string.IsNullOrEmpty(accessLevel));
        }

        [Fact]
        public async Task GetAccessibleDatabasesAsync_ShouldUseQueryParameters_WhenProvided()
        {
            var mockTransport = new Mock<IApiClientTransport>();

            var mockResponse = new Mock<IApiClientResponse>();

            var mockResponseContent = new Mock<IApiClientResponseContent>();

            mockResponse.Setup(x => x.Content)
                .Returns(mockResponseContent.Object);

            mockResponse.Setup(x => x.IsSuccessStatusCode)
                .Returns(true);

            string requestUri = null;

            mockTransport.Setup(x => x.GetAsync(It.IsAny<string>()))
                .Returns((string uri) =>
                {
                    requestUri = uri;
                    return Task.FromResult(mockResponse.Object);
                });

            var client = new UserApiClient(mockTransport.Object);

            await client.GetAccessibleDatabasesAsync("", new GetAccessibleDatabasesQuery()
            {
                Full = true
            });

            Assert.NotNull(requestUri);
            Assert.Contains("full=true", requestUri);
        }

        [Fact]
        public async Task GetAccessibleDatabasesAsync_ShouldSucceed_WhenFullIsProvided()
        {
            GetAccessibleDatabasesResponse response =
                await _userClient.GetAccessibleDatabasesAsync(
                    _fixture.UsernameExisting,
                    new GetAccessibleDatabasesQuery()
                    {
                        Full = true
                    });

            Assert.False(response.Error);
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.NotNull(response.Result);
            Assert.True(response.Result.Keys.Count > 0);

            object accessLevel = response.Result[response.Result.Keys.First()];
            var jObject = accessLevel as Newtonsoft.Json.Linq.JObject;

            Assert.NotNull(jObject);
            Assert.True(jObject.ContainsKey("permission"));
            Assert.True(jObject.ContainsKey("collections"));
        }

        [Fact]
        public async Task GetAccessibleDatabasesAsync_ShouldThrow_WhenUserDoesNotExist()
        {
            string username = nameof(GetAccessibleDatabasesAsync_ShouldThrow_WhenUserDoesNotExist);

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _userClient.GetAccessibleDatabasesAsync(username);
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.NotNull(ex.ApiError.ErrorMessage);
            Assert.Equal(1703, ex.ApiError.ErrorNum); // ERROR_USER_NOT_FOUND
        }

        [Fact]
        public async Task PutCollectionAccessLevelAsync_ShouldSucceed()
        {
            PutAccessLevelResponse response =
                await _userClient.PutCollectionAccessLevelAsync(
                    _fixture.UsernameExisting,
                    _fixture.TestDbName,
                    _fixture.CollectionNameToSetAccess,
                    new PutAccessLevelBody()
                    {
                        Grant = "rw"
                    });

            Assert.False(response.Error);
            Assert.Equal(HttpStatusCode.OK, response.Code);
        }

        [Fact]
        public async Task PutCollectionAccessLevelAsync_ShouldThrow_WhenUserDoesNotExist()
        {
            string username = nameof(PutCollectionAccessLevelAsync_ShouldThrow_WhenUserDoesNotExist);

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _userClient.PutCollectionAccessLevelAsync(
                    username,
                    _fixture.TestDbName,
                    _fixture.CollectionNameToSetAccess,
                    new PutAccessLevelBody()
                    {
                        Grant = "rw"
                    });
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.NotNull(ex.ApiError.ErrorMessage);
            Assert.Equal(1703, ex.ApiError.ErrorNum); // ERROR_USER_NOT_FOUND
        }

        [Fact]
        public async Task GetCollectionAccessLevelAsync_ShouldSucceed()
        {
            GetAccessLevelResponse response =
                await _userClient.GetCollectionAccessLevelAsync(
                    _fixture.UsernameExisting,
                    _fixture.TestDbName,
                    _fixture.CollectionNameToSetAccess);

            Assert.False(response.Error);
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.False(string.IsNullOrEmpty(response.Result));
        }

        [Fact]
        public async Task GetCollectionAccessLevelAsync_ShouldThrow_WhenErrorResponseReturned()
        {
            // Arrange

            var mockTransport = new Mock<IApiClientTransport>();

            var mockResponse = new Mock<IApiClientResponse>();

            var mockResponseContent = new Mock<IApiClientResponseContent>();

            string mockJsonResponse = "{\"error\":true,\"errorMessage\":\"user not found\",\"errorNum\":1703,\"code\":404}";

            mockResponseContent.Setup(x => x.ReadAsStreamAsync())
                .Returns(Task.FromResult<Stream>(
                    new MemoryStream(Encoding.UTF8.GetBytes(mockJsonResponse))));

            mockResponse.Setup(x => x.Content)
                .Returns(mockResponseContent.Object);

            mockResponse.Setup(x => x.IsSuccessStatusCode)
                .Returns(false);

            string requestUri = null;

            mockTransport.Setup(x => x.GetAsync(It.IsAny<string>()))
                .Returns((string uri) =>
                {
                    requestUri = uri;
                    return Task.FromResult(mockResponse.Object);
                });

            var client = new UserApiClient(mockTransport.Object);

            // Act

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await client.GetCollectionAccessLevelAsync("", "", "");
            });

            // Assert

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.NotNull(ex.ApiError.ErrorMessage);
            Assert.Equal(1703, ex.ApiError.ErrorNum); // ERROR_USER_NOT_FOUND
        }

        [Fact]
        public async Task DeleteCollectionAccessLevelAsync_ShouldSucceed()
        {
            DeleteAccessLevelResponse response =
                await _userClient.DeleteCollectionAccessLevelAsync(
                    _fixture.UsernameToRemoveAccess,
                    _fixture.TestDbName,
                    _fixture.CollectionNameToRemoveAccess);

            Assert.False(response.Error);
            Assert.Equal(HttpStatusCode.Accepted, response.Code);
        }

        [Fact]
        public async Task DeleteCollectionAccessLevelAsync_ShouldThrow_WhenUserDoesNotExist()
        {
            string username = nameof(DeleteCollectionAccessLevelAsync_ShouldThrow_WhenUserDoesNotExist);

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _userClient.DeleteCollectionAccessLevelAsync(
                    username,
                    _fixture.TestDbName,
                    _fixture.CollectionNameToRemoveAccess);
            });

            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.NotNull(ex.ApiError.ErrorMessage);
            Assert.Equal(1703, ex.ApiError.ErrorNum); // ERROR_USER_NOT_FOUND
        }
    }
}
