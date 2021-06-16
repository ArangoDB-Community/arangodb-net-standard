using System;
using System.Threading.Tasks;
using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.TransactionApi.Models;
using ArangoDBNetStandard.Transport;

namespace ArangoDBNetStandard.TransactionApi
{
    /// <summary>
    /// Provides access to ArangoDB transaction API.
    /// </summary>
    public class TransactionApiClient : ApiClientBase, ITransactionApiClient
    {
        /// <summary>
        /// The transport client used to communicate with the ArangoDB host.
        /// </summary>
        private readonly IApiClientTransport _client;

        /// <summary>
        /// The root path of the API.
        /// </summary>
        private readonly string _transactionApiPath = "_api/transaction";

        /// <summary>
        /// The root path of the API to abort, commit a transaction and get the status of a transaction.
        /// </summary>
        private readonly string _streamTransactionApiPath = "_api/transaction/{0}";

        /// <summary>
        /// The root path of the API to begin a transaction.
        /// </summary>
        private readonly string _beginTransactionApiPath = "_api/transaction/begin";

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionApiClient"/> class.
        /// Create an instance of <see cref="TransactionApiClient"/>
        /// using the provided transport layer and the default JSON serialization.
        /// </summary>
        /// <param name="client">The ApiClientTransport.</param>
        public TransactionApiClient(IApiClientTransport client)
            : base(new JsonNetApiClientSerialization())
        {
            _client = client;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionApiClient"/> class.
        /// Create an instance of <see cref="TransactionApiClient"/>
        /// using the provided transport and serialization layers.
        /// </summary>
        /// <param name="client">The ApiClientTransport.</param>
        /// <param name="serializer">The ApiClientSerialization.</param>
        public TransactionApiClient(IApiClientTransport client, IApiClientSerialization serializer)
            : base(serializer)
        {
            _client = client;
        }

        /// <summary>
        /// POST a js-transaction to ArangoDB.
        /// </summary>
        /// <remarks>
        /// https://www.arangodb.com/docs/stable/http/transaction-js-transaction.html.
        /// </remarks>
        /// <typeparam name="T">Type to use for deserializing the object returned by the transaction function.</typeparam>
        /// <param name="body">Object containing information to submit in the POST transaction request.</param>
        /// <returns>Response from ArangoDB after processing the request.</returns>
        public virtual async Task<PostTransactionResponse<T>> PostTransactionAsync<T>(
            PostTransactionBody body)
        {
            var content = GetContent(body, new ApiClientSerializationOptions(true, true));
            using (var response = await _client.PostAsync(_transactionApiPath, content))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                if (response.IsSuccessStatusCode)
                {
                    return DeserializeJsonFromStream<PostTransactionResponse<T>>(stream);
                }

                var error = DeserializeJsonFromStream<ApiErrorResponse>(stream);
                throw new ApiErrorException(error);
            }
        }

        /// <summary>
        /// Abort a stream transaction by DELETE.
        /// </summary>
        /// /// <remarks>
        /// https://www.arangodb.com/docs/3.6/http/transaction-stream-transaction.html#abort-transaction.
        /// </remarks>
        /// <typeparam name="TStreamTransactionResult">Type to use for deserializing the object returned by the transaction function.</typeparam>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <exception cref="ApiErrorException">
        /// With ErrorNum 1653 if the transaction cannot be aborted.
        /// With ErrorNum 10 if the transaction is not found.
        /// </exception>
        /// <returns>Response from ArangoDB after aborting a transaction.</returns>
        public virtual async Task<PostTransactionResponse<TStreamTransactionResult>>
            AbortTransaction<TStreamTransactionResult>(string transactionId)
        {
            string completeAbortTransactionPath = string.Format(_streamTransactionApiPath, transactionId);
            using (var response = await _client.DeleteAsync(completeAbortTransactionPath))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                if (response.IsSuccessStatusCode)
                {
                    return DeserializeJsonFromStream<PostTransactionResponse<TStreamTransactionResult>>(stream);
                }

                var error = DeserializeJsonFromStream<ApiErrorResponse>(stream);
                throw new ApiErrorException(error);
            }
        }

        /// <summary>
        /// Begin a stream transaction by POST.
        /// </summary>
        /// <remarks>
        /// https://www.arangodb.com/docs/3.6/http/transaction-stream-transaction.html#begin-a-transaction.
        /// </remarks>
        /// <typeparam name="TStreamTransactionResult">Type to use for deserializing the object returned by the transaction function.</typeparam>
        /// <param name="body">Object containing information to submit in the POST stream transaction request.</param>
        /// <exception cref="ApiErrorException">
        /// With ErrorNum 10 if the <paramref name="body"/> is missing or malformed.
        /// With ErrorNum 1203 if the <paramref name="body"/> contains an unknown collection.
        /// </exception>
        /// <returns>Response from ArangoDB after beginning a transaction.</returns>
        public virtual async Task<PostTransactionResponse<TStreamTransactionResult>>
            BeginTransaction<TStreamTransactionResult>(StreamTransactionBody body)
        {
            var content = GetContent(body, new ApiClientSerializationOptions(true, true));
            using (var response = await _client.PostAsync(_beginTransactionApiPath, content))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                if (response.IsSuccessStatusCode)
                {
                    return DeserializeJsonFromStream<PostTransactionResponse<TStreamTransactionResult>>(stream);
                }

                var error = DeserializeJsonFromStream<ApiErrorResponse>(stream);
                throw new ApiErrorException(error);
            }
        }

        /// <summary>
        /// Commit a transaction by PUT.
        /// </summary>
        /// <remarks>
        /// https://www.arangodb.com/docs/3.6/http/transaction-stream-transaction.html#commit-transaction.
        /// </remarks>
        /// <typeparam name="TStreamTransactionResult">Type to use for deserializing the object returned by the transaction function.</typeparam>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <exception cref="ApiErrorException">
        /// With ErrorNum 1653 if the transaction cannot be committed.
        /// With ErrorNum 10 if the transaction is not found.
        /// </exception>
        /// <returns>Response from ArangoDB after committing a transaction.</returns>
        public virtual async Task<PostTransactionResponse<TStreamTransactionResult>>
            CommitTransaction<TStreamTransactionResult>(string transactionId)
        {
            string completeCommitTransactionPath = string.Format(_streamTransactionApiPath, transactionId);
            using (var response = await _client.PutAsync(completeCommitTransactionPath, Array.Empty<byte>()))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                if (response.IsSuccessStatusCode)
                {
                    return DeserializeJsonFromStream<PostTransactionResponse<TStreamTransactionResult>>(stream);
                }

                var error = DeserializeJsonFromStream<ApiErrorResponse>(stream);
                throw new ApiErrorException(error);
            }
        }

        /// <summary>
        /// Get currently running transactions.
        /// </summary>
        /// <remarks>
        /// https://www.arangodb.com/docs/3.6/http/transaction-stream-transaction.html#get-currently-running-transactions.
        /// </remarks>
        /// <returns>Response from ArangoDB with all running transactions.</returns>
        public virtual async Task<StreamTransactions> GetAllRunningTransactions()
        {
            using (var response = await _client.GetAsync(_transactionApiPath))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                if (response.IsSuccessStatusCode)
                {
                    return DeserializeJsonFromStream<StreamTransactions>(stream);
                }

                var error = DeserializeJsonFromStream<ApiErrorResponse>(stream);
                throw new ApiErrorException(error);
            }
        }

        /// <summary>
        /// Get the status of a transaction.
        /// </summary>
        /// <remarks>
        /// https://www.arangodb.com/docs/3.6/http/transaction-stream-transaction.html#get-transaction-status.
        /// </remarks>
        /// <typeparam name="TStreamTransactionResult">Type to use for deserializing the object returned by the transaction function.</typeparam>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <exception cref="ApiErrorException">With ErrorNum 10 if the transaction is not found.</exception>
        /// <returns>Response from ArangoDB with the status of a transaction.</returns>
        public virtual async Task<PostTransactionResponse<TStreamTransactionResult>>
            GetTransactionStatus<TStreamTransactionResult>(string transactionId)
        {
            string getTransactionPath = string.Format(_streamTransactionApiPath, transactionId);
            using (var response = await _client.GetAsync(getTransactionPath))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                if (response.IsSuccessStatusCode)
                {
                    return DeserializeJsonFromStream<PostTransactionResponse<TStreamTransactionResult>>(stream);
                }

                var error = DeserializeJsonFromStream<ApiErrorResponse>(stream);
                throw new ApiErrorException(error);
            }
        }
    }
}
