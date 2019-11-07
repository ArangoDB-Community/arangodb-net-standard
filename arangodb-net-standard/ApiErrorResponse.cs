using System.Net;

namespace ArangoDBNetStandard
{
    /// <summary>
    /// ArangoDB API error model
    /// </summary>
    public class ApiErrorResponse
    {
        /// <summary>
        /// Whether this is an error response (always true).
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        /// Error message.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// ArangoDB error number.
        /// See https://www.arangodb.com/docs/stable/appendix-error-codes.html for error numbers and descriptions.
        /// </summary>
        public int ErrorNum { get; set; }

        /// <summary>
        /// HTTP status code.
        /// </summary>
        public HttpStatusCode Code { get; set; }
    }
}
