using System.Collections.Generic;
using System.Linq;
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
        [Trait("Feature", "StreamTransaction")]
        public async Task AbortTransaction_ShouldSucceed()
        {
            // Begin a new transaction.
            var beginTransaction = await _adb.Transaction.BeginTransaction(
                new StreamTransactionBody
                {
                    Collections = new PostTransactionRequestCollections
                    {
                        Write = new[] { "TestCollection1" },
                    },
                });

            // Abort the transaction.
            var abortTransaction = await _adb.Transaction.AbortTransaction(beginTransaction.Result.Id);

            // Check for the correct transaction status.
            Assert.Equal(StreamTransactionStatus.Aborted, abortTransaction.Result.Status);
        }

        /// <summary>
        /// Test that an exception is thrown when trying to abort a transaction that does not exist.
        /// </summary>
        /// <exception cref="ApiErrorException">With ErrorNum 10 if the transaction is not found.</exception>
        [Fact]
        [Trait("Feature", "StreamTransaction")]
        public async Task AbortTransaction_ShouldThrowException_WhenTheTransactionIsNotFound()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _adb.Transaction.AbortTransaction("Some Bogus Transaction Id");
            });

            // Check for the correct error number.
            Assert.Equal(10, ex.ApiError.ErrorNum);
        }

        /// <summary>
        /// Tests that when the existing collections are used to begin a transaction, the stream transaction is running.
        /// </summary>
        [Fact]
        [Trait("Feature", "StreamTransaction")]
        public async Task BeginTransaction_ShouldSucceed()
        {
            // Begin a transaction.
            var beginTransaction = await _adb.Transaction.BeginTransaction(
                new StreamTransactionBody
                {
                    Collections = new PostTransactionRequestCollections
                    {
                        Write = new[] { "TestCollection1" },
                    },
                });

            // Check for the correct transaction status.
            Assert.Equal(StreamTransactionStatus.Running, beginTransaction.Result.Status);
        }

        /// <summary>
        /// Tests that when referring to a non existing collection to transact, the stream transaction does not begin.
        /// </summary>
        /// <exception cref="ApiErrorException">With ErrorNum 1203 if the collection is not found.</exception>
        [Fact]
        [Trait("Feature", "StreamTransaction")]
        public async Task BeginTransaction_ShouldThrowException_WhenReferringToANonExistingCollection()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                await _adb.Transaction.BeginTransaction(
                    new StreamTransactionBody
                    {
                        Collections = new PostTransactionRequestCollections
                        {
                            Read = new[] { "SomeCollection" },
                            Write = new[] { "TestCollection1" },
                        },
                    }));

            // Check for the correct error number.
            Assert.Equal(1203, ex.ApiError.ErrorNum);
        }

        /// <summary>
        /// Tests that the stream transaction gets committed successfully.
        /// </summary>
        [Fact]
        [Trait("Feature", "StreamTransaction")]
        public async Task CommitTransaction_ShouldSucceed()
        {
            // Begin a new transaction.
            var beginTransaction = await _adb.Transaction.BeginTransaction(
                new StreamTransactionBody
                {
                    Collections = new PostTransactionRequestCollections
                    {
                        Write = new[] { "TestCollection1" },
                    },
                });

            // Commit the transaction.
            var result = await _adb.Transaction.CommitTransaction(beginTransaction.Result.Id);

            // Check for the correct transaction status.
            Assert.Equal(StreamTransactionStatus.Committed, result.Result.Status);
        }

        /// <summary>
        /// Test that an exception is thrown when trying to commit a transaction that does not exist.
        /// </summary>
        /// <exception cref="ApiErrorException">With ErrorNum 10 if the transaction is not found.</exception>
        [Fact]
        [Trait("Feature", "StreamTransaction")]
        public async Task CommitTransaction_ShouldThrowException_WhenTheTransactionIsNotFound()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _adb.Transaction.CommitTransaction("Some Bogus Transaction Id");
            });

            // Check for the correct error number.
            Assert.Equal(10, ex.ApiError.ErrorNum);
        }

        /// <summary>
        /// Tests that all running transactions are returned successfully.
        /// </summary>
        [Fact]
        [Trait("Feature", "StreamTransaction")]
        public async Task GetAllRunningTransactions_ShouldSucceed()
        {
            // Begin the first transaction.
            var firstTransaction = await _adb.Transaction.BeginTransaction(
                new StreamTransactionBody
                {
                    Collections = new PostTransactionRequestCollections
                    {
                        Write = new[] { "TestCollection1" },
                    },
                });

            // Begin the second transaction.
            var secondTransaction = await _adb.Transaction.BeginTransaction(
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
            Assert.Equal(2, result.Transactions.Count());
            Assert.Contains(result.Transactions, x => x.State.Equals(StreamTransactionStatus.Running));
        }

        /// <summary>
        /// Tests that when there are no running transactions, an empty list of <see cref="Transaction"/> is returned.
        /// </summary>
        [Fact]
        [Trait("Feature", "StreamTransaction")]
        public async Task GetAllRunningTransactions_ShouldSucceed_WhenThereAreNoRunningTransactions()
        {
            // Get all running transactions.
            var result = await _adb.Transaction.GetAllRunningTransactions();

            // Check for all the running transactions.
            Assert.NotNull(result);
            Assert.Empty(result.Transactions);
        }

        /// <summary>
        /// Tests that the status of a transaction is returned successfully.
        /// </summary>
        [Fact]
        [Trait("Feature", "StreamTransaction")]
        public async Task GetTransactionStatus_ShouldSucceed()
        {
            // Begin the transaction.
            var firstTransaction = await _adb.Transaction.BeginTransaction(
                new StreamTransactionBody
                {
                    Collections = new PostTransactionRequestCollections
                    {
                        Write = new[] { "TestCollection1" },
                    },
                });

            // Get the transaction status.
            var transaction = await _adb.Transaction.GetTransactionStatus(firstTransaction.Result.Id);

            // Check for the correct transaction status.
            Assert.Equal(firstTransaction.Result.Status, transaction.Result.Status);
        }

        /// <summary>
        /// Test that an exception is thrown when trying to get the status of a transaction that does not exist.
        /// </summary>
        /// <exception cref="ApiErrorException">With ErrorNum 10 if the transaction is not found.</exception>
        [Fact]
        [Trait("Feature", "StreamTransaction")]
        public async Task GetTransactionStatus_ShouldThrowException_WhenTheTransctionIdIsNotFound()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
            {
                await _adb.Transaction.GetTransactionStatus("Some Bogus Transaction Id");
            });

            // Check for the correct error number.
            Assert.Equal(10, ex.ApiError.ErrorNum);
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
