namespace ArangoDBNetStandard.TransactionApi.Models
{
    /// <summary>
    /// Result from ArangoDB after performing a Stream transaction operation.
    /// </summary>
    public class StreamTransactionResult
    {
        /// <summary>
        /// Gets or sets the identifier of the transaction.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the status of the transaction.
        /// </summary>
        public StreamTransactionStatus Status { get; set; }
    }
}
