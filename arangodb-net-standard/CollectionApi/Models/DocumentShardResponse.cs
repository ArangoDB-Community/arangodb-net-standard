namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class DocumentShardResponse : ResponseBase
    {
        /// <summary>
        /// The Id of the responsible shard.
        /// </summary>
        public string ShardId { get; set; }
    }
}