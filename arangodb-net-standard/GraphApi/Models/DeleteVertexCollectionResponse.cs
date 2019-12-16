using System.Net;

namespace ArangoDBNetStandard.GraphApi.Models
{
    public class DeleteVertexCollectionResponse
    {
        public bool Error { get; set; }

        public HttpStatusCode Code { get; set; }

        public GraphResult Graph { get; set; }
    }
}
