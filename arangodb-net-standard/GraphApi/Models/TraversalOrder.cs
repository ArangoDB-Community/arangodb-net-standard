namespace ArangoDBNetStandard.GraphApi.Models
{
    public static class TraversalOrder
    {
        /// <summary>
        /// The traversal will be executed breadth-first. 
        /// The results will first contain all vertices at
        /// depth 1, then all vertices at depth 2 and so on.
        /// </summary>
        public static string BFS = "bfs";

        /// <summary>
        /// The traversal will be executed depth-first.
        /// It will first return all paths from min depth to
        /// max depth for one vertex at depth 1, then for the
        /// next vertex at depth 1 and so on.
        /// </summary>
        public static string DFS = "dfs";

        /// <summary>
        /// The traversal will be a weighted traversal (introduced in v3.8.0).
        /// Paths are enumerated with increasing cost. 
        /// </summary>
        public static string Weighted = "weighted";
    }
}