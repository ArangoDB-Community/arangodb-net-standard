using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi;
using ArangoDBNetStandard.DocumentApi;
using Xunit;

namespace ArangoDBNetStandardTest.DocumentApi
{
    public class DocumentApiClientTest : IClassFixture<DocumentApiClientTestFixture>
    {
        /// <summary>
        /// Class used for testing document API.
        /// </summary>
        public class MyTestClass: DocumentBase
        {
            public string Message { get; set; }
        }

        private static readonly int NOT_FOUND_NUM = 1202;

        private DocumentApiClient _docClient;
        private ArangoDBClient _adb;

        public DocumentApiClientTest(DocumentApiClientTestFixture fixture)
        {
            _adb = fixture.ArangoDBClient;
            _docClient = _adb.Document;

            // Truncate TestCollection before each test
            _adb.Collection.PutCollectionTruncateAsync(fixture.TestCollection)
                .GetAwaiter()
                .GetResult();
        }

        [Fact]
        public async Task DeleteDocument_ShouldSucceed()
        {
            Dictionary<string, object> document = new Dictionary<string, object> { ["key"] = "value" };
            var response = await _docClient.PostDocumentAsync("TestCollection", document);
            Assert.NotNull(response._id);

            var deleteResponse = await _docClient.DeleteDocumentAsync(response._id);
            Assert.True(deleteResponse.StatusCode >= 200 && deleteResponse.StatusCode < 300);

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                await _docClient.GetDocumentAsync<object>(response._id));

            Assert.Equal(NOT_FOUND_NUM, ex.ApiError.ErrorNum); // document not found
        }

        [Fact]
        public async Task DeleteDocument_ShouldThrow_WhenDocumentNotFound()
        {
            Dictionary<string, object> document = new Dictionary<string, object> { ["key"] = "value" };
            var response = await _docClient.PostDocumentAsync("TestCollection", document);
            Assert.NotNull(response._id);

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                await _docClient.DeleteDocumentAsync("TestCollection/abc123"));

            Assert.Equal(NOT_FOUND_NUM, ex.ApiError.ErrorNum);
        }

        [Fact]
        public async Task DeleteDocument_ShouldThrow_WhenDocumentIdNotValid()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await _docClient.DeleteDocumentAsync("NotAValidID"));
        }

        [Fact]
        public async Task GetDocument_ShouldSucceed()
        {
            var document = new Dictionary<string, object> { ["Message"] = "value" };
            var response = await _docClient.PostDocumentAsync("TestCollection", document);
            Assert.NotNull(response._id);

            var newDoc = await _docClient.GetDocumentAsync<MyTestClass>(response._id);

            Assert.NotNull(response._rev);
            Assert.Equal(response._rev, newDoc._rev);
            Assert.Equal("value", newDoc.Message);
        }

        [Fact]
        public async Task GetDocument_ShouldThrow_WhenDocumentDoesNotExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                await _docClient.GetDocumentAsync<object>("TestCollection/123"));

            Assert.Equal(NOT_FOUND_NUM, ex.ApiError.ErrorNum);
        }

        [Fact]
        public async Task GetDocument_ShouldThrow_WhenDocumentIdNotValid()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
               await _docClient.GetDocumentAsync<object>("123"));
        }

        [Fact]
        public async Task PostDocument_ShouldSucceed()
        {
            Dictionary<string, object> document = new Dictionary<string, object> { ["key"] = "value" };
            var response = await _docClient.PostDocumentAsync("TestCollection", document);
            Assert.False(string.IsNullOrWhiteSpace(response._id));
            Assert.False(string.IsNullOrWhiteSpace(response._key));
            Assert.False(string.IsNullOrWhiteSpace(response._rev));
            Assert.Null(response.New);
            Assert.Null(response.Old);
        }

        [Fact]
        public async Task PostDocument_ShouldSucceed_WhenNewDocIsReturned()
        {
            var doc = new { test = 123 };
            var response = await _docClient.PostDocumentAsync("TestCollection", doc, new PostDocumentsOptions
            {
                ReturnNew = true
            });
            Assert.Null(response.Old);
            Assert.NotNull(response.New);
            Assert.Equal(123, (int)response.New.test);
        }

        [Fact]
        public async Task PostDocument_ShouldFail_WhenDocumentIsInvalid()
        {
            var doc = new { test = 123, _key = "Spaces are not allowed in keys" };
            ApiErrorException ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                await _docClient.PostDocumentAsync("TestCollection", doc));

            Assert.NotNull(ex.ApiError.ErrorMessage);
        }

        [Fact]
        public async Task PostDocuments_ShouldSucceed()
        {
            var document1 = new { test = "value" };
            var document2 = new { test = "value" };
            PostDocumentsResponse<dynamic> response = await _docClient.PostDocumentsAsync("TestCollection", new dynamic[] { document1, document2 });
            Assert.Equal(2, response.Count);
            foreach (var innerResponse in response)
            {
                Assert.False(string.IsNullOrWhiteSpace(innerResponse._id));
                Assert.False(string.IsNullOrWhiteSpace(innerResponse._key));
                Assert.False(string.IsNullOrWhiteSpace(innerResponse._rev));
                Assert.Null(innerResponse.New);
                Assert.Null(innerResponse.Old);
            }
        }

        [Fact]
        public async Task PostDocuments_ShouldSucceed_WhenNewDocIsReturned()
        {
            dynamic document1 = new { test = "value" };
            dynamic document2 = new { test = "value" };
            var response = await _docClient.PostDocumentsAsync("TestCollection", new dynamic[] { document1, document2 }, new PostDocumentsOptions
            {
                ReturnNew = true
            });
            Assert.Equal(2, response.Count);
            foreach (var innerResponse in response)
            {
                Assert.False(string.IsNullOrWhiteSpace(innerResponse._id));
                Assert.False(string.IsNullOrWhiteSpace(innerResponse._key));
                Assert.False(string.IsNullOrWhiteSpace(innerResponse._rev));
                Assert.NotNull(innerResponse.New);
                Assert.Null(innerResponse.Old);
                Assert.Equal("value", (string)innerResponse.New.test);
            }
        }

        [Fact]
        public async Task PostDocuments_ShouldNotThrowButShouldReportError_WhenADocumentIsInvalid()
        {
            dynamic document1 = new { _key = "spaces are not allowed in keys" };
            dynamic document2 = new { test = "value" };
            var response = await _docClient.PostDocumentsAsync("TestCollection", new dynamic[] { document1, document2 });

            Assert.Equal(2, response.Count);
            Assert.True(response[0].Error);
            Assert.False(response[1].Error);
        }

        [Fact]
        public async Task PutDocument_ShouldSucceed()
        {
            var doc1 = new { _key = "test", stuff = "test" };
            var response = await _docClient.PostDocumentAsync("TestCollection", doc1);

            var updateResponse = await _docClient.PutDocumentAsync(
                response._id,
                new { stuff = "new" });

            Assert.NotNull(response._rev);
            Assert.NotNull(updateResponse._rev);
            Assert.NotEqual(response._rev, updateResponse._rev);
        }

        [Fact]
        public async Task PutDocument_ShouldSucceed_WhenNewDocumentIsReturned()
        {
            var doc1 = new { _key = "test", stuff = "test" };
            var response = await _docClient.PostDocumentAsync("TestCollection", doc1);

            var updateResponse = await _docClient.PutDocumentAsync(
                response._id,
                new { stuff = "new" },
                new PutDocumentsOptions
                {
                    ReturnNew = true
                });

            Assert.NotNull(response._rev);
            Assert.NotNull(updateResponse._rev);
            Assert.NotEqual(response._rev, updateResponse._rev);

            Assert.NotNull(updateResponse.New);
        }

        [Fact]
        public async Task PutDocument_ShouldThrow_WhenConflictingWriteAttempted_WithIgnoreRevsOptionFalse()
        {
            var doc1 = new { _key = "test", stuff = "test" };
            var response = await _docClient.PostDocumentAsync("TestCollection", doc1);

            var updateResponse1 = await _docClient.PutDocumentAsync(
                response._id,
                new { stuff = "new" },
                new PutDocumentsOptions
                {
                    ReturnNew = true
                });

            Assert.NotNull(response._rev);
            Assert.NotNull(updateResponse1._rev);
            Assert.NotEqual(response._rev, updateResponse1._rev);

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                await _docClient.PutDocumentAsync(
                    response._id,
                    // Use the initial rev value when updating, is out of sync
                    new { stuff = "more", response._rev },
                    new PutDocumentsOptions
                    {
                        IgnoreRevs = false
                    }));

            Assert.Equal(1200, ex.ApiError.ErrorNum); // 1200: ERROR_ARANGO_CONFLICT
        }

        [Fact]
        public async Task PutDocuments_ShouldSucceed()
        {
            var response = await _docClient.PostDocumentsAsync("TestCollection",
                new[] {
                    new { value = 1 },
                    new { value = 2 }
                });

            var updateResponse = await _docClient.PutDocumentsAsync("TestCollection",
                new[]
                {
                    new { response[0]._key, value = 3 },
                    new { response[1]._key, value = 4 }
                });

            Assert.NotNull(response[0]._rev);
            Assert.NotNull(updateResponse[0]._rev);
            Assert.NotEqual(response[0]._rev, updateResponse[0]._rev);

            Assert.NotNull(response[1]._rev);
            Assert.NotNull(updateResponse[1]._rev);
            Assert.NotEqual(response[1]._rev, updateResponse[1]._rev);
        }

        [Fact]
        public async Task PutDocuments_ShouldNotThrowButReturnError_WhenDocumentIsNotFound()
        {
            var response = await _docClient.PostDocumentsAsync("TestCollection",
                new[] {
                    new { value = 1 },
                    new { value = 2 }
                });

            var updateResponse = await _docClient.PutDocumentsAsync("TestCollection",
                new[]
                {
                    new { _key = "nonsense", value = 3 },
                    new { response[1]._key, value = 4 }
                });

            Assert.True(updateResponse[0].Error);
            Assert.Equal(NOT_FOUND_NUM, updateResponse[0].ErrorNum);
        }
    }
}
