using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ArangoDBNetStandard;
using ArangoDBNetStandard.ViewApi;
using ArangoDBNetStandard.ViewApi.Models;
using ArangoDBNetStandard.Transport;
using Moq;
using Xunit;

namespace ArangoDBNetStandardTest.ViewApi
{
    public class ViewApiClientTest : IClassFixture<ViewApiClientTestFixture>, IAsyncLifetime
    {
        private IViewApiClient _viewApi;
        private ArangoDBClient _adb;

        public ViewApiClientTest(ViewApiClientTestFixture fixture)
        {
            _adb = fixture.ArangoDBClient;
            _viewApi = _adb.View;
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
        public async Task GetAllViewsAsync_ShouldSucceed()
        {
            var testName = "GetAllViewsAsyncShouldSucceedView";
            var createResponse = await _viewApi.PostCreateViewAsync(
                new ViewDetails() 
                {
                    Name = testName,
                    Type = "arangosearch"
                });
            var res = await _viewApi.GetAllViewsAsync();

            Assert.Equal(HttpStatusCode.OK, res.Code);
            Assert.False(res.Error);
            Assert.NotNull(res.Result);
            Assert.NotEmpty(res.Result);
        }

        [Fact]
        public async Task PostCreateViewAsync_ShouldSucceed()
        {
            var testName = "PostCreateViewAsyncShouldSucceedView";
            var res = await _viewApi.PostCreateViewAsync(
                new ViewDetails()
                {
                    Name = testName,
                    Type = "arangosearch"
                });
            Assert.NotNull(res);
            Assert.NotNull(res.GloballyUniqueId);
        }

        [Fact]
        public async Task DeleteViewAsync_ShouldSucceed()
        {
            var testName = "DeleteViewAsyncShouldSucceedView";
            var createResponse = await _viewApi.PostCreateViewAsync(
                new ViewDetails()
                {
                    Name = testName,
                    Type = "arangosearch"
                });
            var res = await _viewApi.DeleteViewAsync(testName);

            Assert.Equal(HttpStatusCode.OK, res.Code);
            Assert.False(res.Error);
            Assert.True(res.Result);
        }

        [Fact]
        public async Task GetViewAsync_ShouldSucceed()
        {
            var testName = "GetViewAsyncShouldSucceedView";
            var createResponse = await _viewApi.PostCreateViewAsync(
                new ViewDetails()
                {
                    Name = testName,
                    Type = "arangosearch"
                });
            var res = await _viewApi.GetViewAsync(testName);

            Assert.Equal(HttpStatusCode.OK, res.Code);
            Assert.False(res.Error);
        }

        [Fact]
        public async Task GetViewPropertiesAsync_ShouldSucceed()
        {
            var testName = "GetViewPropertiesAsyncShouldSucceedView";
            var createResponse = await _viewApi.PostCreateViewAsync(
                new ViewDetails()
                {
                    Name = testName,
                    Type = "arangosearch"
                });
            var res = await _viewApi.GetViewPropertiesAsync(testName);

            Assert.Equal(HttpStatusCode.OK, res.Code);
            Assert.False(res.Error);
            Assert.NotNull(res.GloballyUniqueId);
        }

        [Fact]
        public async Task PutRenameViewAsync_ShouldSucceed()
        {
            var testName = "PutRenameViewAsyncShouldSucceedView";
            var newName = "PutRenameViewAsyncShouldSucceedViewRenamed";
            var createResponse = await _viewApi.PostCreateViewAsync(
                new ViewDetails()
                {
                    Name = testName,
                    Type = "arangosearch"
                });
            var res = await _viewApi.PutRenameViewAsync(
                testName,
                new PutRenameViewBody()
                {
                     Name = newName
                });

            Assert.Equal(HttpStatusCode.OK, res.Code);
            Assert.False(res.Error);
            Assert.NotNull(res.GloballyUniqueId);
            Assert.Equal(newName, res.Name);
        }
    }
}