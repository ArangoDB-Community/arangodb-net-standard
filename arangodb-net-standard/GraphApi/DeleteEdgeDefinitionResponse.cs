using System.Net;

namespace ArangoDBNetStandard.GraphApi
{
    public class DeleteEdgeDefinitionResponse
    {
        public HttpStatusCode Code { get; set; }

        public bool Error { get; set; }

        public GetGraphsGraph Graph { get; set; }
    }
}
