using System.Collections.Generic;

namespace ArangoDB_NET_Standard.TransactionApi
{
    public class PostTransactionRequestCollections
    {
        public IList<string> Read { get; set; }

        public IList<string> Write { get; set; }

        public IList<string> Exclusive { get; set; }
    }
}