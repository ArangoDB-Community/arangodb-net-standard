using System.Collections.Generic;
using System.Net;

namespace ArangoDBNetStandard.CursorApi.Models
{
    /// <summary>
    /// Represents common properties in the response from Cursor API endpoints.
    /// </summary>
    /// <typeparam name="T">The type of the document deserialized in the results.</typeparam>
    public interface ICursorResponse<T>
    {
        /// <summary>
        /// A flag to indicate that an error occurred (false in this case).
        /// </summary>
        bool Error { get; set; }

        /// <summary>
        /// The HTTP status code.
        /// </summary>
        HttpStatusCode Code { get; set; }

        /// <summary>
        /// The cursor identifier.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// A list of documents for the current batch of results
        /// (might be empty if query has no results).
        /// </summary>
        IEnumerable<T> Result { get; set; }

        /// <summary>
        /// The total number of result documents available
        /// (only available if requested in the initial cursor query).
        /// </summary>
        long Count { get; set; }

        /// <summary>
        /// Whether more results are available to fetch from the cursor.
        /// False if this was the last batch.
        /// </summary>
        bool HasMore { get; set; }
    }
}
