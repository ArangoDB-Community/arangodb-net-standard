using System.Net;

namespace ArangoDBNetStandard.DatabaseApi.Models
{
    /// <summary>
    /// Represents a response containing information about the current database.
    /// </summary>
    public class GetCurrentDatabaseInfoResponse
    {
        /// <summary>
        /// Always false.
        /// </summary>
        /// <remarks>
        /// To handle errors, catch <see cref="ApiErrorException"/>
        /// thrown by API client methods.
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
