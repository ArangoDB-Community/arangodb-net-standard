using ArangoDBNetStandard;
using ArangoDBNetStandard.UserApi;
using ArangoDBNetStandard.UserApi.Models;
using System.Collections.Generic;
using System.Net;
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
    }
}
