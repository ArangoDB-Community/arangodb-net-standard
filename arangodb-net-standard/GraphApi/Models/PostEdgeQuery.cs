using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi.Models
{
    /// <summary>
    /// Represents query parameters used when creating a new graph edge.
    /// </summary>
    public class PostEdgeQuery
    {
        /// <summary>
        /// Whether the response should contain the complete new version of the document.
        /// </summary>
        public bool? ReturnNew { get; set; }

        /// <summary>
        /// Whether the request should wait until synced to disk.
        /// </summary>
        public bool? WaitForSync { get; set; }

        public string ToQueryString()
        {
            List<string> query = new List<string>();
            if (ReturnNew != null)
            {
                query.Add("returnNew=" + ReturnNew.ToString().ToLower());
            }
            if (WaitForSync != null)
            {
                query.Add("waitForSync=" + WaitForSync.ToString().ToLower());
            }
            return string.Join("&", query);
        }
    }
}
