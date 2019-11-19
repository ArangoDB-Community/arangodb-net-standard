using System.Net;

namespace ArangoDBNetStandard.DocumentApi
{
    public class PatchDocumentResponse
    {
        public HttpStatusCode Code { get; set; }

        public PatchDocumentResult Result { get; set; }
    }
}
