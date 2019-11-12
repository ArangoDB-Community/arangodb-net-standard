using System.Collections.Generic;

namespace ArangoDBNetStandard.CollectionApi
{
    public class PostCollectionQuery
    {
        /// <summary>
        /// Default is true which means the server will only report success back to the
        /// client if all replicas have created the collection. Set to false if you want
        /// faster server responses and don’t care about full replication.
        /// </summary>
        public bool? WaitForSyncReplication { get; set; }

        /// <summary>
        /// Default is true which means the server will check if there are enough replicas
        /// available at creation time and bail out otherwise. Set to false to disable
        /// this extra check.
        /// </summary>
        public bool? EnforceReplicationFactor { get; set; }

        /// <summary>
        /// Get the set of options in a format suited to a URL query string.
        /// </summary>
        /// <returns></returns>
        public string ToQueryString()
        {
            List<string> query = new List<string>();
            if (WaitForSyncReplication != null)
            {
                query.Add("waitForSyncReplication=" + (WaitForSyncReplication.Value ? 1 : 0));
            }
            if (EnforceReplicationFactor != null)
            {
                query.Add("enforceReplicationFactor=" + (EnforceReplicationFactor.Value ? 1 : 0));
            }
            return string.Join("&", query);
        }
    }
}