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
                query.Add("excludeSystem=" + ExcludeSystem.ToString().ToLower());
            }
            return string.Join("&", query);
        }
    }
}
