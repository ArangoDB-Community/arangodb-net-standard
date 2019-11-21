using System.Net;

namespace ArangoDBNetStandard.GraphApi
{
    /// <summary>
    /// Represents a response containing information about the graph
    /// and its new edge definition.
    /// </summary>
    public class PostGraphEdgeDefinitionResponse
    {
        /// <summary>
        /// The information about the modified graph.
        /// </summary>
        public PostGraphEdgeDefinitionGraph Graph { get; set; }

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
