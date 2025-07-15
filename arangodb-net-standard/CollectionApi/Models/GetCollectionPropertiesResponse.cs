using System.Collections.Generic;
using System.Net;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class GetCollectionPropertiesResponse
    {
        /// <summary>
        /// If true then the data is synchronized to disk before returning
        /// from a document create, update, replace or removal operation.
        /// (default: false)
        /// </summary>
        public bool WaitForSync { get; set; }

        /// <summary>
        /// Deprecated.
        /// Whether or not the collection is compacted.
        /// This option is meaningful for the MMFiles storage engine only.
        /// </summary>
        [System.Obsolete()]
        public bool DoCompact { get; set; }

        /// <summary>
        /// The maximal size of a journal or datafile in bytes.
        /// This option is meaningful for the MMFiles storage engine only.
        /// </summary>
        [System.Obsolete()]
        public int JournalSize { get; set; }

        /// <summary>
        /// Additional options for key generation.
        /// </summary>
        public CollectionKeyOptions KeyOptions { get; set; }

        /// <summary>
        /// Deprecated.
        /// If true then the collection data is kept in-memory only and not made persistent.
        /// This option is meaningful for the MMFiles storage engine only.
        /// </summary>
        [System.Obsolete()]
        public bool IsVolatile { get; set; }

        /// <summary>
        /// In a cluster, this value determines the number of shards
        /// created for the collection. In a single server setup, this option is meaningless.
        /// </summary>
        public int? NumberOfShards { get; set; }

        /// <summary>
        /// In a cluster, this attribute determines
        /// which document attributes are used to determine the target shard for documents.
        /// This option is meaningless in a single server setup.
        /// </summary>
        public IEnumerable<string> ShardKeys { get; set; }

        /// <summary>
        /// In a cluster, this attribute determines how many copies
        /// of each shard are kept on different DB-Servers.
        /// (Enterprise Edition only)
        /// </summary>
        public int? ReplicationFactor { get; set; }

        /// <summary>
        /// This attribute specifies the name of the sharding strategy used for the collection.
        /// </summary>
        public string ShardingStrategy { get; set; }

        /// <summary>
        /// Indicates whether an error occurred (false in this case).
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// The HTTP status code.
        /// </summary>
        public HttpStatusCode Code { get; set; }

        public bool CacheEnabled { get; set; }

        /// <summary>
        /// If true then the collection data is kept in-memory only and not made persistent.
        /// </summary>
        public bool IsSystem { get; set; }

        public string GloballyUniqueId { get; set; }

        public string ObjectId { get; set; }

        public string Id { get; set; }

        /// <summary>
        /// The name of the collection.
        /// </summary>
        public string Name { get; set; }

        public int Status { get; set; }

        public string StatusString { get; set; }

        /// <summary>
        /// The type of the collection.
        /// </summary>
        public CollectionType Type { get; set; }

        /// <summary>
        /// The collection level schema for documents.
        /// </summary>
        public CollectionSchema Schema { get; set; }
        
        /// <summary>
        /// A list of computed values configured for the 
        /// collection.
        /// </summary>
        public List<ComputedValue> ComputedValues { get; set; }
    }
}