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

        public bool Error { get; set; }
    }
}
