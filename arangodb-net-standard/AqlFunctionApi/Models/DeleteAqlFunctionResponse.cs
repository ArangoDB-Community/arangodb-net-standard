using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Represents a response containing the number of deleted AQL user functions.
    /// </summary>
    public class DeleteAqlFunctionResponse
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
        /// The number of deleted user functions,
        /// always 1 when <see cref="DeleteAqlFunctionQuery.Group"/> is set to false.
        /// Any number >= 0 when <see cref="DeleteAqlFunctionQuery.Group"/> is set to true.
        /// </summary>
        public int DeletedCount { get; set; }
    }
}
