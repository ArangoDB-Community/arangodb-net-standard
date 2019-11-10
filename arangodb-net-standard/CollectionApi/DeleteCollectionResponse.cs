using System.Net;

namespace ArangoDBNetStandard.CollectionApi
{
    public class DeleteCollectionResponse
    {
        public bool Error { get; set; }

        public HttpStatusCode Code { get; set; }

        public string Id { get; set; }
    }
}