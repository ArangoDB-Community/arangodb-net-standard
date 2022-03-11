using System.Collections.Generic;
using System.Net;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Response from
    /// <see cref="AqlFunctionApiClient.GetCurrentlyRunningAqlQueriesAsync(GetCurrentlyRunningAqlQueriesQuery)"/>
    /// </summary>
    public class GetCurrentlyRunningAqlQueriesResponse : List<RunningAqlQuery>
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
