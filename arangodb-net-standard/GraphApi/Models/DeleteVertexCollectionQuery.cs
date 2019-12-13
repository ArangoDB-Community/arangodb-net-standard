using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi.Models
{
    public class DeleteVertexCollectionQuery
    {
        public bool? DropCollection { get; set; }

        internal string ToQueryString()
        {
            List<string> query = new List<string>();
            if (DropCollection != null)
            {
                query.Add("dropCollection=" + DropCollection.ToString().ToLower());
            }
            return string.Join("&", query);
        }
    }
}
