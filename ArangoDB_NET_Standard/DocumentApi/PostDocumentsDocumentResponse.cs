namespace ArangoDB_NET_Standard.DocumentApi
{
    public class PostDocumentsDocumentResponse<T> : PostDocumentResponse<T>
    {
        public bool Error { get; set; }

        public string ErrorMessage { get; set; }

        public int ErrorNum { get; set; }

        public int Code { get; set; }
    }
}