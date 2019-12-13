using System.Net;

namespace ArangoDBNetStandard.GraphApi.Models
{
    public class DeleteGraphResponse
    {
        public bool Error { get; set; }

        public HttpStatusCode Code { get; set; }

        public bool Removed { get; set; }
    }
}
