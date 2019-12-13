using System.Net;

namespace ArangoDBNetStandard.GraphApi.Models
{
    /// <summary>
    /// Represents a response containing information about the newly created graph.
    /// </summary>
    public class PostGraphResponse
    {
        /// <summary>
        /// Indicates whether an error occurred (false in this case).
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        /// The HTTP status code.
        /// </summary>
        public HttpStatusCode Code { get; set; }

        /// <summary>
        /// The information about the newly created graph.
        /// </summary>
        public GraphResult Graph { get; set; }
    }
}
