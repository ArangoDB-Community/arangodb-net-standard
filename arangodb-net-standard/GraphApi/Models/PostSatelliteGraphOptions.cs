namespace ArangoDBNetStandard.GraphApi.Models
{
    /// <summary>
    /// Options to create a satellite graph.
    /// </summary>
    public class PostSatelliteGraphOptions : PostGraphOptions
    {
        /// <summary>
        ///  Always set to "satellite" to create 
        ///  a SatelliteGraph (Enterprise Edition only).
        /// </summary>
        public string ReplicationFactor { get; } = "satellite";
    }
}