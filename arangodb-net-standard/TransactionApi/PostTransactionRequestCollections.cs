using System.Collections.Generic;

namespace ArangoDBNetStandard.TransactionApi
{
    public class PostTransactionRequestCollections
    {
        public IList<string> Read { get; set; }

        public IList<string> Write { get; set; }

        public IList<string> Exclusive { get; set; }
    }
}