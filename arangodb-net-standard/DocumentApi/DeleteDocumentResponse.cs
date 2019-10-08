using System.Net;

namespace ArangoDBNetStandard.DocumentApi
{
    public class DeleteDocumentResponse
    {
        public int StatusCode { get; private set; }

        public DeleteDocumentResponse(int statusCode)
        {
            this.StatusCode = statusCode;
        }
    }
}