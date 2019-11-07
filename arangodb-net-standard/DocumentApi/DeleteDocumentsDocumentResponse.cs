using System.Net;

namespace ArangoDBNetStandard.DocumentApi
{
    public class DeleteDocumentsDocumentResponse<T>: DeleteDocumentResponse<T>
    {
        public bool Error { get; set; }

        public string ErrorMessage { get; set; }

        public int ErrorNum { get; set; }

        public HttpStatusCode Code { get; set; }
    }
}