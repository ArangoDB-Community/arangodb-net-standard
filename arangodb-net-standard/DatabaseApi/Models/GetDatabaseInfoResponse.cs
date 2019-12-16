using System.Net;

namespace ArangoDBNetStandard.DatabaseApi.Models
{
    /// <summary>
    /// Represents a response containing information about the current database.
    /// </summary>
    public class GetCurrentDatabaseInfoResponse
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
        /// The database information.
        /// </summary>
        public CurrentDatabaseInfo Result { get; set; }
    }
}
