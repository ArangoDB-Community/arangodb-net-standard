using System.Net;

namespace ArangoDBNetStandard.GraphApi
{
    public class PostVertexResponse
    {
        public PostVertexResult New { get; set; }

        public PostVertexResult Vertex { get; set; }

        public HttpStatusCode Code { get; set; }

        public bool Error { get; set; }
    }
}
