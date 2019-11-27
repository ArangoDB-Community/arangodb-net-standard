using System.Net;

namespace ArangoDBNetStandard.GraphApi
{
    public class PatchEdgeResponse<U>
    {
        public HttpStatusCode Code { get; set; }

        public bool Error { get; set; }

        public U New { get; set; }

        public U Old { get; set; }

        public PatchEdgeresult Edge { get; set; }
    }
}
