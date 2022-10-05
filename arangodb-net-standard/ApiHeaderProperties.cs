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
        /// </summary>
        public bool? AllowReadFromFollowers { get; set; }

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
