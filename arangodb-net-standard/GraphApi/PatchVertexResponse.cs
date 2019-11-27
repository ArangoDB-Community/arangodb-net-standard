using System.Net;

namespace ArangoDBNetStandard.GraphApi
{
    public class PatchVertexResponse<U>
    {
        public U New { get; set; }

        public U Old { get; set; }

        public HttpStatusCode Code { get; set; }

        public PatchVertexResult Vertex { get; set; }

        public bool Error { get; set; }
    }
}
