namespace ArangoDBNetStandard.GraphApi.Models
{
    /// <summary>
    /// Represents query parameters used when fetching an edge in a graph.
    /// </summary>
    public class GetEdgeQuery
    {
        /// <summary>
        /// Must contain a revision.
        /// If this is set, a document is only returned if it has exactly this revision.
        /// </summary>
        public string Rev { get; set; }

        internal string ToQueryString()
        {
            if (Rev != null)
            {
                return "rev=" + Rev;
            }
            else
            {
                return "";
            }
        }
    }
}
