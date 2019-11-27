using System.Net;

namespace ArangoDBNetStandard.GraphApi
{
    public class PutGraphEdgeResponse<T>
    {
        public bool Error { get; set; }

        public HttpStatusCode Code { get; set; }

        public T Old { get; set; }

        public T New { get; set; }

        public PutGraphEdgeResult Edge { get; set; }
    }
}
