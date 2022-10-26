using System.Net;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class GetCollectionFiguresResponse
    {
        public FiguresResult Figures { get; set; }

        public CollectionKeyOptions KeyOptions { get; set; }

        public string GloballyUniqueId { get; set; }

        public string StatusString { get; set; }

        public string Id { get; set; }

        [System.Obsolete()]
        public int IndexBuckets { get; set; }

        public string Error { get; set; }

        public HttpStatusCode Code { get; set; }

        public CollectionType Type { get; set; }

        public int Status { get; set; }

        [System.Obsolete()]
        public int JournalSize { get; set; }

        [System.Obsolete()]
        public bool IsVolatile { get; set; }

        public string Name { get; set; }

        [System.Obsolete()]
        public bool DoCompact { get; set; }

        public bool IsSystem { get; set; }

        public int Count { get; set; }

        public bool WaitForSync { get; set; }
    }
}
