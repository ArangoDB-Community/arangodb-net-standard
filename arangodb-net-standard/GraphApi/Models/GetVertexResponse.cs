using System.Net;

namespace ArangoDBNetStandard.GraphApi.Models
{
    public class GetVertexResponse<T>
    {
        public HttpStatusCode Code { get; set; }

        public T Vertex { get; set; }

        public bool Error { get; set; }
    }
}
