using System.Net;

namespace ArangoDBNetStandard.CursorApi.Models
{
    /// <summary>
    /// Represents a response returned after deleting a cursor.
    /// </summary>
    public class DeleteCursorResponse
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
        /// The id of the cursor.
        /// </summary>
        public string Id { get; set; }
    }
}
