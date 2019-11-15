using System.Collections.Generic;
using System.Net;

namespace ArangoDBNetStandard.GraphApi
{
    public class GetGraphEdgesResponse
    {
        public bool Error { get; set; }

        public int ErrorNum { get; set; }

        public HttpStatusCode Code { get; set; }

        public IEnumerable<string> Collections { get; set; }
    }
}
