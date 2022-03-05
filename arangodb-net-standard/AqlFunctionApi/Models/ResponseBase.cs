using System.Net;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Represents a common response class for API operations.
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
