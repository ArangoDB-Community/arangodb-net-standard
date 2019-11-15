using System.Net;

namespace ArangoDBNetStandard.GraphApi
{
    public class PostVertexCollectionResponse
    {
        public PostGraphVertex New { get; set; }

        public PostGraphVertex Vertex { get; set; }

        public HttpStatusCode Code { get; set; }

        public bool Error { get; set; }
    }
}