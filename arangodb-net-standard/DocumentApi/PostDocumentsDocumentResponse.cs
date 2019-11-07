namespace ArangoDBNetStandard.DocumentApi
{
    /// <summary>
    /// Response model for a single POST Document request.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PostDocumentsDocumentResponse<T> : PostDocumentResponse<T>
    {
        public bool Error { get; set; }

        public string ErrorMessage { get; set; }

        public ErrorCode ErrorNum { get; set; }

        public int Code { get; set; }
    }
}