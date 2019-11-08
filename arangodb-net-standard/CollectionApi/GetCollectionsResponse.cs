using System.Collections.Generic;
using System.Net;

namespace ArangoDBNetStandard.CollectionApi
{
    public class GetCollectionsResponse
    {
        public bool Error { get; set; }
        public HttpStatusCode Code { get; set; }
        public List<GetCollectionsResponseResult> Result { get; set; }
    }
}