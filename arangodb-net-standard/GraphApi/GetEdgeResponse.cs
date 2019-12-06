using System.Net;

namespace ArangoDBNetStandard.GraphApi
{
    /// <summary>
    /// Represents a response containing an edge in a graph.
    /// </summary>
    /// <typeparam name="T">The type of the edge document.</typeparam>
    public class GetEdgeResponse<T>
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
        /// The complete edge.
        /// </summary>
        public T Edge { get; set; }
    }
}
