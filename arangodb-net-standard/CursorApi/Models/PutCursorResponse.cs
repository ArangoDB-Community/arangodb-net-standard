using System.Collections.Generic;
using System.Net;

namespace ArangoDBNetStandard.CursorApi.Models
{
    /// <summary>
    /// Represents a response returned when reading next batch of results
    /// from an existing cursor.
    /// </summary>
    /// <remarks>
    /// Note that even if <see cref="HasMore"/> returns true, the next call might still return no documents.
    /// If, however, <see cref="HasMore"/> is false, then the cursor is exhausted.
    /// Once the <see cref="HasMore"/> has a value of false, the client can stop.
    /// </remarks>
    /// <typeparam name="T">The type of the document deserialized in the results.</typeparam>
    public class PutCursorResponse<T> : ICursorResponse<T>
    {
        /// <summary>
        /// The cursor identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// A list of documents for the current batch.
        /// </summary>
        public IEnumerable<T> Result { get; set; }

        /// <summary>
        /// Whether more results are available to fetch from the cursor.
        /// False if this was the last batch.
        /// </summary>
        public bool HasMore { get; set; }

        /// <summary>
        /// The total number of result documents available
        /// (only available if requested in the initial cursor query).
        /// </summary>
        public long Count { get; set; }

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
    }
}