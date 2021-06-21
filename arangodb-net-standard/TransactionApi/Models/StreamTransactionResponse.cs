using System.Net;

namespace ArangoDBNetStandard.TransactionApi.Models
{
    /// <summary>
    /// Response from ArangoDB after executing a stream transaction.
    /// </summary>
    public class StreamTransactionResponse
    {
        /// <summary>
        /// Gets or sets the Http Status code.
        /// </summary>    
        public HttpStatusCode Code { get; set; }

        /// <summary>
        /// Gets or sets whether an error has occured.
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        /// Gets or sets the deserialized result from the stream transaction result.
        /// </summary>
        public StreamTransactionResult Result { get; set; }
    }
}
