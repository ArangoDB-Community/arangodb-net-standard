using System.Collections.Generic;
using System.Net;

namespace ArangoDBNetStandard.DocumentApi
{
    public class PatchDocumentsResponse<T>
    {
        public HttpStatusCode Code { get; set; }

        public IList<PatchDocument<T>> Documents { get; set; }
    }
}
