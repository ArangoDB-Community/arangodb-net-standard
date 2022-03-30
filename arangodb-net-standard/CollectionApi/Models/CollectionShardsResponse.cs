using System.Collections.Generic;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class CollectionShardsResponse : CollectionShardsResponseBase
    {
        public List<string> Shards { get; set; }
    }
}