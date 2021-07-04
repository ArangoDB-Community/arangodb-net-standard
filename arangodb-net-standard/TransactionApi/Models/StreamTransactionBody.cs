namespace ArangoDBNetStandard.TransactionApi.Models
{
    /// <summary>
    /// Represents information required to make a stream transaction to begin.
    /// </summary>
    public class StreamTransactionBody
    {
        /// <summary>
        /// Gets or sets to allow reading from undeclared collections.
        /// </summary>
        public bool AllowImplicit { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="PostTransactionRequestCollections"/> object that can have one or all sub-attributes read, write or exclusive,
        /// each being an array of collection names.
        /// Collections that will be written to in the transaction must be declared with the write or exclusive attribute or it will fail,
        /// whereas non-declared collections from which is solely read will be added lazily.
        /// </summary>
        public PostTransactionRequestCollections Collections { get; set; }

        /// <summary>
        /// Gets or sets an optional numeric value that can be used to set a timeout for waiting on collection locks.
        /// If not specified, a default value will be used.
        /// Setting lockTimeout to 0 will make ArangoDB not time out waiting for a lock.
        /// </summary>
        public long? LockTimeout { get; set; }

        /// <summary>
        /// Gets or sets the maximum transaction size limit in bytes. Honored by the RocksDB storage engine only.
        /// </summary>
        public long? MaxTransactionSize { get; set; }

        /// <summary>
        /// Gets or sets optional flag to force the transaction to write all data to disk before returning.
        /// </summary>
        public bool? WaitForSync { get; set; }
    }
}
