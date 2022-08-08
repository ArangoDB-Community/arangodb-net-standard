using System.Net;

namespace ArangoDBNetStandard.DocumentApi.Models
{
    /// <summary>
    /// Response model for a single POST Document request.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PostDocumentsDocumentResponse<T> : PostDocumentResponse<T>
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

        public string ErrorMessage { get; set; }

        public int ErrorNum { get; set; }
    }
}