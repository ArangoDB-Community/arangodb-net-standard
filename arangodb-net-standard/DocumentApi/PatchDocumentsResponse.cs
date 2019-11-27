using System.Collections.Generic;
using System.Net;

namespace ArangoDBNetStandard.DocumentApi
{
    public class PatchDocumentsResponse<U>
    {
        public HttpStatusCode Code { get; set; }

        public IList<PatchDocument<U>> Documents { get; set; }
    }
}
