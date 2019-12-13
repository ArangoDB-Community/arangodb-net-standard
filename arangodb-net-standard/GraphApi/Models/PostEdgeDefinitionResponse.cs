using System.Net;

namespace ArangoDBNetStandard.GraphApi.Models
{
    /// <summary>
    /// Represents a response containing information about the graph
    /// and its new edge definition.
    /// </summary>
    public class PostEdgeDefinitionResponse
    {
        /// <summary>
        /// The information about the modified graph.
        /// </summary>
        public GraphResult Graph { get; set; }

        /// <summary>
        /// The HTTP status code.
        /// </summary>
        public HttpStatusCode Code { get; set; }

        /// <summary>
        /// Indicates whether an error occurred (false in this case).
        /// </summary>
        public bool Error { get; set; }
    }
}
