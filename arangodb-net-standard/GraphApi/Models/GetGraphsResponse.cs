using System.Collections.Generic;
using System.Net;

namespace ArangoDBNetStandard.GraphApi.Models
{
    /// <summary>
    /// Represents a response containing the list of graph.
    /// </summary>
    public class GetGraphsResponse
    {
        /// <summary>
        /// The list of graph.
        /// Note: The <see cref="GraphResult.Name"/> property is null for <see cref="GraphApiClient.GetGraphsAsync"/> in ArangoDB 4.5.2 and below,
        /// in which case you can use <see cref="GraphResult._key"/> instead.
        /// </summary>
        public IEnumerable<GraphResult> Graphs { get; set; }

        public HttpStatusCode Code { get; set; }

        /// <summary>
        /// Indicates whether an error occurred
        /// </summary>
        /// <remarks>
        /// Note that in cases where an error occurs, the ArangoDBNetStandard
        /// client will throw an <see cref="ApiErrorException"/> rather than
        /// populating this property. A try/catch block should be used instead
        /// for any required error handling.
        /// </remarks>
        public bool Error { get; set; }
    }
}
