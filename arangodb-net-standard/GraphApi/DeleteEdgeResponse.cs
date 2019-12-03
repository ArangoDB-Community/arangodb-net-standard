using System.Net;

namespace ArangoDBNetStandard.GraphApi
{
    /// <summary>
    /// Represents a response containing information about a deleted edge in a graph.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DeleteEdgeResponse<T>
    {
        /// <summary>
        /// The complete deleted edge document.
        /// Includes all attributes stored before the delete operation.
        /// Will only be present if <see cref="DeleteEdgeQuery.ReturnOld"/> is true.
        /// </summary>
        public T Old { get; set; }

        /// <summary>
        /// Is set to true if the edge was successful removed.
        /// </summary>
        public bool Removed { get; set; }

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
