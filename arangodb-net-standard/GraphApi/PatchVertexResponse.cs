using System.Net;

namespace ArangoDBNetStandard.GraphApi
{
    public class PatchVertexResponse<T>
    {
        public T New { get; set; }

        public T Old { get; set; }

        public HttpStatusCode Code { get; set; }

        public PatchVertexResult Vertex { get; set; }

        public bool Error { get; set; }
    }
}
