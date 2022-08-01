using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ArangoDBNetStandard;
using ArangoDBNetStandard.DocumentApi;
using ArangoDBNetStandard.DocumentApi.Models;
using ArangoDBNetStandard.TransactionApi.Models;
using ArangoDBNetStandard.Transport;
using ArangoDBNetStandardTest.DocumentApi.Models;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace ArangoDBNetStandardTest.DocumentApi
{
    public class DocumentApiClientTest : IClassFixture<DocumentApiClientTestFixture>, IAsyncLifetime
    {
        /// <summary>
        /// Class used for testing document API.
        /// </summary>
        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class MyTestClass : DocumentBase
        {
            public string Message { get; set; }
        }

        private static readonly int NOT_FOUND_NUM = 1202;

        private readonly DocumentApiClient _docClient;
        private readonly ArangoDBClient _adb;
        private readonly string _testCollection;

        public DocumentApiClientTest(DocumentApiClientTestFixture fixture)
        {
            _adb = fixture.ArangoDBClient;
            _docClient = _adb.Document;
            _testCollection = fixture.TestCollection;
        }

        public async Task InitializeAsync()
        {
            // Truncate TestCollection before each test
            await _adb.Collection.TruncateCollectionAsync(_testCollection);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        [Fact]
        public async Task DeleteDocument_ShouldSucceed()
        {
            Dictionary<string, object> document = new Dictionary<string, object> { ["key"] = "value" };
            var response = await _docClient.PostDocumentAsync(_testCollection, document);
            Assert.NotNull(response._id);

            var deleteResponse = await _docClient.DeleteDocumentAsync(response._id);
            Assert.Null(deleteResponse.Old); // we didn't request `Old` so it should be null
            Assert.Equal(response._id, deleteResponse._id);
            Assert.Equal(response._key, deleteResponse._key);
            Assert.Equal(response._rev, deleteResponse._rev);

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                await _docClient.GetDocumentAsync<object>(response._id));

            Assert.Equal(NOT_FOUND_NUM, ex.ApiError.ErrorNum); // document not found
        }

        [Fact]
        public async Task DeleteDocument_ShouldSucceed_WhenOldDocumentOptionIsRequested()
        {
            Dictionary<string, object> document = new Dictionary<string, object> { ["Message"] = "Hello" };
            var response = await _docClient.PostDocumentAsync(_testCollection, document);
            Assert.NotNull(response._id);

            var deleteResponse = await _docClient.DeleteDocumentAsync<MyTestClass>(response._id, new DeleteDocumentQuery
            {
                ReturnOld = true
            });
            Assert.NotNull(deleteResponse.Old); // we requested `Old`, it should not be null
            Assert.Equal(response._id, deleteResponse._id);
            Assert.Equal(response._key, deleteResponse._key);
            Assert.Equal(response._rev, deleteResponse._rev);
            Assert.Equal("Hello", deleteResponse.Old.Message);

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                await _docClient.GetDocumentAsync<object>(response._id));

            Assert.Equal(NOT_FOUND_NUM, ex.ApiError.ErrorNum); // document not found
        }

        [Fact]
        public async Task DeleteDocument_ShouldUseQueryParameters_WhenProvided()
        {
            var mockTransport = new Mock<IApiClientTransport>();

            var mockResponse = new Mock<IApiClientResponse>();

            var mockResponseContent = new Mock<IApiClientResponseContent>();

            mockResponse.Setup(x => x.Content)
                .Returns(mockResponseContent.Object);

            mockResponse.Setup(x => x.IsSuccessStatusCode)
                .Returns(true);

            string requestUri = null;

            mockTransport.Setup(x => x.DeleteAsync(
                It.IsAny<string>(),
                It.IsAny<WebHeaderCollection>(), 
                It.IsAny<CancellationToken>()))
                .Returns((string uri, WebHeaderCollection webHeaderCollection, CancellationToken token) =>
                {
                    requestUri = uri;
                    return Task.FromResult(mockResponse.Object);
                });

            var client = new DocumentApiClient(mockTransport.Object);

            await client.DeleteDocumentAsync(
                "mycollection/0123456789",
                new DeleteDocumentQuery
                {
                    ReturnOld = true,
                    Silent = true,
                    WaitForSync = true
                });

            Assert.NotNull(requestUri);
            Assert.Contains("returnOld=true", requestUri);
            Assert.Contains("silent=true", requestUri);
            Assert.Contains("waitForSync=true", requestUri);
        }

        [Fact]
        public async Task DeleteDocument_ShouldThrow_WhenDocumentNotFound()
        {
            Dictionary<string, object> document = new Dictionary<string, object> { ["key"] = "value" };
            var response = await _docClient.PostDocumentAsync(_testCollection, document);
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
        public async Task DeleteDocuments_ShouldSucceed()
        {
            var docTasks = new[] {
                new Dictionary<string, object> { ["Message"] = "first" },
                new Dictionary<string, object> { ["Message"] = "second" }
            }
            .Select(item => _docClient.PostDocumentAsync(_testCollection, item))
            .ToList();

            PostDocumentResponse<Dictionary<string, object>>[] docs =
                await Task.WhenAll(docTasks);

            Assert.Collection(docs,
                (item) => Assert.NotNull(item._id),
                (item) => Assert.NotNull(item._id));

            var response = await _docClient.DeleteDocumentsAsync(_testCollection, docs.Select(d => d._id).ToList());
            Assert.Collection(response,
                (item) => Assert.NotNull(item._id),
                (item) => Assert.NotNull(item._id));

            // Should get "not found" for deleted docs
            Assert.Collection(docs,
                async (item) =>
                {
                    var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                        await _docClient.GetDocumentAsync<object>(item._id));

                    Assert.Equal(NOT_FOUND_NUM, ex.ApiError.ErrorNum); // document not found)
                },
                async (item) =>
                {
                    var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                        await _docClient.GetDocumentAsync<object>(item._id));

                    Assert.Equal(NOT_FOUND_NUM, ex.ApiError.ErrorNum); // document not found)
                });
        }

        [Fact]
        public async Task DeleteDocuments_ShouldSucceed_WhenOldDocumentOptionIsSelected()
        {
            var docTasks = new[] {
                new Dictionary<string, object> { ["Message"] = "first" },
                new Dictionary<string, object> { ["Message"] = "second" }
            }
            .Select(item => _docClient.PostDocumentAsync(_testCollection, item))
            .ToList();

            PostDocumentResponse<Dictionary<string, object>>[] docs =
                await Task.WhenAll(docTasks);

            Assert.Collection(docs,
                (item) => Assert.NotNull(item._id),
                (item) => Assert.NotNull(item._id));

            var response = await _docClient.DeleteDocumentsAsync<MyTestClass>(
                _testCollection,
                docs.Select(d => d._id).ToList(),
                new DeleteDocumentsQuery
                {
                    ReturnOld = true
                });

            Assert.Collection(response,
                (item) =>
                {
                    Assert.NotNull(item._id);
                    Assert.NotNull(item.Old);
                    Assert.Equal("first", item.Old.Message);
                },
                (item) =>
                {
                    Assert.NotNull(item._id);
                    Assert.NotNull(item.Old);
                    Assert.Equal("second", item.Old.Message);
                });

            // Should get "not found" for deleted docs
            Assert.Collection(docs,
                async (item) =>
                {
                    var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                        await _docClient.GetDocumentAsync<object>(item._id));

                    Assert.Equal(NOT_FOUND_NUM, ex.ApiError.ErrorNum); // document not found)
                },
                async (item) =>
                {
                    var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                        await _docClient.GetDocumentAsync<object>(item._id));

                    Assert.Equal(NOT_FOUND_NUM, ex.ApiError.ErrorNum); // document not found)
                });
        }

        [Fact]
        public async Task DeleteDocuments_ShouldSucceed_WhenSilent()
        {
            var createResponse = await _docClient.PostDocumentsAsync(
                _testCollection,
                new object[] {
                    new { testKey = "testValue" },
                    new { testKey = "testValue" }
                });

            Assert.Equal(2, createResponse.Count);

            var deleteResponse = await _docClient.DeleteDocumentsAsync(
                _testCollection,
                createResponse.Select(x => x._key).ToList(),
                new DeleteDocumentsQuery()
                {
                    Silent = true
                });

            Assert.Empty(deleteResponse);
        }

        [Fact]
        public async Task DeleteDocuments_ShouldUseQueryParameters_WhenProvided()
        {
            var mockTransport = new Mock<IApiClientTransport>();

            var mockResponse = new Mock<IApiClientResponse>();

            var mockResponseContent = new Mock<IApiClientResponseContent>();

            mockResponse.Setup(x => x.Content)
                .Returns(mockResponseContent.Object);

            mockResponse.Setup(x => x.IsSuccessStatusCode)
                .Returns(true);

            string requestUri = null;

            mockTransport.Setup(x => x.DeleteAsync(It.IsAny<string>(), It.IsAny<byte[]>(),
                It.IsAny<WebHeaderCollection>(),
                It.IsAny<CancellationToken>()))
                .Returns((string uri, byte[] content, WebHeaderCollection webHeaderCollection, CancellationToken token) =>
                {
                    requestUri = uri;
                    return Task.FromResult(mockResponse.Object);
                });

            var client = new DocumentApiClient(mockTransport.Object);

            await client.DeleteDocumentsAsync(
                "mycollection",
                new List<string>() { "0123456789" },
                new DeleteDocumentsQuery
                {
                    IgnoreRevs = true,
                    ReturnOld = true,
                    Silent = true,
                    WaitForSync = true
                });

            Assert.NotNull(requestUri);
            Assert.Contains("ignoreRevs=true", requestUri);
            Assert.Contains("returnOld=true", requestUri);
            Assert.Contains("silent=true", requestUri);
            Assert.Contains("waitForSync=true", requestUri);
        }

        [Fact]
        public async Task DeleteDocuments_ShouldNotThrowButReportFailure_WhenSomeDocumentSelectorsAreInvalid()
        {
            var docTasks = new[] {
                new Dictionary<string, object> { ["Message"] = "first" },
                new Dictionary<string, object> { ["Message"] = "second" }
            }
            .Select(item => _docClient.PostDocumentAsync(_testCollection, item))
            .ToList();

            PostDocumentResponse<Dictionary<string, object>>[] docs =
                await Task.WhenAll(docTasks);

            Assert.Collection(docs,
                (item) => Assert.NotNull(item._id),
                (item) => Assert.NotNull(item._id));

            var ids = docs.Select(d => d._id).ToList();
            ids[1] = "nonsense";

            var response = await _docClient.DeleteDocumentsAsync(_testCollection, ids);
            Assert.Collection(response,
                // First result succeeds
                (item) => Assert.NotNull(item._id),
                // Second result fails with NOT_FOUND error
                (item) =>
                {
                    Assert.Null(item._id);
                    Assert.True(item.Error);
                    Assert.Equal(NOT_FOUND_NUM, item.ErrorNum);
                });

            // Should get "not found" for deleted docs
            Assert.Collection(docs,
                async (item) =>
                {
                    var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                        await _docClient.GetDocumentAsync<object>(item._id));

                    Assert.Equal(NOT_FOUND_NUM, ex.ApiError.ErrorNum); // document not found)
                },
                async (item) =>
                {
                    var doc = await _docClient.GetDocumentAsync<MyTestClass>(item._id);

                    Assert.Equal("second", doc.Message); // document is found, it was not deleted
                });
        }

        [Fact]
        public async Task GetDocuments_ShouldSucceed()
        {
            const int NUM_DOCS = 3;
            var postDocResponses = new PostDocumentResponse<MyTestClass>[NUM_DOCS];
            for (int i = 0; i < NUM_DOCS; i++)
            {
                var document = new MyTestClass { Message = "Test " + i };
                postDocResponses[i] = await _docClient.PostDocumentAsync(_testCollection, document);
            }

            var getDocsResponse = await _docClient.GetDocumentsAsync<MyTestClass>(
                _testCollection,
                postDocResponses.Select(r => r._key).ToList());

            Assert.Equal(NUM_DOCS, getDocsResponse.Count);
            for (int i = 0; i < NUM_DOCS; i++)
            {
                var getDocResponse = getDocsResponse.FirstOrDefault(doc => doc._key == postDocResponses[i]._key);
                Assert.NotNull(getDocResponse);
                Assert.Equal(postDocResponses[i]._rev, getDocResponse._rev);
                Assert.Equal(postDocResponses[i]._id, getDocResponse._id);
            }
        }

        [Fact]
        public async Task GetDocument_ShouldSucceed()
        {
            var document = new Dictionary<string, object> { ["Message"] = "value" };
            var response = await _docClient.PostDocumentAsync(_testCollection, document);
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
            var response = await _docClient.PostDocumentAsync(_testCollection, document);
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
            var response = await _docClient.PostDocumentAsync(_testCollection, doc, new PostDocumentsQuery
            {
                ReturnNew = true
            });
            Assert.Null(response.Old);
            Assert.NotNull(response.New);
            Assert.Equal(123, (int)response.New.test);
        }

        [Fact]
        public async Task PostDocument_ShouldSucceed_WhenNewDocIsReturnedWithDifferentType()
        {
            var doc = new PostDocumentMockModelNew
            {
                Message = "Hello"
            };
            var response = await _docClient.PostDocumentAsync<PostDocumentMockModelNew, PostDocumentMockModel>(
                _testCollection,
                doc,
                new PostDocumentsQuery
                {
                    ReturnNew = true
                });
            Assert.Null(response.Old);
            Assert.NotNull(response.New);
            Assert.Equal(doc.Message, response.New.Message);
            Assert.Equal(response._id, response.New._id);
        }

        [Fact]
        public async Task PostDocument_ShouldFail_WhenDocumentIsInvalid()
        {
            var doc = new { test = 123, _key = "Spaces are not allowed in keys" };
            ApiErrorException ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                await _docClient.PostDocumentAsync(_testCollection, doc));

            Assert.NotNull(ex.ApiError.ErrorMessage);
        }

        [Fact]
        public async Task PostDocuments_ShouldSucceed()
        {
            var document1 = new { test = "value" };
            var document2 = new { test = "value" };
            PostDocumentsResponse<dynamic> response = await _docClient.PostDocumentsAsync(_testCollection, new dynamic[] { document1, document2 });
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
            var response = await _docClient.PostDocumentsAsync(_testCollection, new dynamic[] { document1, document2 }, new PostDocumentsQuery
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
        public async Task PostDocuments_ShouldSucceed_WhenSilent()
        {
            var createResponse = await _docClient.PostDocumentsAsync(
                _testCollection,
                new object[] {
                    new { testKey = "testValue" },
                    new { testKey = "testValue" }
                },
                new PostDocumentsQuery()
                {
                    Silent = true
                });

            Assert.Empty(createResponse);
        }

        [Fact]
        public async Task PostDocuments_ShouldNotThrowButShouldReportError_WhenADocumentIsInvalid()
        {
            dynamic document1 = new { _key = "spaces are not allowed in keys" };
            dynamic document2 = new { test = "value" };
            var response = await _docClient.PostDocumentsAsync(_testCollection, new dynamic[] { document1, document2 });

            Assert.Equal(2, response.Count);
            Assert.True(response[0].Error);
            Assert.False(response[1].Error);
        }

        [Fact]
        public async Task PutDocument_ShouldSucceed()
        {
            var doc1 = new { _key = "test", stuff = "test" };
            var response = await _docClient.PostDocumentAsync(_testCollection, doc1);

            var updateResponse = await _docClient.PutDocumentAsync(
                response._id,
                new { stuff = "new" });

            Assert.NotNull(response._rev);
            Assert.NotNull(updateResponse._rev);
            Assert.NotNull(updateResponse._oldRev);
            Assert.NotEqual(response._rev, updateResponse._rev);
            Assert.Equal(response._rev, updateResponse._oldRev);
        }

        /// <summary>
        /// Tests PutDocument overload accepting collection name + key instead of document id as parameter.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PutDocumentOverload_ShouldSucceed()
        {
            var doc1 = new { _key = "test", stuff = "test" };
            var response = await _docClient.PostDocumentAsync(_testCollection, doc1);

            var updateResponse = await _docClient.PutDocumentAsync(
                _testCollection,
                response._key,
                new { stuff = "new" });

            Assert.NotNull(response._rev);
            Assert.NotNull(updateResponse._rev);
            Assert.NotNull(updateResponse._oldRev);
            Assert.NotEqual(response._rev, updateResponse._rev);
            Assert.Equal(response._rev, updateResponse._oldRev);
        }

        [Fact]
        public async Task PutDocument_ShouldSucceed_WhenNewDocumentIsReturned()
        {
            var doc1 = new { _key = "test", stuff = "test" };
            var response = await _docClient.PostDocumentAsync(_testCollection, doc1);

            var updateResponse = await _docClient.PutDocumentAsync(
                response._id,
                new { stuff = "new" },
                new PutDocumentQuery
                {
                    ReturnNew = true
                });

            Assert.NotNull(response._rev);
            Assert.NotNull(updateResponse._rev);
            Assert.NotEqual(response._rev, updateResponse._rev);

            Assert.NotNull(updateResponse.New);
        }

        [Fact]
        public async Task PutDocument_ShouldUseQueryParameters_WhenProvided()
        {
            var mockTransport = new Mock<IApiClientTransport>();

            var mockResponse = new Mock<IApiClientResponse>();

            var mockResponseContent = new Mock<IApiClientResponseContent>();

            mockResponse.Setup(x => x.Content)
                .Returns(mockResponseContent.Object);

            mockResponse.Setup(x => x.IsSuccessStatusCode)
                .Returns(true);

            string requestUri = null;

            mockTransport.Setup(x => x.PutAsync(It.IsAny<string>(), It.IsAny<byte[]>(),
                It.IsAny<WebHeaderCollection>(),
                It.IsAny<CancellationToken>()))
                .Returns((string uri, byte[] content, WebHeaderCollection webHeaderCollection, CancellationToken token) =>
                {
                    requestUri = uri;
                    return Task.FromResult(mockResponse.Object);
                });

            var client = new DocumentApiClient(mockTransport.Object);

            await client.PutDocumentAsync(
                "mycollection/0123456789",
                new { },
                new PutDocumentQuery
                {
                    IgnoreRevs = true,
                    ReturnOld = true,
                    ReturnNew = true,
                    Silent = true,
                    WaitForSync = true
                });

            Assert.NotNull(requestUri);
            Assert.Contains("ignoreRevs=true", requestUri);
            Assert.Contains("returnOld=true", requestUri);
            Assert.Contains("returnNew=true", requestUri);
            Assert.Contains("silent=true", requestUri);
            Assert.Contains("waitForSync=true", requestUri);
        }

        [Fact]
        public async Task PutDocument_ShouldThrow_WhenConflictingWriteAttempted_WithIgnoreRevsOptionFalse()
        {
            var doc1 = new { _key = "test", stuff = "test" };
            var response = await _docClient.PostDocumentAsync(_testCollection, doc1);

            var updateResponse1 = await _docClient.PutDocumentAsync(
                response._id,
                new { stuff = "new" },
                new PutDocumentQuery
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
                    new PutDocumentQuery
                    {
                        IgnoreRevs = false
                    }));

            Assert.Equal(1200, ex.ApiError.ErrorNum); // 1200: ERROR_ARANGO_CONFLICT
        }

        [Fact]
        public async Task PutDocuments_ShouldSucceed()
        {
            var response = await _docClient.PostDocumentsAsync(_testCollection,
                new[] {
                    new { value = 1 },
                    new { value = 2 }
                });

            var updateResponse = await _docClient.PutDocumentsAsync(_testCollection,
                new[]
                {
                    new { response[0]._key, value = 3 },
                    new { response[1]._key, value = 4 }
                });

            Assert.NotNull(response[0]._rev);
            Assert.NotNull(updateResponse[0]._rev);
            Assert.NotNull(updateResponse[0]._oldRev);
            Assert.NotEqual(response[0]._rev, updateResponse[0]._rev);
            Assert.Equal(response[0]._rev, updateResponse[0]._oldRev);

            Assert.NotNull(response[1]._rev);
            Assert.NotNull(updateResponse[1]._rev);
            Assert.NotNull(updateResponse[1]._oldRev);
            Assert.NotEqual(response[1]._rev, updateResponse[1]._rev);
            Assert.Equal(response[1]._rev, updateResponse[1]._oldRev);
        }

        [Fact]
        public async Task PutDocumentsAsync_ShouldUseQueryParameters_WhenProvided()
        {
            var mockTransport = new Mock<IApiClientTransport>();

            var mockResponse = new Mock<IApiClientResponse>();

            var mockResponseContent = new Mock<IApiClientResponseContent>();

            mockResponse.Setup(x => x.Content)
                .Returns(mockResponseContent.Object);

            mockResponse.Setup(x => x.IsSuccessStatusCode)
                .Returns(true);

            string requestUri = null;

            mockTransport.Setup(x => x.PutAsync(It.IsAny<string>(), It.IsAny<byte[]>(),
                It.IsAny<WebHeaderCollection>(),
                It.IsAny<CancellationToken>()))
                .Returns((string uri, byte[] content, WebHeaderCollection webHeaderCollection, CancellationToken token) =>
                {
                    requestUri = uri;
                    return Task.FromResult(mockResponse.Object);
                });

            var client = new DocumentApiClient(mockTransport.Object);

            await client.PutDocumentsAsync<object>(
                "mycollection",
                new[]
                {
                    new { Value = 1, Name = "test1" }
                },
                new PutDocumentsQuery
                {
                    IgnoreRevs = true,
                    ReturnOld = true,
                    Silent = true,
                    WaitForSync = true,
                    ReturnNew = true
                });

            Assert.NotNull(requestUri);
            Assert.Contains("ignoreRevs=true", requestUri);
            Assert.Contains("returnOld=true", requestUri);
            Assert.Contains("silent=true", requestUri);
            Assert.Contains("waitForSync=true", requestUri);
            Assert.Contains("returnNew=true", requestUri);
        }

        [Fact]
        public async Task PutDocumentsAsync_ShouldSucceed_WhenSilent()
        {
            var postResponse = await _docClient.PostDocumentsAsync(
                _testCollection,
                new[]
                {
                    new { value = 1 },
                    new { value = 2 }
                });

            var putResponse = await _docClient.PutDocumentsAsync(
                _testCollection,
                new[]
                {
                    new { postResponse[0]._key, value = 3 },
                    new { postResponse[1]._key, value = 4 }
                },
                new PutDocumentsQuery()
                {
                    Silent = true
                });

            Assert.Empty(putResponse);
        }

        [Fact]
        public async Task PutDocuments_ShouldNotThrowButReturnError_WhenDocumentIsNotFound()
        {
            var response = await _docClient.PostDocumentsAsync(_testCollection,
                new[] {
                    new { value = 1 },
                    new { value = 2 }
                });

            var updateResponse = await _docClient.PutDocumentsAsync(_testCollection,
                new[]
                {
                    new { _key = "nonsense", value = 3 },
                    new { response[1]._key, value = 4 }
                });

            Assert.True(updateResponse[0].Error);
            Assert.Equal(NOT_FOUND_NUM, updateResponse[0].ErrorNum);
        }

        [Fact]
        public async Task PatchDocumentsAsync_ShouldSucceed()
        {
            var postResponse = await _docClient.PostDocumentsAsync(_testCollection,
               new[] {
                    new { Value = 1, Name = "test1" },
                    new { Value = 2, Name = "test2" },
                    new { Value = 3, Name = "test3" }
               }, new PostDocumentsQuery
               {
                   ReturnNew = true,
                   WaitForSync = true
               });

            var response = await _docClient.PatchDocumentsAsync<object, PatchDocumentsMockModel>(_testCollection,
                new[] {
                    new { postResponse[0]._key, Name = "test5" },
                    new { postResponse[1]._key, Name = "test4" }
                    }, new PatchDocumentsQuery
                    {
                        ReturnNew = true,
                        WaitForSync = true,
                        ReturnOld = true
                    });

            Assert.Equal(2, response.Count);
            Assert.NotEqual(postResponse[0]._rev, response[0]._rev);
            Assert.NotEqual(postResponse[0].New.Name, response[0].New.Name);
            Assert.Equal(postResponse[0].New.Name, response[0].Old.Name);
            Assert.Equal(postResponse[0]._rev, response[0]._oldRev);
        }

        [Fact]
        public async Task PatchDocumentsAsync_ShouldUseQueryParameters_WhenProvided()
        {
            var mockTransport = new Mock<IApiClientTransport>();

            var mockResponse = new Mock<IApiClientResponse>();

            var mockResponseContent = new Mock<IApiClientResponseContent>();

            mockResponse.Setup(x => x.Content)
                .Returns(mockResponseContent.Object);

            mockResponse.Setup(x => x.IsSuccessStatusCode)
                .Returns(true);

            string requestUri = null;

            mockTransport.Setup(x => x.PatchAsync(
                It.IsAny<string>(), It.IsAny<byte[]>(),
                It.IsAny<WebHeaderCollection>(),
                It.IsAny<CancellationToken>()))
                .Returns((string uri, byte[] content, WebHeaderCollection webHeaderCollection, CancellationToken token) =>
                {
                    requestUri = uri;
                    return Task.FromResult(mockResponse.Object);
                });

            var client = new DocumentApiClient(mockTransport.Object);

            await client.PatchDocumentsAsync<object, object>(
                "mycollection",
                new[]
                {
                    new { Value = 1, Name = "test1" },
                    new { Value = 2, Name = "test2" },
                    new { Value = 3, Name = "test3" }
                },
                new PatchDocumentsQuery
                {
                    IgnoreRevs = true,
                    ReturnOld = true,
                    Silent = true,
                    WaitForSync = true,
                    KeepNull = true,
                    MergeObjects = true,
                    ReturnNew = true
                });

            Assert.NotNull(requestUri);
            Assert.Contains("ignoreRevs=true", requestUri);
            Assert.Contains("returnOld=true", requestUri);
            Assert.Contains("silent=true", requestUri);
            Assert.Contains("waitForSync=true", requestUri);
            Assert.Contains("keepNull=true", requestUri);
            Assert.Contains("mergeObjects=true", requestUri);
            Assert.Contains("returnNew=true", requestUri);
        }

        [Fact]
        public async Task PatchDocumentsAsync_ShouldSucceed_WhenSilent()
        {
            var postResponse = await _docClient.PostDocumentsAsync(
                _testCollection,
                new[]
                {
                    new { Value = 1, Name = "test1" },
                    new { Value = 2, Name = "test2" },
                    new { Value = 3, Name = "test3" }
                },
                new PostDocumentsQuery
                {
                    ReturnNew = true,
                    WaitForSync = true
                });

            var response = await _docClient.PatchDocumentsAsync<object, PatchDocumentsMockModel>(
                _testCollection,
                new[]
                {
                    new { postResponse[0]._key, Name = "test5" },
                    new { postResponse[1]._key, Name = "test4" }
                },
                new PatchDocumentsQuery
                {
                    Silent = true
                });

            Assert.Empty(response);
        }

        [Fact]
        public async Task PatchDocumentsAsync_ShouldReturnError_WhenDocumentDoesNotExist()
        {
            var response = await _docClient.PatchDocumentsAsync<object, PatchDocumentsMockModel>(
                _testCollection,
                new[] { new { _key = "bogusDocument", value = 4 } },
                null);

            Assert.True(response[0].Error);
            Assert.Equal(1202, response[0].ErrorNum); // ARANGO_DOCUMENT_NOT_FOUND
            Assert.NotNull(response[0].ErrorMessage);
        }

        [Fact]
        public async Task PatchDocumentAsync_ShouldSucceed()
        {
            var addDocResponse = await _docClient.PostDocumentsAsync(_testCollection,
                new[] {
                    new { value = 1, name = "test1" },
                    new { value = 2, name = "test2" }
                }, new PostDocumentsQuery
                {
                    ReturnNew = true,
                    ReturnOld = true,
                    WaitForSync = true
                });

            var response = await _docClient.PatchDocumentAsync<object, PatchDocumentMockModel>(_testCollection, addDocResponse[0]._key, new
            {
                addDocResponse[0]._key,
                value = 3
            }, new PatchDocumentQuery
            {
                ReturnNew = true,
                ReturnOld = true,
                WaitForSync = true
            });

            Assert.Equal(addDocResponse[0]._rev, response._oldRev);
            Assert.NotEqual(addDocResponse[0]._rev, response._rev);
            Assert.Equal(addDocResponse[0]._key, response._key);
            Assert.Equal(addDocResponse[0].New.value, response.Old.value);
            Assert.NotEqual(addDocResponse[0].New.value, response.New.value);
            Assert.Equal(addDocResponse[0].New.name, response.New.name);
        }

        [Fact]
        public async Task PatchDocumentAsync_ShouldReturnNullResponse_WhenSilentIsTrue()
        {
            var addDocResponse = await _docClient.PostDocumentsAsync(_testCollection,
                new[] {
                    new { value = 1, name = "test1" },
                    new { value = 2 , name = "test2"}
                }, new PostDocumentsQuery
                {
                    ReturnNew = true,
                    ReturnOld = true,
                    WaitForSync = true
                });

            var response = await _docClient.PatchDocumentAsync<object, PatchDocumentMockModel>(_testCollection, addDocResponse[0]._key, new
            {
                addDocResponse[0]._key,
                value = 3
            }, new PatchDocumentQuery
            {
                ReturnNew = true,
                ReturnOld = true,
                WaitForSync = true,
                Silent = true
            });

            Assert.Null(response.Old);
            Assert.Null(response.New);
            Assert.Null(response._oldRev);
            Assert.Null(response._id);
            Assert.Null(response._key);
            Assert.Null(response._rev);
        }

        [Fact]
        public async Task PatchDocumentAsync_ShouldThrowBadRequest_WhenJsonIsInvalid()
        {
            var addDocResponse = await _docClient.PostDocumentsAsync(_testCollection,
                new[] {
                    new { value = 1 },
                    new { value = 2 }
                });

            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _docClient.PatchDocumentAsync<object, PatchDocumentMockModel>(_testCollection, addDocResponse[0]._key, new
                {
                    _key = 1351.3,
                    bogusProp = "bogusProp"
                }, new PatchDocumentQuery
                {
                    WaitForSync = true
                });
            });
            Assert.Equal(HttpStatusCode.BadRequest, ex.ApiError.Code);
            Assert.Equal(1221, ex.ApiError.ErrorNum); // ARANGO_DOCUMENT_KEY_BAD
        }

        [Fact]
        public async Task PatchDocumentAsync_ShouldThrowNotFound_WhenCollectionDoesNotExist()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _docClient.PatchDocumentAsync<object, PatchDocumentMockModel>("BogusCollection", "12345", new
                {
                    _key = 1351.3,
                    bogusProp = "bogusProp"
                });
            });
            Assert.Equal(HttpStatusCode.NotFound, ex.ApiError.Code);
            Assert.Equal(1203, ex.ApiError.ErrorNum); // ARANGO_DATA_SOURCE_NOT_FOUND
        }

        [Fact]
        public async Task ReadDocumentHeaderAsync_ShouldSucceed()
        {
            Dictionary<string, object> document = new Dictionary<string, object> { ["key"] = "value" };
            var docResponse = await _docClient.PostDocumentAsync(_testCollection, document);
            var response = await _docClient.HeadDocumentAsync(_testCollection, docResponse._key);

            Assert.Equal(HttpStatusCode.OK, response.Code);
        }

        [Fact]
        public async Task ReadDocumentHeaderAsync_ShouldReturnNotModified_WhenIfNoneMatchIsGivenAndVersionIsTheSame()
        {
            Dictionary<string, object> document = new Dictionary<string, object> { ["key"] = "value" };
            var docResponse = await _docClient.PostDocumentAsync(_testCollection, document);
            var response = await _docClient.HeadDocumentAsync(_testCollection, docResponse._key, new DocumentHeaderProperties
            {
                IfNoneMatch = docResponse._rev
            });

            Assert.Equal(HttpStatusCode.NotModified, response.Code);
            Assert.Equal($"\"{docResponse._rev}\"", response.Etag.Tag);
        }

        [Fact]
        public async Task ReadDocumentHeaderAsync_ShouldReturnPreconditionFailed_WhenIfMatchIsGivenAndRevisionIsDifferent()
        {
            Dictionary<string, object> document = new Dictionary<string, object> { ["key"] = "value" };
            // create the doc
            var docResponse = await _docClient.PostDocumentAsync(_testCollection, document);
            // Change the revision
            var updateDocResponse = await _docClient.PutDocumentAsync($"{_testCollection}/{docResponse._key}", new Dictionary<string, object>
            {
                ["key"] = "newValue"
            });
            // check if revision has changed
            var response = await _docClient.HeadDocumentAsync(_testCollection, docResponse._key, new DocumentHeaderProperties
            {
                IfMatch = docResponse._rev
            });

            Assert.Equal(HttpStatusCode.PreconditionFailed, response.Code);
            Assert.NotEqual($"\"{docResponse._rev}\"", response.Etag.Tag);
        }

        [Fact]
        [Trait("Feature", "StreamTransaction")]
        public async Task ReadDocumentHeaderAsync_ShouldReturnOk_WhenTransactionIdIsGivenAndIsTheSame()
        {
            // Post a single document.
            var docResponse =
                await _docClient.PostDocumentAsync(_testCollection, new Dictionary<string, object> { ["key"] = "value" });

            // Begin a transaction.
            var beginTransaction = await _adb.Transaction.BeginTransaction(
                new StreamTransactionBody
                {
                    Collections = new PostTransactionRequestCollections
                    {
                        Write = new[] { _testCollection }
                    }
                });

            // Get the header fields.
            var response = await _docClient.HeadDocumentAsync(
                _testCollection,
                docResponse._key,
                new DocumentHeaderProperties { TransactionId = beginTransaction.Result.Id });

            // Check for the expected status.
            Assert.Equal(HttpStatusCode.OK, response.Code);

            // Abort the transaction.
            await _adb.Transaction.AbortTransaction(beginTransaction.Result.Id);
        }

        [Fact]
        [Trait("Feature", "StreamTransaction")]
        public async Task ReadDocumentHeaderAsync_ShouldReturnNotFound_WhenTransctionIdIsGiveAndIsNotTheSame()
        {
            string dummyTransactionId = "Bogus transaction Id";

            // Post a single document.
            var docResponse =
                await _docClient.PostDocumentAsync(_testCollection, new Dictionary<string, object> { ["key"] = "value" });

            // Get the header fields.
            var response = await _docClient.HeadDocumentAsync(
                _testCollection,
                docResponse._key,
                new DocumentHeaderProperties { TransactionId = dummyTransactionId });

            // Check for the expected status.
            Assert.Equal(HttpStatusCode.BadRequest, response.Code);
        }

        [Fact]
        public async Task ReadDocumentHeaderAsync_ShouldReturnNotFound_WhenCollectionDoesNotExist()
        {
            var response = await _docClient.HeadDocumentAsync("bogusCollection", "123456");

            Assert.Equal(HttpStatusCode.NotFound, response.Code);
        }
    }
}
