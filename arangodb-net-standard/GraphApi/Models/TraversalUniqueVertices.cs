namespace ArangoDBNetStandard.GraphApi.Models
{
    public static class TraversalUniqueVertices
    {
        /// <summary>
        /// It is guaranteed that there is no
        /// path returned with a duplicate vertex.
        /// </summary>
        public static string Path = "path";

        /// <summary>
        /// It is guaranteed that each vertex is visited at
        /// most once during the traversal, no matter how 
        /// many paths lead from the start vertex to this one. 
        /// If you start with <see cref="TraverseGraphBody.MinDepth"/> > 1 
        /// a vertex that was found before <see cref="TraverseGraphBody.MinDepth"/> 
        /// might not be returned at all (it still might be part of a path). 
        /// </summary>
        /// <remarks>
        /// It is required to set
        /// <see cref="TraverseGraphOptions.Order"/> to 
        /// <see cref="TraversalOrder.BFS"/> or
        /// <see cref="TraversalOrder.Weighted"/>
        ///  because with depth-first search the results
        ///  would be unpredictable. Using this configuration 
        ///  the result is not deterministic any more. 
        ///  If there are multiple paths from
        ///  <see cref="TraverseGraphBody.StartVertex"/>
        ///  to <see cref="TraverseGraphBody.CurrentVertex"/>, 
        ///  one of those is picked. In case of a weighted 
        ///  traversal, the path with the lowest weight is picked, 
        ///  but in case of equal weights it is undefined
        ///  which one is chosen.
        /// </remarks>
        public static string Global = "global";

        /// <summary>
        /// The default value. Indicates that no uniqueness
        /// check is applied on vertices.
        /// </summary>
        public static string None = "none";
    }
}