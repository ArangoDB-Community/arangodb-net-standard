namespace ArangoDBNetStandard.GraphApi.Models
{
    /// <summary>
    /// Options to create a satellite graph.
    /// </summary>
    public class PostSatelliteGraphOptions : PostGraphOptions
    {
        /// <summary>
        ///  Set to "satellite" to create a SatelliteGraph,
        ///  which will ignore numberOfShards, minReplicationFactor
        ///  and writeConcern (Enterprise Edition only).
        /// </summary>
        public string ReplicationFactor { get; set; }
    }
}