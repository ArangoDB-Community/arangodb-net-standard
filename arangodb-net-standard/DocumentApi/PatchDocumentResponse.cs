using System.Net;

namespace ArangoDBNetStandard.DocumentApi
{
    public class PatchDocumentResponse<T>
    {
        public HttpStatusCode Code { get; set; }

        public PatchDocumentResult<T> Result { get; set; }
    }
}
