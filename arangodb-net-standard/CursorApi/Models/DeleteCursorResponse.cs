using System.Net;

namespace ArangoDBNetStandard.CursorApi.Models
{
    /// <summary>
    /// Represents a response returned after deleting a cursor.
    /// </summary>
    public class DeleteCursorResponse
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
        /// The id of the cursor.
        /// </summary>
        public string Id { get; set; }
    }
}
