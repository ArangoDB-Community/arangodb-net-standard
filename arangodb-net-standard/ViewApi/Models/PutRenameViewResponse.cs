using System.Net;

namespace ArangoDBNetStandard.ViewApi.Models
{
    /// <summary>
    /// Response from <see cref="IViewApiClient.PutRenameViewAsync(string, PutRenameViewBody)"/>
    /// </summary>
    public class PutRenameViewResponse : ViewSummary
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
