using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi
{
    public class GetVertexQuery
    {
        public bool? Rev { get; set; }

        internal string ToQueryString()
        {
            List<string> query = new List<string>();
            if (Rev != null)
            {
                query.Add("rev=" + Rev.ToString().ToLower());
            }
            return string.Join("&", query);
        }
    }
}
