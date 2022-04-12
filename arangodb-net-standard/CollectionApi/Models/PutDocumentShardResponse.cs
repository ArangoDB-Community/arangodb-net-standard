namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class PutDocumentShardResponse : ResponseBase
    {
        /// <summary>
        /// The Id of the responsible shard.
        /// </summary>
        public string ShardId { get; set; }
    }
}