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
        public async Task ListDatabasesAsync_ShouldSucceed()
        {
            ListDatabaseResult result = await _fixture.DatabaseClientSystem.ListDatabasesAsync();

            Assert.False(result.Error);
            Assert.Equal(HttpStatusCode.OK, result.Code);
            Assert.True(result.Result.Count() > 0);
        }

        [Fact]
        public async Task ListDatabasesAsync_ShouldThrow_WhenDatabaseIsNotSystem()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _fixture.DatabaseClientOther.ListDatabasesAsync();
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
                await _fixture.DatabaseClientNonExistent.ListDatabasesAsync();
            });

            ApiErrorResponse apiError = ex.ApiError;

            Assert.Equal(HttpStatusCode.NotFound, apiError.Code);
            Assert.Equal(1228, apiError.ErrorNum);
        }

        [Fact]
        public async Task ListUserDatabasesAsync_ShouldSucceed()
        {
            ListDatabaseResult result = await _fixture.DatabaseClientOther.ListUserDatabasesAsync();

            Assert.False(result.Error);
            Assert.Equal(HttpStatusCode.OK, result.Code);
            Assert.True(result.Result.Count() > 0);
        }

        [Fact]
        public async Task ListUserDatabasesAsync_ShouldThrow_WhenDatabaseDoesNotExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _fixture.DatabaseClientNonExistent.ListDatabasesAsync();
            });

            ApiErrorResponse apiError = ex.ApiError;

            Assert.Equal(HttpStatusCode.NotFound, apiError.Code);
            Assert.Equal(1228, apiError.ErrorNum);
        }
    }
}
