using System.Collections.Generic;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class PutCollectionPropertyBody
    {
        /// <summary>
        /// If true then the data is synchronized to disk before
        /// returning from a document create, update, replace 
        /// or removal operation. (default: false)
        /// </summary>
        public bool? WaitForSync { get; set; }

        [System.Obsolete()]
        public long? JournalSize { get; set; }

        /// <summary>
        /// Whether the in-memory hash cache for documents should 
        /// be enabled for this collection (default: false). 
        /// Can be controlled globally with the --cache.size startup option.
        /// The cache can speed up repeated reads of the same documents
        /// via their document keys. If the same documents are not fetched
        /// often or are modified frequently, then you may disable the cache 
        /// to avoid the maintenance costs.
        /// </summary>
        public bool? CacheEnabled { get; set; }

        /// <summary>
        ///  (The default is 1): in a cluster, this attribute determines how
        ///  many copies of each shard are kept on different DB-Servers. 
        ///  The value 1 means that only one copy (no synchronous replication) is kept. 
        ///  A value of k means that k-1 replicas are kept. It can also be the 
        ///  string "satellite" for a SatelliteCollection, where the replication 
        ///  factor is matched to the number of DB-Servers (Enterprise Edition only).
        ///  Any two copies reside on different DB-Servers. Replication between them 
        ///  is synchronous, that is, every write operation to the “leader” copy 
        ///  will be replicated to all “follower” replicas, before the write operation 
        ///  is reported successful.
        ///  If a server fails, this is detected automatically and one of the servers
        ///  holding copies take over, usually without an error being reported.
        /// </summary>
        public int? ReplicationFactor { get; set; }

        /// <summary>
        /// Write concern for this collection (default: 1). It determines how many 
        /// copies of each shard are required to be in sync on the different DB-Servers. 
        /// If there are less then these many copies in the cluster, a shard will refuse 
        /// to write. Writes to shards with enough up-to-date copies will succeed at
        /// the same time however. The value of writeConcern can not be larger than
        /// replicationFactor. (cluster only)
        /// </summary>
        public int? WriteConcern { get; set; }

        /// <summary>
        /// The collection level schema for documents.
        /// </summary>
        public CollectionSchema Schema { get; set; }
        
        /// <summary>
        /// A list of computed values.
        /// </summary>
        public List<ComputedValue> ComputedValues { get; set; }
    }
}