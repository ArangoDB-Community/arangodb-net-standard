using System.Collections.Generic;
using System.Net;

namespace ArangoDBNetStandard.GraphApi.Models
{
    public class GetEdgeCollectionsResponse
    {
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

        public int ErrorNum { get; set; }

        public HttpStatusCode Code { get; set; }

        public IEnumerable<string> Collections { get; set; }
    }
}
