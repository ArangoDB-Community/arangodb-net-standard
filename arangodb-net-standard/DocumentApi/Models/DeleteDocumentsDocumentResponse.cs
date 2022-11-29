namespace ArangoDBNetStandard.DocumentApi.Models
{
    /// <summary>
    /// Represents the response for one document when deleting multiple document.
    /// </summary>
    /// <typeparam name="T">The type of the deserialized old document object when requested.</typeparam>
    public class DeleteDocumentsDocumentResponse<T> : DeleteDocumentResponse<T>
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
        /// ArangoDB error message.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// ArangoDB error number.
        /// </summary>
        public int ErrorNum { get; set; }
    }
}