namespace ArangoDBNetStandard.GraphApi.Models
{
    public static class TraversalUniqueEdges
    {
        /// <summary>
        /// The default value. It is guaranteed that there
        /// is no path returned with a duplicate edge
        /// </summary>
        public static string Path = "path";

        /// <summary>
        /// No uniqueness check is applied on edges.        
        /// </summary>
        /// <remarks>
        /// Using this configuration the traversal 
        /// will follow edges in cycles.
        /// </remarks>
        public static string None = "none";
    }
}