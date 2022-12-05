using System.Net;

namespace ArangoDBNetStandard.ViewApi.Models
{
    /// <summary>
    /// Response from <see cref="IViewApiClient.GetViewPropertiesAsync"/>
    /// </summary>
    public class GetViewPropertiesResponse : ViewResponse
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

        /// <summary>
        /// The HTTP status code.
        /// </summary>
        public HttpStatusCode Code { get; set; }
    }
}
