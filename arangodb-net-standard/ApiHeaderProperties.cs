using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ArangoDBNetStandard
{
    /// <summary>
    /// Generic headers for ArangoDB requests
    /// </summary>
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
        /// Set queue time limit.
        /// When executing a request, specify a maximal 
        /// queue time before the request is canceled 
        /// and removed from the queue.
        /// </summary>
        public decimal? QueueTimeLimit { get; set; }

        /// <summary>
        /// Any other headers you wish to add based on
        /// the specifications of the API operation.
        /// </summary>
        public Dictionary<string,string> OtherHeaders { get; set; }

        /// <summary>
        /// Converts header properties to header collection
        /// for a web request.
        /// </summary>
        /// <returns></returns>
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

            if (QueueTimeLimit != null)
            {
                collection.Add(CustomHttpHeaders.QueueTimeLimitHeader, QueueTimeLimit.Value.ToString());
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
