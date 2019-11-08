using System.Collections.Generic;

namespace ArangoDBNetStandard.CollectionApi
{
    public class GetCollectionsOptions
    {
        public bool? ExcludeSystem { get; set; }

        public string ToQueryString()
        {
            List<string> query = new List<string>();
            if (ExcludeSystem != null)
            {
                query.Add("excludeSystem=" + (ExcludeSystem.Value ? 1 : 0));
            }
            return string.Join("&", query);
        }
    }
}
