namespace ArangoDBNetStandard.DocumentApi.Models
{
    public class PatchDocumentsResponse<T> : PatchDocumentResponse<T>
    {
        public bool Error { get; set; }

        /// <summary>
        /// Error message.
        /// </summary>
        public string ErrorMessage { get; set; }

        public int ErrorNum { get; set; }
    }
}
