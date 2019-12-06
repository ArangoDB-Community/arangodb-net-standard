using System.Net;

namespace ArangoDBNetStandard.GraphApi
{
    public class PutEdgeResponse<T>
    {
        public bool Error { get; set; }

        public HttpStatusCode Code { get; set; }

        public T Old { get; set; }

        public T New { get; set; }

        public PutEdgeResult Edge { get; set; }
    }
}
