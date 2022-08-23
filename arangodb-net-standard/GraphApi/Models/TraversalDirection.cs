namespace ArangoDBNetStandard.GraphApi.Models
{
    public static class TraversalDirection
    {
        /// <summary>
        /// Edges pointing in either direction in the traversal
        /// </summary>
        public static string Any = "ANY";
        
        /// <summary>
        /// Inbound edges only.
        /// </summary>
        public static string Inbound = "INBOUND";

        /// <summary>
        /// Outbound edges only.
        /// </summary>
        public static string Outbound = "OUTBOUND";
    }
}