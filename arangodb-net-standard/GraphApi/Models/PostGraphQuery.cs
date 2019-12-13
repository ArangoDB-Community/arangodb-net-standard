namespace ArangoDBNetStandard.GraphApi.Models
{
    /// <summary>
    /// Represents query parameters used when creating a new graph.
    /// </summary>
    public class PostGraphQuery
    {
        /// <summary>
        /// Whether the request should wait until synced to disk.
        /// </summary>
        public bool? WaitForSync { get; set; }

        internal string ToQueryString()
        {
            if (WaitForSync != null)
            {
                return "waitForSync=" + WaitForSync.ToString().ToLower();
            }
            else
            {
                return "";
            }
        }
    }
}
