using System.Collections.Generic;
using System.Net;

namespace ArangoDBNetStandard.GraphApi.Models
{
    public class GetEdgeCollectionsResponse
    {
        public bool Error { get; set; }

        public int ErrorNum { get; set; }

        public HttpStatusCode Code { get; set; }

        public IEnumerable<string> Collections { get; set; }
    }
}
