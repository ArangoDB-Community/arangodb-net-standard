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
        /// https://www.arangodb.com/docs/3.6/http/transaction-stream-transaction.html#abort-transaction.
        /// </remarks>
        /// <typeparam name="TStreamTransactionResult">Type to use for deserializing the object returned by the transaction function.</typeparam>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns>Response from ArangoDB after aborting a transaction.</returns>
        Task<PostTransactionResponse<TStreamTransactionResult>> AbortTransaction<TStreamTransactionResult>(
            string transactionId);

        /// <summary>
        /// Begin a stream transaction by POST.
        /// </summary>
        /// <remarks>
        /// https://www.arangodb.com/docs/3.6/http/transaction-stream-transaction.html#begin-a-transaction.
        /// </remarks>
        /// <typeparam name="TStreamTransactionResult">Type to use for deserializing the object returned by the transaction function.</typeparam>
        /// <param name="body">Object containing information to submit in the POST stream transaction request.</param>
        /// <returns>Response from ArangoDB after beginning a transaction.</returns>
        Task<PostTransactionResponse<TStreamTransactionResult>> BeginTransaction<TStreamTransactionResult>(
            StreamTransactionBody body);

        /// <summary>
        /// Commit a transaction by PUT.
        /// </summary>
        /// <remarks>
        /// https://www.arangodb.com/docs/3.6/http/transaction-stream-transaction.html#commit-transaction.
        /// </remarks>
        /// <typeparam name="TStreamTransactionResult">Type to use for deserializing the object returned by the transaction function.</typeparam>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns>Response from ArangoDB after committing a transaction.</returns>
        Task<PostTransactionResponse<TStreamTransactionResult>> CommitTransaction<TStreamTransactionResult>(
            string transactionId);

        /// <summary>
        /// Get currently running transactions.
        /// </summary>
        /// <remarks>
        /// https://www.arangodb.com/docs/3.6/http/transaction-stream-transaction.html#get-currently-running-transactions.
        /// </remarks>
        /// <returns>Response from ArangoDB with all running transactions.</returns>
        Task<StreamTransactions> GetAllRunningTransactions();

        /// <summary>
        /// Get the status of a transaction.
        /// </summary>
        /// <remarks>
        /// https://www.arangodb.com/docs/3.6/http/transaction-stream-transaction.html#get-transaction-status.
        /// </remarks>
        /// <typeparam name="TStreamTransactionResult">Type to use for deserializing the object returned by the transaction function.</typeparam>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns>Response from ArangoDB with the status of a transaction.</returns>
        Task<PostTransactionResponse<TStreamTransactionResult>> GetTransactionStatus<TStreamTransactionResult>(
            string transactionId);

        /// <summary>
        /// POST a transaction to ArangoDB.
        /// </summary>
        /// <typeparam name="T">Type to use for deserializing the object returned by the transaction function.</typeparam>
        /// <param name="body">Object containing information to submit in the POST transaction request.</param>
        /// <returns>Response from ArangoDB after processing the request.</returns>
        Task<PostTransactionResponse<T>> PostTransactionAsync<T>(PostTransactionBody body);
    }
}
