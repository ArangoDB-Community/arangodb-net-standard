using ArangoDBNetStandard;
using ArangoDBNetStandard.UserApi;
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
    }
}
