using System.Net;

namespace ArangoDBNetStandard.DocumentApi
{
    public class PatchDocumentResponse<U>
    {
        public HttpStatusCode Code { get; set; }

        public PatchDocumentResult<U> Result { get; set; }
    }
}
