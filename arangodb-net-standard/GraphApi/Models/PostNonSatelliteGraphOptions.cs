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

        /// <summary>
        /// The number of shards that is used for every collection within this graph.
        /// Cannot be modified later.
        /// </summary>
        /// <remarks>
        /// (cluster only)
        /// </remarks>
        public int NumberOfShards { get; set; }

        /// <summary>
        /// Write concern for new collections in the graph.
        /// It determines how many copies of each shard are 
        /// required to be in sync on the different DB-Servers. 
        /// If there are less then these many copies in the cluster
        /// a shard will refuse to write. Writes to shards with
        /// enough up-to-date copies will succeed at the same time however. 
        /// The value of writeConcern can not be larger than 
        /// <see cref="ReplicationFactor"/>.
        /// </summary>
        /// <remarks>
        /// (cluster only)
        /// </remarks>
        public int? WriteConcern { get; set; }
    }
}