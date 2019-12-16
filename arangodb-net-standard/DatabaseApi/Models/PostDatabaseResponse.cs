using System.Net;

namespace ArangoDBNetStandard.DatabaseApi.Models
{
    /// <summary>
    /// Represents the content of the response returned
    /// by an endpoint that creates a new database.
    /// </summary>
    public class PostDatabaseResponse
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
        /// Indicates that the database was created. Always true.
        /// </summary>
        public bool Result { get; set; }
    }
}
