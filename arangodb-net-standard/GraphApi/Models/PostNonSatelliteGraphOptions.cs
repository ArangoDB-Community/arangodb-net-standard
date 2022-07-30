namespace ArangoDBNetStandard.GraphApi.Models
{
    /// <summary>
    /// Options to create a non-satellite graph.
    /// </summary>
    public class PostNonSatelliteGraphOptions : PostGraphOptions
    {
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