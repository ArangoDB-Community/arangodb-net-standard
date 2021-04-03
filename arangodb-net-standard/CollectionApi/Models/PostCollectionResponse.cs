using System.Net;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class PostCollectionResponse
    {
        public bool Error { get; set; }

        public HttpStatusCode Code { get; set; }

        /// <summary>
        /// If true then the data is synchronized to disk before returning
        /// from a document create, update, replace or removal operation.
        /// (default: false)
        /// </summary>
        public bool WaitForSync { get; set; }

        /// <summary>
        /// The type of the collection.
        /// </summary>
        public CollectionType Type { get; set; }

        public int Status { get; set; }

        /// <summary>
        /// The maximal size of a journal or datafile in bytes.
        /// This option is meaningful for the MMFiles storage engine only.
        /// </summary>
        public long JournalSize { get; set; }

        public PostCollectionResponseCollectionKeyOptions KeyOptions { get; set; }

        public string GloballyUniqueId { get; set; }

        public string StatusString { get; set; }

        public string Id { get; set; }

        /// <summary>
        /// The name of the collection.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Deprecated.
        /// Whether or not the collection is compacted.
        /// This option is meaningful for the MMFiles storage engine only.
        /// </summary>
        public bool DoCompact { get; set; }

        /// <summary>
        /// If true then the collection data is kept in-memory only and not made persistent.
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// Deprecated.
        /// The number of buckets into which indexes using a hash table are split.
        /// This option is meaningful for the MMFiles storage engine only.
        /// </summary>
        public int IndexBuckets { get; set; }

        /// <summary>
        /// Deprecated.
        /// If true then the collection data is kept in-memory only and not made persistent.
        /// This option is meaningful for the MMFiles storage engine only.
        /// </summary>
        public bool IsVolatile { get; set; }

    }
}