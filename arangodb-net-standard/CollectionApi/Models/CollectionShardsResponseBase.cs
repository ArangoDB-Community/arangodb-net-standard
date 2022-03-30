using System.Collections.Generic;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class CollectionShardsResponseBase : ResponseBase
    {
        public bool? WaitForSync { get; set; }
        public string ShardingStrategy { get; set; }
        public bool? UsesRevisionsAsDocumentIds { get; set; }
        public object Schema { get; set; }
        public int? WriteConcern { get; set; }
        public bool? SyncByRevision { get; set; }
        public int? ReplicationFactor { get; set; }
        public int? NumberOfShards { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public bool? IsDisjoint { get; set; }
        public int? MinReplicationFactor { get; set; }
        public int? Status { get; set; }
        public int? Type { get; set; }
        public string GloballyUniqueId { get; set; }
        public bool? IsSmart { get; set; }
        public bool? IsSystem { get; set; }
        public int? InternalValidatorType { get; set; }
        public bool? IsSmartChild { get; set; }
        public string StatusString { get; set; }
        public bool? CacheEnabled { get; set; }
        public List<string> ShardKeys { get; set; }
        public CollectionShardsKeyOption KeyOptions { get; set; }
    }
}