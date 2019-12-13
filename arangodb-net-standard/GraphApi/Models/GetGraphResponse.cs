using System.Net;

namespace ArangoDBNetStandard.GraphApi.Models
{
    public class GetGraphResponse
    {
        public GraphResult Graph { get; set; }

        public HttpStatusCode Code { get; set; }

        public bool Error { get; set; }
    }
}
