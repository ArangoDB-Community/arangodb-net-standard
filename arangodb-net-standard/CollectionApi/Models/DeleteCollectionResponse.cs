using System.Net;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class DeleteCollectionResponse
    {
        public bool Error { get; set; }

        public HttpStatusCode Code { get; set; }

        public string Id { get; set; }
    }
}