using System.Net;

namespace ArangoDBNetStandard.GraphApi
{
    public class DeleteVertexResponse<T>
    {
        public HttpStatusCode Code { get; set; }

        public bool Error { get; set; }

        public T Old { get; set; }

        public bool Removed { get; set; }
    }
}
