using System.Collections.Generic;
using System.Threading.Tasks;
using ArangoDBNetStandard;
using ArangoDBNetStandard.DocumentApi.Models;
using ArangoDBNetStandard.TransactionApi.Models;
using Xunit;

namespace ArangoDBNetStandardTest.TransactionApi
{
    /// <summary>
    /// The Transaction Api Client test class.
    /// </summary>
    public class TransactionApiClientTest : IClassFixture<TransactionApiClientTestFixture>
    {
        /// <summary>
        /// The Arango database client.
        /// </summary>
        private readonly ArangoDBClient _adb;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionApiClientTest"/> class.
        /// </summary>
        /// <param name="fixture">The test repository fixture with the blank database etc.</param>
        public TransactionApiClientTest(TransactionApiClientTestFixture fixture)
        {
            _adb = fixture.ArangoDBClient;
            _adb.Collection.TruncateCollectionAsync(fixture.TestCollection1).Wait();
            _adb.Collection.TruncateCollectionAsync(fixture.TestCollection2).Wait();
        }

        /// <summary>
        /// Tests that the stream transaction gets aborted successfully.
        /// </summary>
        [Fact]
        public async Task AbortTransaction_ShouldSucceed()
        {
            var beginTransaction = await _adb.Transaction.BeginTransaction<StreamTransactionResult>(
                new StreamTransactionBody
                {
                    Collections = new PostTransactionRequestCollections
                    {
                        Write = new[] { "TestCollection2" },
                    },
                });

            var dummyCollectionUpdate = await _adb.Document.PostDocumentAsync(
                "TestCollection2",
                new
                {
                    _key = "names",
                    value = new[] { "world", "love" },
                });

            // Abort the transaction.
            var result =
                await _adb.Transaction.AbortTransaction<StreamTransactionResult>(beginTransaction.Result.Id);

            // Check for the correct transaction status.
            Assert.Equal(StreamTransactionStatus.Aborted, result.Result.Status);

            // Check the collection is not updated yet.
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _adb.Document.GetDocumentAsync<object>(dummyCollectionUpdate._id);
            });

            Assert.Equal(1202, ex.ApiError.ErrorNum);
        }

        /// <summary>
        /// Tests that when the existing collections are used to begin a transaction, the stream transaction is running.
        /// </summary>
        [Fact]
        public async Task BeginTransaction_ShouldSucceed()
        {
            var result = await _adb.Transaction.BeginTransaction<StreamTransactionResult>(
                new StreamTransactionBody
                {
                    Collections = new PostTransactionRequestCollections
                    {
                        Write = new[] { "TestCollection2" },
                    },
                });

            // Check for the correct transaction status.
            Assert.Equal(StreamTransactionStatus.Running, result.Result.Status);

            var dummyCollectionUpdate = await _adb.Document.PostDocumentAsync(
            "TestCollection2",
            new
            {
                _key = "names",
                value = new[] { "world", "love" },
            });

            // Check the collection is not updated yet.
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _adb.Document.GetDocumentAsync<object>(dummyCollectionUpdate._id);
            });

            Assert.Equal(1202, ex.ApiError.ErrorNum);
        }

        /// <summary>
        /// Tests that when referring to a non existing collection to transact, the stream transaction does not begin.
        /// </summary>
        /// <exception cref="ApiErrorException">With ErrorNum 1203 if the collection is not found.</exception>
        [Fact]
        public async Task BeginTransaction_ShouldThrowException_WhenReferring_To_A_NonExisting_Collection()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                await _adb.Transaction.BeginTransaction<object>(new StreamTransactionBody
                {
                    Collections = new PostTransactionRequestCollections
                    {
                        Read = new[] { "SomeCollection" },
                        Write = new[] { "TestCollection2" },
                    },
                }));

            Assert.Equal(1203, ex.ApiError.ErrorNum);
        }

        /// <summary>
        /// Tests that the stream transaction gets committed successfully.
        /// </summary>
        [Fact]
        public async Task CommitTransaction_ShouldSucceed()
        {
            var beginTransaction = await _adb.Transaction.BeginTransaction<StreamTransactionResult>(
                new StreamTransactionBody
                {
                    Collections = new PostTransactionRequestCollections
                    {
                        Write = new[] { "TestCollection2" },
                    },
                });

            var dummyCollectionUpdate = await _adb.Document.PostDocumentAsync(
                    "TestCollection2",
                    new
                    {
                        _key = "names",
                        value = new[] { "world", "love" },
                    });

            // Check the collection is not updated yet.
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _adb.Document.GetDocumentAsync<object>(dummyCollectionUpdate._id);
            });

            Assert.Equal(1202, ex.ApiError.ErrorNum);

            // Commit a transaction.
            var result =
                await _adb.Transaction.CommitTransaction<StreamTransactionResult>(beginTransaction.Result.Id);

            // Check for the correct transaction status.
            Assert.Equal(StreamTransactionStatus.Committed, result.Result.Status);

            // Check that the collection is now updated.
            var latestCollectionUpdate = await _adb.Document.GetDocumentAsync<object>(dummyCollectionUpdate._id);

            // Check that the document exists.
            Assert.NotNull(latestCollectionUpdate);
        }

        /// <summary>
        /// Tests that all running transactions are returned successfully.
        /// </summary>
        [Fact]
        public async Task GetAllRunningTransactions_ShouldSucceed()
        {
            var firstTransaction = await _adb.Transaction.BeginTransaction<StreamTransactionResult>(
                    new StreamTransactionBody
                    {
                        Collections = new PostTransactionRequestCollections
                        {
                            Write = new[] { "TestCollection1" },
                        },
                    });

            var secondTransaction = await _adb.Transaction.BeginTransaction<StreamTransactionResult>(
                    new StreamTransactionBody
                    {
                        Collections = new PostTransactionRequestCollections
                        {
                            Write = new[] { "TestCollection2" },
                        },
                    });

            // Get all running transactions.
            var result = await _adb.Transaction.GetAllRunningTransactions();

            // Check for all the running transactions.
            Assert.Equal(2, result.Transactions.Count);
            Assert.Contains(result.Transactions, x => x.State.Equals(StreamTransactionStatus.Running));
        }

        /// <summary>
        /// Tests that the status of a transaction is returned successfully.
        /// </summary>
        [Fact]
        public async Task GetTransactionStatus_ShouldSucceed()
        {
            var firstTransaction = await _adb.Transaction.BeginTransaction<StreamTransactionResult>(
                        new StreamTransactionBody
                        {
                            Collections = new PostTransactionRequestCollections
                            {
                                Write = new[] { "TestCollection1" },
                            },
                        });

            // Get the transaction status.
            var transaction =
                await _adb.Transaction.GetTransactionStatus<StreamTransactionResult>(firstTransaction.Result.Id);

            // Check for the correct transaction status.
            Assert.Equal(firstTransaction.Result.Status, transaction.Result.Status);
        }

        /// <summary>
        /// Tests that a post JS transaction succeeds.
        /// </summary>
        [Fact]
        public async Task PostTransaction_ShouldSucceed()
        {
            await _adb.Document.PostDocumentAsync(
                "TestCollection2",
                new
                {
                    _key = "names",
                    value = new[] { "world", "love" },
                });

            var result = await _adb.Transaction.PostTransactionAsync<List<DocumentBase>>(new PostTransactionBody
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
                    Write = new[] { "TestCollection1" },
                },
            });

            Assert.Equal(2, result.Result.Count);

            var doc1 = await _adb.Document.GetDocumentAsync<dynamic>(result.Result[0]._id);
            var doc2 = await _adb.Document.GetDocumentAsync<dynamic>(result.Result[1]._id);

            Assert.Equal("Hello, world", (string)doc1.message);
            Assert.Equal("Hello, love", (string)doc2.message);
        }

        /// <summary>
        /// Tests that when the action in JS transaction has syntax error, an exception is thrown.
        /// </summary>
        /// <exception cref="ApiErrorException">With ErrorNum 10 missing/invalid action definition for transaction.</exception>
        [Fact]
        public async Task PostTransaction_ShouldThrow_WhenFunctionDefinitionHasSyntaxErrors()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                await _adb.Transaction.PostTransactionAsync<object>(new PostTransactionBody
                {
                    Action = "function (params) { syntax error }",
                    Collections = new PostTransactionRequestCollections
                    {
                        Write = new[] { "test" },
                    },
                }));
            Assert.Equal(10, ex.ApiError.ErrorNum);
        }

        /// <summary>
        /// Tests that when there is no collection, an exception is thrown.
        /// </summary>
        /// <exception cref="ApiErrorException">With ErrorNum 10 missing/invalid collections definition for transaction.</exception>
        [Fact]
        public async Task PostTransaction_ShouldThrow_WhenWriteCollectionIsNotDeclared()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                await _adb.Transaction.PostTransactionAsync<object>(new PostTransactionBody
                {
                    Action = "function (params) { console.log('This is a test'); }",
                }));
            Assert.Equal(10, ex.ApiError.ErrorNum);
        }
    }
}
