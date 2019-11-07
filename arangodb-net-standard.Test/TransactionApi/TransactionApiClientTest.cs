using System.Collections.Generic;
using System.Threading.Tasks;
using ArangoDBNetStandard;
using ArangoDBNetStandard.DocumentApi;
using ArangoDBNetStandard.TransactionApi;
using Xunit;

namespace ArangoDBNetStandardTest.TransactionApi
{
    public class TransactionApiClientTest: IClassFixture<TransactionApiClientTestFixture>
    {
        private ArangoDBClient _adb;

        public TransactionApiClientTest(TransactionApiClientTestFixture fixture)
        {
            _adb = fixture.ArangoDBClient;
            _adb.Collection.PutCollectionTruncateAsync(fixture.TestCollection1).Wait();
            _adb.Collection.PutCollectionTruncateAsync(fixture.TestCollection2).Wait();
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

            var result = await _adb.Transaction.PostTransactionAsync<List<DocumentBase>>(new PostTransactionRequest
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

            Assert.Equal("Hello, world", (string)doc1.message);
            Assert.Equal("Hello, love", (string)doc2.message);
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
            Assert.Equal(ErrorCode.BAD_PARAMETER, ex.ApiError.ErrorNum);
        }

        [Fact]
        public async Task PostTransaction_ShouldThrow_WhenWriteCollectionIsNotDeclared()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                await _adb.Transaction.PostTransactionAsync<object>(new PostTransactionRequest
                {
                    Action = "function (params) { console.log('This is a test'); }"
                }));
            Assert.Equal(ErrorCode.BAD_PARAMETER, ex.ApiError.ErrorNum);
        }
    }
}
