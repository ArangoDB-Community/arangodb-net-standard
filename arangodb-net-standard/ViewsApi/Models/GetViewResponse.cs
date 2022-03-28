using System.Net;

namespace ArangoDBNetStandard.ViewsApi.Models
{
    public class GetViewResponse : ViewSummary
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
