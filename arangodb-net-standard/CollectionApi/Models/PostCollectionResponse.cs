using System.Net;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class PostCollectionResponse
    {
        /// <summary>
        /// Always false.
        /// </summary>
        /// <remarks>
        /// To handle errors, catch <see cref="ApiErrorException"/>
        /// thrown by API client methods.
        /// </remarks>
        public bool Error { get; set; }

        public HttpStatusCode Code { get; set; }

        /// <summary>
        /// If true then the data is synchronized to disk before returning
        /// from a document create, update, replace or removal operation.
        /// (default: false)
        /// </summary>
        public bool WaitForSync { get; set; }

        /// <summary>
        /// The type of the collection.
        /// </summary>
        public CollectionType Type { get; set; }

        public int Status { get; set; }

        /// <summary>
        /// The maximal size of a journal or datafile in bytes.
        /// This option is meaningful for the MMFiles storage engine only.
        /// </summary>
        public long JournalSize { get; set; }

        public PostCollectionResponseCollectionKeyOptions KeyOptions { get; set; }

        public string GloballyUniqueId { get; set; }

        public string StatusString { get; set; }

        public string Id { get; set; }

        /// <summary>
        /// The name of the collection.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Deprecated.
        /// Whether or not the collection is compacted.
        /// This option is meaningful for the MMFiles storage engine only.
        /// </summary>
        public bool DoCompact { get; set; }

        /// <summary>
        /// If true then the collection data is kept in-memory only and not made persistent.
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// Deprecated.
        /// The number of buckets into which indexes using a hash table are split.
        /// This option is meaningful for the MMFiles storage engine only.
        /// </summary>
        public int IndexBuckets { get; set; }

        /// <summary>
        /// Deprecated.
        /// If true then the collection data is kept in-memory only and not made persistent.
        /// This option is meaningful for the MMFiles storage engine only.
        /// </summary>
        public bool IsVolatile { get; set; }

        /// <summary>
        /// Write concern for the collection (default: 1). It determines how many copies of 
        /// each shard are required to be in sync on the different DB-Servers. If there are
        /// less then these many copies in the cluster a shard will refuse to write. Writes 
        /// to shards with enough up-to-date copies will succeed at the same time however.
        /// The value of WriteConcern cannot be larger than replicationFactor. (cluster only)
        /// </summary>
        public int WriteConcern { get; set; }

        /// <summary>
        /// (The default is 1): in a cluster, this attribute determines how many copies of 
        /// each shard are kept on different DB-Servers. The value 1 means that only one copy 
        /// (no synchronous replication) is kept. A value of k means that k-1 replicas are kept.
        /// It can also be the string "satellite" for a SatelliteCollection, where the replication
        /// factor is matched to the number of DB-Servers (Enterprise Edition only).
        /// Any two copies reside on different DB-Servers.Replication between them is synchronous,
        /// that is, every write operation to the “leader” copy will be replicated to all “follower”
        /// replicas, before the write operation is reported successful.
        /// If a server fails, this is detected automatically and one of the servers holding 
        /// copies take over, usually without an error being reported.
        /// </summary>
        public int ReplicationFactor { get; set; }
    }
}