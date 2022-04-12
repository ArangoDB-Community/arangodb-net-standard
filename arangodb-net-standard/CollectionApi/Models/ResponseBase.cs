using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    /// <summary>
    /// Represents a common response class for API operations.
    /// </summary>
    public class ResponseBase
    {
        /// <summary>
        /// Indicates whether an error occurred
        /// </summary>
        public bool? Error { get; set; }

        /// <summary>
        /// The HTTP status code.
        /// </summary>
        public HttpStatusCode Code { get; set; }
    }
}
