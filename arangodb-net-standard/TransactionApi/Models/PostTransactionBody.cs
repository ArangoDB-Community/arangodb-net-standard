using System.Collections.Generic;

namespace ArangoDBNetStandard.TransactionApi.Models
{
    /// <summary>
    /// Represents information required to make a transaction request to ArangoDB.
    /// </summary>
    public class PostTransactionBody : StreamTransactionBody
    {
        /// <summary>
        /// JavaScript function describing the transaction action.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Parameters to be passed into the transaction JavaScript function defined by <see cref="Action"/>.
        /// </summary>
        public Dictionary<string, object> Params { get; set; }
    }
}