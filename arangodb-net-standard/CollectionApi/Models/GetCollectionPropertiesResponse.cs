using System.Net;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class GetCollectionPropertiesResponse
    {
        public bool WaitForSync { get; set; }
        public bool DoCompact { get; set; }
        public int JournalSize { get; set; }
        public CollectionKeyOptions KeyOptions { get; set; }
        public bool IsVolatile { get; set; }
        public int? NumberOfShards { get; set; }
        public string ShardKeys { get; set; }
        public int? ReplicationFactor { get; set; }
        public string ShardingStrategy { get; set; }

        public string Error { get; set; }
        public HttpStatusCode Code { get; set; }
        public bool CacheEnabled { get; set; }
        public bool IsSystem { get; set; }
        public string GloballyUniqueId { get; set; }
        public string ObjectId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string StatusString { get; set; }
        public int Type { get; set; }
    }
}
