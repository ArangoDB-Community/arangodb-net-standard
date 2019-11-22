using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi
{
    public class PostVertexQuery
    {
        public bool? WaitForSync { get; set; }

        public bool? ReturnNew { get; set; }

        internal string ToQueryString()
        {
            List<string> query = new List<string>();
            if (WaitForSync != null)
            {
                query.Add("waitForSync=" + WaitForSync.ToString().ToLower());
            }
            if (ReturnNew != null)
            {
                query.Add("returnNew=" + WaitForSync.ToString().ToLower());
            }

            return string.Join("&", query);
        }
    }
}
