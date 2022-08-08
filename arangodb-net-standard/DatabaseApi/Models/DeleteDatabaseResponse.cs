using System.Net;

namespace ArangoDBNetStandard.DatabaseApi.Models
{
    public class DeleteDatabaseResponse
    {
        /// <summary>
        /// HttpStatus
        /// </summary>
        public HttpStatusCode Code { get; set; }

        /// <summary>
        /// True if the database was deleted, otherwise see <see cref="Code"/>
        /// </summary>
        public bool Result { get; set; }

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
    }
}
