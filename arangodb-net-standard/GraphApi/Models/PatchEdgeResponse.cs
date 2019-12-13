using System.Net;

namespace ArangoDBNetStandard.GraphApi.Models
{
    public class PatchEdgeResponse<T>
    {
        public HttpStatusCode Code { get; set; }

        public bool Error { get; set; }

        public T New { get; set; }

        public T Old { get; set; }

        public PatchEdgeResult Edge { get; set; }
    }
}
