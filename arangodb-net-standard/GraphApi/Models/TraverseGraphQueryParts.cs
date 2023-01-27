using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.GraphApi.Models
{
    internal class TraverseGraphQueryParts
    {
        /// <summary>
        /// List of vertex collections that will be involved in the traversal
        /// </summary>
        public List<string> VertexCollections { get; set; }

        /// <summary>
        /// The current vertex in a traversal
        /// </summary>
        public object CurrentVertex { get; set; }

        /// <summary>
        /// The current edge in a traversal
        /// </summary>
        public object CurrentEdge { get; set; }

        /// <summary>
        /// Representation of the current path
        /// </summary>
        public TraverseGraphPath CurrentPath { get; set; }

        /// <summary>
        /// Edges and vertices returned by the query will start
        /// at the traversal depth of min (thus edges and vertices
        /// below will not be returned). 
        /// If not specified, it defaults to 1. 
        /// The minimal possible value is 0.
        /// </summary>
        public int? MinDepth { get; set; }

        /// <summary>
        /// Defines the max depth/length of paths that are traversed. 
        /// If omitted, it defaults to <see cref="MinDepth"/>.
        /// This cannot be specified without <see cref="MinDepth"/>.
        /// </summary>
        public int? MaxDepth { get; set; }

        /// <summary>
        /// Follow outgoing, incoming, or edges pointing in either 
        /// direction in the traversal.
        /// Possible values are <see cref="TraversalDirection.Any"/>,
        /// <see cref="TraversalDirection.Inbound"/>, and 
        /// <see cref="TraversalDirection.Outbound"/>
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// A vertex where the traversal will originate from. This 
        /// can be specified in the form of an ID string or in the 
        /// form of a document with the attribute _id (i.e. new { _id = "ABC" })
        /// </summary>
        public object StartVertex { get; set; }

        /// <summary>
        /// The name of the graph.
        /// </summary>
        public string GraphName { get; set; }

        /// <summary>
        /// Edge collection sets that will be involved in 
        /// the traversal instead of a named graph.
        /// </summary>
        public List<string> EdgeCollections { get; set; }

        /// <summary>
        /// A condition, like in a FILTER statement, which will be evaluated 
        /// in every step of the traversal, as early as possible.
        /// </summary>
        public string PruneCondition { get; set; }

        /// <summary>
        /// Used to modify the execution of the traversal.
        /// </summary>
        public TraverseGraphOptions Options  { get; set; }
    }
}