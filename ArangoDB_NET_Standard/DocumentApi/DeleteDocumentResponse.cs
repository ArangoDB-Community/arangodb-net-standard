using System.Net;

namespace ArangoDB_NET_Standard.DocumentApi
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