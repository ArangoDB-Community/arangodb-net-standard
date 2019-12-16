using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi.Models
{
    public class DeleteGraphQuery
    {
        public bool? DropCollections { get; set; }

        internal string ToQueryString()
        {
            List<string> query = new List<string>();
            if (DropCollections != null)
            {
                query.Add("dropCollections=" + DropCollections.ToString().ToLower());
            }
            return string.Join("&", query);
        }
    }
}
