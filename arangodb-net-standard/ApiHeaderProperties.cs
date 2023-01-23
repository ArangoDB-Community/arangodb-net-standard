using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ArangoDBNetStandard
{
    public class ApiHeaderProperties
    {
        /// <summary>
        /// Gets or sets the Id of a stream transaction.
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// Allow read from followers ("dirty reads").
        /// Introduced in ArangoDB 3.10.
        /// </summary>
        public bool? AllowReadFromFollowers { get; set; }

        /// <summary>
        /// Set max queue time limit.
        /// When executing a request, specify a maximal 
        /// queue time in seconds before the request is 
        /// canceled and removed from the queue.
        /// The value 0 is ignored by the server.
        /// Introduced in ArangoDB 3.9
        /// </summary>
        public decimal? MaxQueueTimeLimit { get; set; }

        /// <summary>
        /// Any other headers you wish to add based on
        /// the specifications of the API operation.
        /// </summary>
        public Dictionary<string,string> OtherHeaders { get; set; }

        public WebHeaderCollection ToWebHeaderCollection()
        {
            WebHeaderCollection collection = new WebHeaderCollection();
            if (TransactionId != null)
            {
                collection.Add(CustomHttpHeaders.StreamTransactionHeader, TransactionId);
            }

            if (AllowReadFromFollowers != null)
            {
                collection.Add(CustomHttpHeaders.ReadFromFollowersHeader, AllowReadFromFollowers.Value.ToString());
            }

            if (MaxQueueTimeLimit != null)
            {
                collection.Add(CustomHttpHeaders.MaxQueueTimeLimitHeader, MaxQueueTimeLimit.Value.ToString());
            }

            if (OtherHeaders != null && OtherHeaders.Count > 0)
            {
                foreach (string key in OtherHeaders.Keys)
                {
                    collection.Add(key, OtherHeaders[key]); 
                }
            }

            return collection;
        }
    }
}
