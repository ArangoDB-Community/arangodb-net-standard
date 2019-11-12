using ArangoDBNetStandard;
using ArangoDBNetStandard.DatabaseApi;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ArangoDBNetStandardTest.DatabaseApi
{
    /// <summary>
    /// Test class for <see cref="DatabaseApiClient"/>.
    /// </summary>
    public class DatabaseApiClientTest : IClassFixture<DatabaseApiClientTestFixture>
    {
        private DatabaseApiClientTestFixture _fixture;

        public DatabaseApiClientTest(DatabaseApiClientTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task PostDatabaseAsync_ShouldSucceed()
        {
            PostDatabaseResult result = await _fixture.DatabaseClientSystem.PostDatabaseAsync(
                new PostDatabaseRequest()
                {
                    Name = nameof(PostDatabaseAsync_ShouldSucceed)
                });

            await _fixture.DatabaseClientSystem.DeleteDatabaseAsync(nameof(PostDatabaseAsync_ShouldSucceed));

            Assert.False(result.Error);
            Assert.Equal(HttpStatusCode.Created, result.Code);
            Assert.True(result.Result);
        }

        [Fact]
        public async Task PostDatabaseAsync_ShouldThrow_WhenDatabaseUsedIsNotSystem()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _fixture.DatabaseClientOther.PostDatabaseAsync(new PostDatabaseRequest()
                {
                    Name = nameof(PostDatabaseAsync_ShouldThrow_WhenDatabaseUsedIsNotSystem)
                });
            });

            ApiErrorResponse apiError = ex.ApiError;

            Assert.Equal(HttpStatusCode.Forbidden, apiError.Code);
            Assert.Equal(1230, apiError.ErrorNum);
        }

        [Fact]
        public async Task PostDatabaseAsync_ShouldThrow_WhenDatabaseToCreateAlreadyExist()
        {
            await _fixture.DatabaseClientSystem.PostDatabaseAsync(new PostDatabaseRequest()
            {
                Name = nameof(PostDatabaseAsync_ShouldThrow_WhenDatabaseToCreateAlreadyExist)
            });

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _fixture.DatabaseClientSystem.PostDatabaseAsync(new PostDatabaseRequest()
                {
                    Name = nameof(PostDatabaseAsync_ShouldThrow_WhenDatabaseToCreateAlreadyExist)
                });
            });

            await _fixture.DatabaseClientSystem.DeleteDatabaseAsync(
                nameof(PostDatabaseAsync_ShouldThrow_WhenDatabaseToCreateAlreadyExist));

            ApiErrorResponse apiError = ex.ApiError;

            Assert.Equal(HttpStatusCode.Conflict, apiError.Code);
            Assert.Equal(1207, apiError.ErrorNum);
        }

        [Fact]
        public async Task PostDatabaseAsync_ShouldThrow_WhenDatabaseUsedDoesNotExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _fixture.DatabaseClientNonExistent.PostDatabaseAsync(new PostDatabaseRequest()
                {
                    Name = nameof(PostDatabaseAsync_ShouldThrow_WhenDatabaseUsedDoesNotExist)
                });
            });

            ApiErrorResponse apiError = ex.ApiError;

            Assert.Equal(HttpStatusCode.NotFound, apiError.Code);
            Assert.Equal(1228, apiError.ErrorNum);
        }

        [Fact]
        public async Task ListDatabasesAsync_ShouldSucceed()
        {
            ListDatabaseResult result = await _fixture.DatabaseClientSystem.GetDatabasesAsync();

            Assert.False(result.Error);
            Assert.Equal(HttpStatusCode.OK, result.Code);
            Assert.True(result.Result.Count() > 0);
        }

        [Fact]
        public async Task ListDatabasesAsync_ShouldThrow_WhenDatabaseIsNotSystem()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _fixture.DatabaseClientOther.GetDatabasesAsync();
            });

            ApiErrorResponse apiError = ex.ApiError;

            Assert.Equal(HttpStatusCode.Forbidden, apiError.Code);
            Assert.Equal(1230, apiError.ErrorNum);
        }

        [Fact]
        public async Task ListDatabasesAsync_ShouldThrow_WhenDatabaseDoesNotExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _fixture.DatabaseClientNonExistent.GetDatabasesAsync();
            });

            ApiErrorResponse apiError = ex.ApiError;

            Assert.Equal(HttpStatusCode.NotFound, apiError.Code);
            Assert.Equal(1228, apiError.ErrorNum);
        }

        [Fact]
        public async Task ListUserDatabasesAsync_ShouldSucceed()
        {
            ListDatabaseResult result = await _fixture.DatabaseClientOther.GetUserDatabasesAsync();

            Assert.False(result.Error);
            Assert.Equal(HttpStatusCode.OK, result.Code);
            Assert.True(result.Result.Count() > 0);
        }

        [Fact]
        public async Task ListUserDatabasesAsync_ShouldThrow_WhenDatabaseDoesNotExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _fixture.DatabaseClientNonExistent.GetDatabasesAsync();
            });

            ApiErrorResponse apiError = ex.ApiError;

            Assert.Equal(HttpStatusCode.NotFound, apiError.Code);
            Assert.Equal(1228, apiError.ErrorNum);
        }
    }
}
