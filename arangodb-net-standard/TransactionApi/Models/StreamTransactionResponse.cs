using System.Net;

namespace ArangoDBNetStandard.TransactionApi.Models
{
    /// <summary>
    /// Response from ArangoDB after executing a stream transaction operation.
    /// </summary>
    public class StreamTransactionResponse
    {
        /// <summary>
        /// Gets or sets the Http Status code.
        /// </summary>    
        public HttpStatusCode Code { get; set; }

        /// <summary>
        /// Indicates whether an error occurred
        /// </summary>
        /// <remarks>
        /// Note that in cases where an error occurs, the ArangoDBNetStandard
        /// client will throw an <see cref="ApiErrorException"/> rather than
        /// populating this property. A try/catch block should be used instead
        /// for any required error handling.
        /// </remarks>
        public bool Error { get; set; }

        /// <summary>
        /// Gets or sets the deserialized result from the stream transaction result.
        /// </summary>
        public StreamTransactionResult Result { get; set; }
    }
}
