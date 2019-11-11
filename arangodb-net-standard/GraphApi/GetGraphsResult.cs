using System.Collections.Generic;
using System.Net;

namespace ArangoDBNetStandard.GraphApi
{
    public class GetGraphsResult
    {
        public IEnumerable<GetGraphsGraphResult> Graphs { get; set; }

        public HttpStatusCode Code { get; set; }

        public bool Error { get; set; }
    }
}
