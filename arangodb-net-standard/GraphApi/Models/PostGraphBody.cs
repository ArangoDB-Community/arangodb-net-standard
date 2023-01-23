using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi.Models
{
    /// <summary>
    /// Represents a request body to create a named graph.
    /// </summary>
    /// <remarks>
    /// The creation of a graph requires the name of the graph
    /// and a definition of its edges.
    /// </remarks>
    public class PostGraphBody
    {
        /// <summary>
        /// Name of the graph.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of definitions for the relations of the graph.
        /// </summary>
        public List<EdgeDefinition> EdgeDefinitions { get; set; }

        /// <summary>
        /// (Optional) List of additional vertex collections.
        /// Documents within these collections do not have edges within this graph.
        /// </summary>
        public List<string> OrphanCollections { get; set; }

        /// <summary>
        /// (Optional) Defines if the created graph should be smart (Enterprise Edition only).
        /// </summary>
        /// <remarks>
        /// (cluster only)
        /// </remarks>
        public bool? IsSmart { get; set; }

        /// <summary>
        /// (Optional) Whether to create a Disjoint SmartGraph instead 
        /// of a regular SmartGraph (Enterprise Edition only).
        /// </summary>
        /// <remarks>
        /// (cluster only)
        /// </remarks>
        public bool? IsDisjoint { get; set; }

        /// <summary>
        /// (Optional) Defines options for creating collections within this graph.
        /// Must be an instance of <see cref="PostSatelliteGraphOptions"/> or
        /// <see cref="PostNonSatelliteGraphOptions"/>
        /// </summary>
        public PostGraphOptions Options { get; set; }
    }
}