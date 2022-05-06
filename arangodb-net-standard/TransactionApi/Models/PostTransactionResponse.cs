using System.Net;

namespace ArangoDBNetStandard.TransactionApi.Models
{
    /// <summary>
    /// Response from ArangoDB after executing a transaction.
    /// </summary>
    /// <typeparam name="T">Type used to deserialize the returned object from the transaction function.</typeparam>
    public class PostTransactionResponse<T>
    {
        /// <summary>
        /// Whether the request resulted in error.
        /// </summary>
        /// <remarks>
        /// Note that in cases where an error occurs, the ArangoDBNetStandard
        /// client will throw an <see cref="ApiErrorException"/> rather than
        /// populating this property. A try/catch block should be used instead
        /// for any required error handling.
        /// </remarks>
        public bool Error { get; set; }

        /// <summary>
        /// The ArangoDB result code.
        /// </summary>
        public HttpStatusCode Code { get; set; }

        /// <summary>
        /// Deserialized result from the transaction function.
        /// </summary>
        public T Result { get; set; }
    }
}
