using System.Collections.Generic;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class CollectionShardsDetailedResponse : CollectionShardsResponseBase
    {
        public Dictionary<string,List<string>> Shards { get; set; }
    }
}