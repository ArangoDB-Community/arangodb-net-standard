using System.Collections.Generic;
using System.Net;

namespace ArangoDBNetStandard.DocumentApi
{
    public class PatchDocumentsResponse
    {
        public HttpStatusCode Code { get; set; }

        public IList<PatchDocument> Documents { get; set; }
    }
}
