using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi
{
    /// <summary>
    /// Represents information about a modified graph.
    /// </summary>
    public class PostVertexCollectionModifiedGraph
    {
        /// <summary>
        /// The internal id value of this graph.
        /// </summary>
        public string _id { get; set; }

        /// <summary>
        /// The key of this graph (corresponding to the name).
        /// </summary>
        public string _key { get; set; }

        /// <summary>
        /// The revision of this graph.
        /// Can be used to make sure to not override concurrent modifications to this graph.
        /// </summary>
        public string _rev { get; set; }

        /// <summary>
        /// The name of the graph.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The name of the sharding attribute in smart graph case (Enterprise Edition only).
        /// </summary>
        public string SmartGraphAttribute { get; set; }

        /// <summary>
        /// The replication factor used for every new collection in the graph.
        /// </summary>
        public int ReplicationFactor { get; set; }

        /// <summary>
        /// A list of additional vertex collections.
        /// Documents within these collections do not have edges within this graph.
        /// </summary>
        public IEnumerable<string> OrphanCollections { get; set; }

        /// <summary>
        /// Number of shards created for every new collection in the graph.
        /// </summary>
        public int NumberOfShards { get; set; }

        /// <summary>
        /// Indicates whether the graph is a SmartGraph (Enterprise Edition only).
        /// </summary>
        public bool IsSmart { get; set; }

        /// <summary>
        /// A list of definitions for the relations of the graph.
        /// </summary>
        public IEnumerable<EdgeDefinition> EdgeDefinitions { get; set; }
    }
}
