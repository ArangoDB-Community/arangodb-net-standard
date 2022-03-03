using System.Net;

namespace ArangoDBNetStandard.IndexApi.Models
{
    /// <summary>
    /// Represents a common response class for Index API operations.
    /// </summary>
    public class ResponseBase
    {
        /// <summary>
        /// Indicates whether an error occurred
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        /// The HTTP status code.
        /// </summary>
        public HttpStatusCode Code { get; set; }
    }
}
