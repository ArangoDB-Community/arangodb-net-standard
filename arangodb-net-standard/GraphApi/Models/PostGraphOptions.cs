using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi.Models
{
    /// <summary>
    /// Defines options for creating collections within a graph.
    /// </summary>
    public abstract class PostGraphOptions
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
        /// (Optional) An array of collection names that will be used 
        /// to create SatelliteCollections for a 
        /// Hybrid (Disjoint) SmartGraph (Enterprise Edition only). 
        /// Each array element must be a string and a valid collection name. 
        /// The collection type cannot be modified later.
        /// </summary>
        public IEnumerable<string> Satellites { get; set; }
    }
}