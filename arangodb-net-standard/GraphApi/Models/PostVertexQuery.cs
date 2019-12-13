using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi.Models
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
                query.Add("returnNew=" + ReturnNew.ToString().ToLower());
            }

            return string.Join("&", query);
        }
    }
}
