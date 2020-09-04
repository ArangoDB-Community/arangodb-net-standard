namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class PostCollectionBody
    {
        public string DistributeShardsLike { get; set; }

        public bool? DoCompact { get; set; }

        public int? IndexBuckets { get; set; }

        public bool? IsSystem { get; set; }

        public bool? IsVolatile { get; set; }

        public long? JournalSize { get; set; }

        public CollectionKeyOptions KeyOptions { get; set; }

        public string Name { get; set; }

        public int? NumberOfShards { get; set; }

        public int? ReplicationFactor { get; set; }

        public string ShardKeys { get; set; }

        public string ShardingStrategy { get; set; }

        public string SmartJoinAttribute { get; set; }

        /// <summary>
        /// The type of the collection as number.
        ///   2: document collection (normal case)
        ///   3: edges collection
        /// </summary>
        public int? Type { get; set; }

        public bool? WaitForSync { get; set; }
    }
}