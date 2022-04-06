using System;
using System.Net;
using System.Text;

namespace ArangoDBNetStandard.AnalyzerApi.Models
{
    /// <summary>
    /// Represents a common response class for Analyzer API operations.
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