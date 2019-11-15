using System.Net;

namespace ArangoDBNetStandard.CollectionApi
{
    public class GetCollectionRevisionResponse
    {
        public bool Error { get; set; }

        public HttpStatusCode Code { get; set; }

        public bool WaitForSync { get; set; }

        public int JournalSize { get; set; }

        public bool IsVolatile { get; set; }

        public bool IsSystem { get; set; }

        public int IndexBuckets { get; set; }

        public CollectionKeyOptions KeyOptions { get; set; }

        public string GloballyUniqueId { get; set; }

        public string StatusString { get; set; }

        public string Id { get; set; }

        public string Revision { get; set; }

        public int Status { get; set; }

        public int Type { get; set; }

        public string Name { get; set; }

        public bool DoCompact { get; set; }
    }
}
