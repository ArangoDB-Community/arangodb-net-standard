using System.Collections.Generic;

namespace ArangoDBNetStandard.TransactionApi
{
    /// <summary>
    /// Represents the collections object passed in an ArangoDB transaction request.
    /// </summary>
    public class PostTransactionRequestCollections
    {
        /// <summary>
        /// The list of read-only collections involved in a transaction.
        /// </summary>
        public IList<string> Read { get; set; }

        /// <summary>
        /// The list of write collection involved in a transaction.
        /// </summary>
        public IList<string> Write { get; set; }

        /// <summary>
        /// Collections for which to obtain exclusive locks during a transaction.
        /// </summary>
        public IList<string> Exclusive { get; set; }
    }
}