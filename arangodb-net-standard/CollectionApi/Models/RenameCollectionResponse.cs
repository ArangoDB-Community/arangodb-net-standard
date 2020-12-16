using System.Net;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class RenameCollectionResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Status { get; set; }

        public CollectionType Type { get; set; }

        public bool IsSystem { get; set; }

        public HttpStatusCode Code { get; set; }

        public bool Error { get; set; }
    }
}
