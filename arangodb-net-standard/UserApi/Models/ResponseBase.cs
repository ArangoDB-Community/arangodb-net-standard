using System.Net;

namespace ArangoDBNetStandard.UserApi.Models
{
    /// <summary>
    /// Represents a common response class for user API operations.
    /// </summary>
    public class ResponseBase
    {
        /// <summary>
        /// Indicates whether an error occurred (false in this case).
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        /// The HTTP status code.
        /// </summary>
        public HttpStatusCode Code { get; set; }
    }
}
