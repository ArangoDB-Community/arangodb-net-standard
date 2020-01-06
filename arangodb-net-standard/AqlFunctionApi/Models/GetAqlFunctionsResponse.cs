using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Represents a response returned when getting all AQL user functions.
    /// </summary>
    public class GetAqlFunctionsResponse
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
        /// All functions, or the ones matching
        /// the <see cref="GetAqlFunctionsQuery.Namespace"/> parameter.
        /// </summary>
        public IList<AqlFunctionResult> Result { get; set; }
    }
}
