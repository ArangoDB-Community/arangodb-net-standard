using System.Net;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Represents a response returned when creating an AQL user function.
    /// </summary>
    public class PostAqlFunctionResponse
    {
        /// <summary>
        /// Indicates whether an error occurred (false in this case).
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        /// The HTTP status code.
        /// </summary>
        public HttpStatusCode Code { get; set; }

        /// <summary>
        /// Indicates whether the function was newly created.
        /// </summary>
        public bool IsNewlyCreated { get; set; }
    }
}
