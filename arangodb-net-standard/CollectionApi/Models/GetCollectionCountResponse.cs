using System.Net;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class GetCollectionCountResponse
    {
        public bool Error { get; set; }

        public HttpStatusCode Code { get; set; }

        public bool CacheEnabled { get; set; }

        public CollectionKeyOptions KeyOptions { get; set; }

        public int Count { get; set; }

        public bool IsSystem { get; set; }

        public string GloballyUniqueId { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public int Status { get; set; }

        public string StatusString { get; set; }

        public CollectionType Type { get; set; }

        public bool WaitForSync { get; set; }
    }
}
