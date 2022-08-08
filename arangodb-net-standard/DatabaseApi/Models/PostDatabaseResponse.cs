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
        /// Indicates that the database was created. Always true.
        /// </summary>
        public bool Result { get; set; }
    }
}
