using System.Threading;
using System.Threading.Tasks;
using ArangoDBNetStandard.TransactionApi.Models;

namespace ArangoDBNetStandard.TransactionApi
{
    /// <summary>
    /// Defines a client to access the ArangoDB Transactions API.
    /// </summary>
    public interface ITransactionApiClient
    {
        /// <summary>
        /// Abort a stream transaction by DELETE.
        /// </summary>
        /// /// <remarks>
        /// https://www.arangodb.com/docs/stable/http/transaction-stream-transaction.html#abort-transaction
        /// </remarks>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <exception cref="ApiErrorException">
        /// With ErrorNum 1653 if the transaction cannot be aborted.
        /// With ErrorNum 10 if the transaction is not found.
        /// </exception>
        /// <returns>Response from ArangoDB after aborting a transaction.</returns>
        Task<StreamTransactionResponse> AbortTransaction(string transactionId,
            CancellationToken token = default);

        /// <summary>
        /// Begin a stream transaction by POST.
        /// </summary>
        /// <remarks>
        /// https://www.arangodb.com/docs/stable/http/transaction-stream-transaction.html#begin-a-transaction
        /// </remarks>
        /// <param name="body">Object containing information to submit in the POST stream transaction request.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <exception cref="ApiErrorException">
        /// With ErrorNum 10 if the <paramref name="body"/> is missing or malformed.
        /// With ErrorNum 1203 if the <paramref name="body"/> contains an unknown collection.
        /// </exception>
        /// <returns>Response from ArangoDB after beginning a transaction.</returns>
        Task<StreamTransactionResponse> BeginTransaction(StreamTransactionBody body,
            CancellationToken token = default);

        /// <summary>
        /// Commit a transaction by PUT.
        /// </summary>
        /// <remarks>
        /// https://www.arangodb.com/docs/stable/http/transaction-stream-transaction.html#commit-transaction
        /// </remarks>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <exception cref="ApiErrorException">
        /// With ErrorNum 1653 if the transaction cannot be committed.
        /// With ErrorNum 10 if the transaction is not found.
        /// </exception>
        /// <returns>Response from ArangoDB after committing a transaction.</returns>
        Task<StreamTransactionResponse> CommitTransaction(string transactionId,
            CancellationToken token = default);

        /// <summary>
        /// Get currently running transactions.
        /// </summary>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <remarks>
        /// https://www.arangodb.com/docs/stable/http/transaction-stream-transaction.html#get-currently-running-transactions
        /// </remarks>
        /// <returns>Response from ArangoDB with all running transactions.</returns>
        Task<StreamTransactions> GetAllRunningTransactions(
            CancellationToken token = default);

        /// <summary>
        /// Get the status of a transaction.
        /// </summary>
        /// <remarks>
        /// https://www.arangodb.com/docs/stable/http/transaction-stream-transaction.html#get-transaction-status
        /// </remarks>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <exception cref="ApiErrorException">With ErrorNum 10 if the transaction is not found.</exception>
        /// <returns>Response from ArangoDB with the status of a transaction.</returns>
        Task<StreamTransactionResponse> GetTransactionStatus(string transactionId,
            CancellationToken token = default);

        /// <summary>
        /// POST a transaction to ArangoDB.
        /// </summary>
        /// <typeparam name="T">Type to use for deserializing the object returned by the transaction function.</typeparam>
        /// <param name="body">Object containing information to submit in the POST transaction request.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns>Response from ArangoDB after processing the request.</returns>
        Task<PostTransactionResponse<T>> PostTransactionAsync<T>(PostTransactionBody body,
            CancellationToken token = default);
    }
}
