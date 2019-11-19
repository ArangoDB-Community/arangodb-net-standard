using System.Net;

namespace ArangoDBNetStandard.GraphApi
{
    /// <summary>
    /// Represents a response containing information about the modified graph.
    /// </summary>
    public class PostVertexCollectionResponse
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
        /// The information about the modified graph.
        /// </summary>
        public PostVertexCollectionModifiedGraph Graph { get; set; }
    }
}