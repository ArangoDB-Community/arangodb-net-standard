using System.Collections.Generic;
using System.Net;

namespace ArangoDBNetStandard.GraphApi
{
    public class GetGraphsResponse
    {
        public IEnumerable<GetGraphsGraph> Graphs { get; set; }

        public HttpStatusCode Code { get; set; }

        public bool Error { get; set; }
    }
}
