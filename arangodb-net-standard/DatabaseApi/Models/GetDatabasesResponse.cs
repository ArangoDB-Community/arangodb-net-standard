using System.Collections.Generic;
using System.Net;

namespace ArangoDBNetStandard.DatabaseApi.Models
{
    /// <summary>
    /// Represents the content of the response returned
    /// by an endpoint that gets the list of databases.
    /// </summary>
    public class GetDatabasesResponse
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
        /// The list of databases.
        /// </summary>
        public IEnumerable<string> Result { get; set; }
    }
}
