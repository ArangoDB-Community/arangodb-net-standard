using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi
{
    public class PostGraphEdgeQuery
    {
        public bool? ReturnNew { get; set; }

        public bool? WaitForSync { get; set; }

        public string ToQueryString()
        {
            List<string> query = new List<string>();
            if (ReturnNew != null)
            {
                query.Add("waitForSync=" + ReturnNew.ToString().ToLower());
            }
            if (WaitForSync != null)
            {
                query.Add("returnNew=" + WaitForSync.ToString().ToLower());
            }
            return string.Join("&", query);
        }
    }
}
