using System.Net;

namespace ArangoDBNetStandard.GraphApi
{
    public class PutVertexResponse<T>
    {
        public T New { get; set; }

        public T Old { get; set; }

        public HttpStatusCode Code { get; set; }

        public PutVertexResult Vertex { get; set; }

        public bool Error { get; set; }
    }
}
