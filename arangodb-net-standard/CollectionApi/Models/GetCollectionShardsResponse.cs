using System.Collections.Generic;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class GetCollectionShardsResponse : CollectionShardsResponseBase
    {
        public List<string> Shards { get; set; }
    }
}