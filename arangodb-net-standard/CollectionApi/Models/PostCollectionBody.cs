using System.Collections.Generic;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class PostCollectionBody
    {
        /// <summary>
        /// (The default is ""): in an enterprise cluster, this attribute binds the specifics
        /// of sharding for the newly created collection to follow that of a specified existing collection.
        /// Note: Using this parameter has consequences for the prototype collection.
        /// It can no longer be dropped, before sharding imitating collections are dropped.
        /// Equally, backups and restores of imitating collections alone will generate warnings,
        /// which can be overridden, about missing sharding prototype.
        /// </summary>
        public string DistributeShardsLike { get; set; }

        /// <summary>
        /// Deprecated.
        /// Whether or not the collection will be compacted (default is true).
        /// This option is meaningful for the MMFiles storage engine only.
        /// </summary>
        [System.Obsolete()]
        public bool? DoCompact { get; set; }

        /// <summary>
        /// Deprecated.
        /// The number of buckets into which indexes using a hash table are split.
        /// The default is 16 and this number has to be a power of 2 and less than
        /// or equal to 1024.
        /// For very large collections one should increase this to avoid long pauses
        /// when the hash table has to be initially built or resized,
        /// since buckets are resized individually and can be initially built in parallel.
        /// For example, 64 might be a sensible value for a collection with 100 000 000 documents.
        /// This option is meaningful for the MMFiles storage engine only.
        /// </summary>
        /// 
        [System.Obsolete()]
        public int? IndexBuckets { get; set; }

        /// <summary>
        /// If true, create a system collection.
        /// In this case collection-name should start with an underscore.
        /// (The default is false)
        /// </summary>
        public bool? IsSystem { get; set; }

        /// <summary>
        /// Deprecated.
        /// If true then the collection data is kept in-memory only and not made persistent.
        /// Unloading the collection will cause the collection data to be discarded.
        /// Stopping or re-starting the server will also cause full loss of data in the collection.
        /// Setting this option will make the resulting collection be slightly faster
        /// than regular collections because ArangoDB does not enforce any synchronization to disk
        /// and does not calculate any CRC checksums for datafiles (as there are no datafiles).
        /// This option should therefore be used for cache-type collections only,
        /// and not for data that cannot be re-created otherwise. (The default is false)
        /// This option is meaningful for the MMFiles storage engine only.
        /// </summary>
        [System.Obsolete()]
        public bool? IsVolatile { get; set; }

        /// <summary>
        /// Deprecated.
        /// The maximal size of a journal or datafile in bytes.
        /// The value must be at least 1048576 (1 MiB).
        /// (The default is a configuration parameter)
        /// This option is meaningful for the MMFiles storage engine only.
        /// </summary>
        [System.Obsolete()]
        public long? JournalSize { get; set; }

        /// <summary>
        /// Additional options for key generation.
        /// </summary>
        public CollectionKeyOptions KeyOptions { get; set; }

        /// <summary>
        /// The name of the collection to create.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// (The default is 1): in a cluster, this value determines the number of shards
        /// to create for the collection. In a single server setup, this option is meaningless.
        /// </summary>
        public int? NumberOfShards { get; set; }

        /// <summary>
        /// (The default is 1): in a cluster, this attribute determines how many copies
        /// of each shard are kept on different DB-Servers.
        /// The value 1 means that only one copy (no synchronous replication) is kept.
        /// A value of k means that k-1 replicas are kept.
        /// (Enterprise Edition only)
        /// </summary>
        public int? ReplicationFactor { get; set; }

        /// <summary>
        /// (The default is [“_key”]): in a cluster, this attribute determines
        /// which document attributes are used to determine the target shard for documents.
        /// Values of shard key attributes cannot be changed once set.
        /// This option is meaningless in a single server setup.
        /// </summary>
        public IEnumerable<string> ShardKeys { get; set; }

        /// <summary>
        /// This attribute specifies the name of the sharding strategy to use for the collection.
        /// Since ArangoDB 3.4 there are different sharding strategies to select from
        /// when creating a new collection.
        /// The selected ShardingStrategy value will remain fixed for the collection
        /// and cannot be changed afterwards.
        /// </summary>
        /// <remarks>
        /// The available sharding strategies are:
        /// community-compat,
        /// enterprise-compat,
        /// enterprise-smart-edge-compat,
        /// hash,
        /// enterprise-hash-smart-edge.
        /// If no sharding strategy is specified,
        /// the default will be hash for all collections,
        /// and enterprise-hash-smart-edge for all smart edge collections
        /// (the latter requires the Enterprise Edition of ArangoDB).
        /// </remarks>
        public string ShardingStrategy { get; set; }

        /// <summary>
        /// In an Enterprise Edition cluster,
        /// this attribute determines an attribute of the collection
        /// that must contain the shard key value of the referred-to SmartJoin collection.
        /// Additionally, the shard key for a document in this collection must contain
        /// the value of this attribute, followed by a colon,
        /// followed by the actual primary key of the document.
        /// </summary>
        public string SmartJoinAttribute { get; set; }

        /// <summary>
        /// The type of the collection to create.
        /// </summary>
        public CollectionType? Type { get; set; }

        /// <summary>
        /// If true then the data is synchronized to disk before returning
        /// from a document create, update, replace or removal operation.
        /// (default: false)
        /// </summary>
        public bool? WaitForSync { get; set; }

        /// <summary>
        /// Optional object property that specifies the collection level schema for documents.
        /// </summary>
        public CollectionSchema Schema { get; set; }

        /// <summary>
        /// Optional. A list of computed values.
        /// </summary>
        public List<ComputedValue> ComputedValues { get; set; }
    }
}