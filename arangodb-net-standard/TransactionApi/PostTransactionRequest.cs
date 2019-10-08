using System.Collections.Generic;

namespace ArangoDBNetStandard.TransactionApi
{
    public class PostTransactionRequest
    {
        public string Action { get; set; }

        public PostTransactionRequestCollections Collections { get; set; }

        public long? MaxTransactionSize { get; set; }

        public long? LockTimeout { get; set; }

        public bool? WaitForSync { get; set; }

        public Dictionary<string, object> Params { get; set; }
    }
}