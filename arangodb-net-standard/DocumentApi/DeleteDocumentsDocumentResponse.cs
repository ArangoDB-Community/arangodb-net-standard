using System.Net;

namespace ArangoDBNetStandard.DocumentApi
{
    public class DeleteDocumentsDocumentResponse<T>: DeleteDocumentResponse<T>
    {
        public bool Error { get; set; }

        public string ErrorMessage { get; set; }

        public ErrorCode ErrorNum { get; set; }

        public HttpStatusCode Code { get; set; }
    }
}