using System.Net;

namespace ArangoDBNetStandard.DatabaseApi
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
        /// Error specified in Arango Docs
        /// </summary>
        public bool Error { get; set; }
    }
}
