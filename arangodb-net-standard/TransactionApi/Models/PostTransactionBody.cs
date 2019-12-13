using System.Collections.Generic;

namespace ArangoDBNetStandard.TransactionApi.Models
{
    /// <summary>
    /// Represents information required to make a transaction request to ArangoDB.
    /// </summary>
    public class PostTransactionBody
    {
        /// <summary>
        /// JavaScript function describing the transaction action.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Collections configuration for the transaction.
        /// </summary>
        public PostTransactionRequestCollections Collections { get; set; }

        /// <summary>
        /// The maximum transaction size before making intermediate commits (RocksDB only).
        /// </summary>
        public long? MaxTransactionSize { get; set; }

        /// <summary>
        /// The maximum time to wait for required locks to be released, before the transaction times out.
        /// </summary>
        public long? LockTimeout { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? WaitForSync { get; set; }

        /// <summary>
        /// Parameters to be passed into the transaction JavaScript function defined by <see cref="Action"/>.
        /// </summary>
        public Dictionary<string, object> Params { get; set; }
    }
}