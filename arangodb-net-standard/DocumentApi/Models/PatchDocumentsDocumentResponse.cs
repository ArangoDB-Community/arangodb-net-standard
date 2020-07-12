namespace ArangoDBNetStandard.DocumentApi.Models
{
    /// <summary>
    /// Represents the response for one document when updating multiple document.
    /// </summary>
    /// <typeparam name="T">The type of the deserialized new/old document object when requested.</typeparam>
    public class PatchDocumentsDocumentResponse<T> : PatchDocumentResponse<T>
    {
        /// <summary>
        /// Indicates whether an error occurred.
        /// </summary>
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
