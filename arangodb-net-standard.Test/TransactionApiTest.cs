using System.Collections.Generic;
using System.Threading.Tasks;
using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi;
using ArangoDBNetStandard.TransactionApi;
using Xunit;

namespace ArangoDBNetStandardTest
{
    public class TransactionApiTest: ApiTestBase
    {
        private string _dbName = nameof(TransactionApiTest);
        private ArangoDBClient _adb;

        public TransactionApiTest()
        {
            CreateDatabase(_dbName);
            _adb = GetArangoDBClient(_dbName);
            Task.WaitAll(
                _adb.Collection.PostCollectionAsync(new PostCollectionOptions
                {
                    Name = "TestCollection1"
                }),
                _adb.Collection.PostCollectionAsync(new PostCollectionOptions
                {
                    Name = "TestCollection2"
                }));
        }

        [Fact]
        public async Task PostTransaction_ShouldSucceed()
        {
            await _adb.Document.PostDocumentAsync("TestCollection2",
                new
                {
                    _key = "names",
                     value = new[] { "world", "love" }
                });

            var result = await _adb.Transaction.PostTransactionAsync<List<PostDocumentResponse>>(new PostTransactionRequest
            {
                Action = @"
                    function (params) { 
                        const db = require('@arangodb').db;
                        const names = db.TestCollection2.document('names').value;
                        const newDocuments = [];
                        for (let name of names) {
                            const newDoc = db.TestCollection1.insert({ message: params.prefix + name });
                            newDocuments.push(newDoc);
                        }
                        return newDocuments;
                    }",
                Params = new Dictionary<string, object> { ["prefix"] = "Hello, " },
                Collections = new PostTransactionRequestCollections
                {
                    Read = new[] { "TestCollection2" },
                    Write = new[] { "TestCollection1" }
                }
            });

            Assert.Equal(2, result.Result.Count);

            var doc1 = await _adb.Document.GetDocumentAsync<dynamic>(result.Result[0]._id);
            var doc2 = await _adb.Document.GetDocumentAsync<dynamic>(result.Result[1]._id);

            Assert.Equal("Hello, world", (string)doc1.Document.message);
            Assert.Equal("Hello, love", (string)doc2.Document.message);
        }

        [Fact]
        public async Task PostTransaction_ShouldThrow_WhenFunctionDefinitionHasSyntaxErrors()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () => 
                await _adb.Transaction.PostTransactionAsync<object>(new PostTransactionRequest
                {
                    Action = "function (params) { syntax error }",
                    Collections = new PostTransactionRequestCollections
                    {
                        Write = new[] { "test" }
                    }
                }));
            Assert.Equal(10, ex.ApiError.ErrorNum);
        }

        [Fact]
        public async Task PostTransaction_ShouldThrow_WhenWriteCollectionIsNotDeclared()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                await _adb.Transaction.PostTransactionAsync<object>(new PostTransactionRequest
                {
                    Action = "function (params) { console.log('This is a test'); }"
                }));
            Assert.Equal(10, ex.ApiError.ErrorNum);
        }
    }
}
