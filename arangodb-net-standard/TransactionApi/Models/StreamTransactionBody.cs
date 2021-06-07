namespace ArangoDBNetStandard.TransactionApi.Models
{
    /// <summary>
    /// Represents information required to make a stream transaction to begin.
    /// </summary>
    public class StreamTransactionBody
    {
        /// <summary>
        /// Gets or sets the collections configuration for the transaction.
        /// </summary>
        public PostTransactionRequestCollections Collections { get; set; }

        /// <summary>
        /// Gets or sets the maximum time to wait for required locks to be released, before the transaction times out.
        /// </summary>
        public long? LockTimeout { get; set; }

        /// <summary>
        /// Gets or sets the maximum transaction size before making intermediate commits (RocksDB only).
        /// </summary>
        public long? MaxTransactionSize { get; set; }

        /// <summary>
        /// Gets or sets optional flag to force the transaction to write all data to disk before returning.
        /// </summary>
        public bool? WaitForSync { get; set; }
    }
}
