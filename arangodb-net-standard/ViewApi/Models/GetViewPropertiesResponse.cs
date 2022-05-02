using System.Net;

namespace ArangoDBNetStandard.ViewApi.Models
{
    /// <summary>
    /// Response from <see cref="IViewApiClient.GetViewPropertiesAsync(string)"/>
    /// </summary>
    public class GetViewPropertiesResponse : ViewResponse
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
