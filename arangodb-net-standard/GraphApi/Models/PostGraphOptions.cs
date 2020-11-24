namespace ArangoDBNetStandard.GraphApi.Models
{
    /// <summary>
    /// Defines options for creating collections within a graph.
    /// </summary>
    public class PostGraphOptions
    {
        /// <summary>
        /// The attribute name that is used to smartly shard the vertices of a graph.
        /// It is required if creating a SmartGraph.
        /// Every vertex in this SmartGraph has to have this attribute.
        /// Cannot be modified later.
        /// (Enterprise Edition only).
        /// </summary>
        /// <remarks>
        /// (cluster only)
        /// </remarks>
        public string SmartGraphAttribute { get; set; }

        /// <summary>
        /// The number of shards that is used for every collection within this graph.
        /// Cannot be modified later.
        /// </summary>
        /// <remarks>
        /// (cluster only)
        /// </remarks>
        public int NumberOfShards { get; set; }

        /// <summary>
        /// The replication factor used when initially creating collections for this graph
        /// (Enterprise Edition only).
        /// </summary>
        /// <remarks>
        /// (cluster only)
        /// </remarks>
        public int ReplicationFactor { get; set; }
    }
}
