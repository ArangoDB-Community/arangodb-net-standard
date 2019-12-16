using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi.Models
{
    /// <summary>
    /// Represents a request body to add an edge definition to a graph.
    /// </summary>
    public class PostEdgeDefinitionBody
    {
        /// <summary>
        /// One or many vertex collections that can contain target vertices.
        /// </summary>
        public IEnumerable<string> To { get; set; }

        /// <summary>
        /// One or many vertex collections that can contain source vertices.
        /// </summary>
        public IEnumerable<string> From { get; set; }

        /// <summary>
        /// The name of the edge collection to be used.
        /// </summary>
        public string Collection { get; set; }
    }
}
