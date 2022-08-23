using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi.Models
{
    public class TraverseGraphOptions
    {
        /// <summary>
        /// An array of all vertices on this path
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// An array of all edges on this path
        /// </summary>
        public IEnumerable<string> Edges { get; set; }

        /// <summary>
        /// Optionally ensure vertex uniqueness.
        /// For possible values, <see cref="TraversalUniqueVertices"/>
        /// </summary>
        public string UniqueVertices { get; set; }

        /// <summary>
        /// Optionally ensure edge uniqueness.
        /// For possible values, <see cref="TraversalUniqueEdges"/>
        /// </summary>
        public string UniqueEdges { get; set; }

        /// <summary>
        /// Optional. Restricts edge collections the traversal may
        /// visit (introduced in v3.7.0). If omitted, or an empty 
        /// array is specified, then there are no restrictions. 
        /// </summary>
        public IEnumerable<string> EdgeCollections { get; set; }

        /// <summary>
        /// Optional. Restricts restrict vertex collections the 
        /// traversal may visit (introduced in v3.7.0). If omitted,
        /// or an empty array is specified, then there are no 
        /// restrictions. 
        /// </summary>
        /// <remarks>
        /// - Each element of the collection should be a string containing 
        /// the name of a vertex collection.
        /// - The starting vertex is always allowed, even if it does not 
        /// belong to one of the collections specified by a restriction.
        /// </remarks>
        public IEnumerable<string> VertexCollections { get; set; }

        /// <summary>
        /// Optional. Enterprise Edition only. Parallelizes traversal execution.
        /// If omitted or set to a value of 1, traversal execution is not
        /// parallelized. If set to a value greater than 1, then up to that many
        /// worker threads can be used for concurrently executing the traversal. 
        /// The value is capped by the number of available cores on the target
        /// machine.
        /// </summary>
        /// <remarks>
        /// Parallelizing a traversal is normally useful when there are many 
        /// inputs (start vertices) that the nested traversal can work on 
        /// concurrently. This is often the case when a nested traversal is 
        /// fed with several tens of thousands of start vertices, which can 
        /// then be distributed randomly to worker threads for parallel execution.
        /// Traversal parallelization is only available in the Enterprise Edition, 
        /// and limited to traversals in single server deployments and to cluster
        /// traversals that are running in a OneShard setup.Cluster traversals
        /// that run on a Coordinator node and SmartGraph traversals are 
        /// currently not parallelized, even if the options is specified.
        /// </remarks>
        public int? Parallelism { get; set; }

        /// <summary>
        /// Optional. Specifies the name of an attribute that is used to look up the
        /// weight of an edge. If no attribute is specified or if it is not 
        /// present in the edge document then the defaultWeight is used. The
        /// attribute value must not be negative.
        /// </summary>
        /// <remarks>
        /// Weighted traversals do not support negative weights. If a document 
        /// attribute (as specified by <see cref="WeightAttribute"/>) with a 
        /// negative value is encountered during traversal, or 
        /// if <see cref="DefaultWeight"/> is set to a negative number, then 
        /// the query is aborted with an error.
        /// </remarks>
        public string WeightAttribute { get; set; }

        /// <summary>
        /// Optional. Specifies the default weight of an edge. The value must not be 
        /// negative. The default value is 1.
        /// </summary>
        /// <remarks>
        /// Weighted traversals do not support negative weights. If a document 
        /// attribute (as specified by <see cref="WeightAttribute"/>) with a 
        /// negative value is encountered during traversal, or 
        /// if <see cref="DefaultWeight"/> is set to a negative number, then 
        /// the query is aborted with an error.
        /// </remarks>
        public int? DefaultWeight { get; set; }
    }
}