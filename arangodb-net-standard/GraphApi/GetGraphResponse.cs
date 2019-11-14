using System.Net;

namespace ArangoDBNetStandard.GraphApi
{
    public class GetGraphResponse
    {
        public GetGraphsGraph Graph { get; set; }

        public HttpStatusCode Code { get; set; }

        public bool? Error { get; set; }
    }
}
