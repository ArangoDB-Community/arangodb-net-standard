using System.Net;

namespace ArangoDBNetStandard.DatabaseApi.Models
{
    /// <summary>
    /// Represents a response containing information about the current database.
    /// </summary>
    public class GetCurrentDatabaseInfoResponse
    {

        /// <summary>
        /// Indicates whether an error occurred
        /// </summary>
        /// <remarks>
        /// Note that in cases where an error occurs, the ArangoDBNetStandard
        /// client will throw an <see cref="ApiErrorException"/> rather than
        /// populating this property. A try/catch block should be used instead
        /// for any required error handling.
        /// </remarks>
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
