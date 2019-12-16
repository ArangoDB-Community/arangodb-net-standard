using System.Net;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class TruncateCollectionResponse
    {
        public bool Error { get; set; }

        public HttpStatusCode Code { get; set; }

        public int Status { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public bool IsSystem { get; set; }

        public string GloballyUniqueId { get; set; }

        public string Id { get; set; }
    }
}