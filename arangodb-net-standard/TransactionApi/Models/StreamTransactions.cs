using System.Collections.Generic;

namespace ArangoDBNetStandard.TransactionApi.Models
{
    /// <summary>
    /// Result from ArangoDB after getting list of all running transactions.
    /// </summary>
    public class StreamTransactions
    {
        /// <summary>
        /// Gets or sets list of all Stream Transactions.
        /// </summary>
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
