using System.Collections.Generic;
using System.Net;

namespace ArangoDBNetStandard.GraphApi.Models
{
    /// <summary>
    /// Represents a response containing the list of vertex collections within a graph.
    /// </summary>
    public class GetVertexCollectionsResponse
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
        /// The list of vertex collections.
        /// </summary>
        public IEnumerable<string> Collections { get; set; }
    }
}
