using System.Net;

namespace ArangoDBNetStandard.AdminApi.Models
{
    /// <summary>
    /// Represents a common response class for Admin API operations.
    /// </summary>
    public class ResponseBase
    {
        /// <summary>
        /// Always false.
        /// </summary>
        /// <remarks>
        /// To handle errors, catch <see cref="ApiErrorException"/>
        /// thrown by API client methods.
        /// </remarks>
        public bool Error { get; set; }

        /// <summary>
        /// The HTTP status code.
        /// </summary>
        public HttpStatusCode Code { get; set; }
    }

}
