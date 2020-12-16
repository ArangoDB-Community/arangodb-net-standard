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

        public CollectionType? Type { get; set; }

        public bool? WaitForSync { get; set; }
    }
}