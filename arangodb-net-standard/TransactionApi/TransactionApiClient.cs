using System.Threading.Tasks;

using ArangoDBNetStandard.Transport;

namespace ArangoDBNetStandard.TransactionApi
{
    /// <summary>
    /// Provides access to ArangoDB transaction API.
    /// </summary>
    public class TransactionApiClient: ApiClientBase
    {
        private IApiClientTransport _client;
        private readonly string _transactionApiPath = "_api/transaction";

        /// <summary>
        /// Create an instance of <see cref="TransactionApiClient"/>
        /// using the provided transport layer.
        /// </summary>
        /// <param name="client"></param>
        public TransactionApiClient(IApiClientTransport client)
        {
            _client = client;
        }

        /// <summary>
        /// POST a transaction to ArangoDB.
        /// </summary>
        /// <typeparam name="T">Type to use for deserializing the object returned by the transaction function.</typeparam>
        /// <param name="request">Object containing information to submit in the POST transaction request.</param>
        /// <returns>Response from ArangoDB after processing the request.</returns>
        public async Task<PostTransactionResponse<T>> PostTransactionAsync<T>(PostTransactionBody body)
        {
            var content = GetStringContent(body, true, true);
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
    }
}
