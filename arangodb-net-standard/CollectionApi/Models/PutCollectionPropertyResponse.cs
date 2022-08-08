using System.Net;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class PutCollectionPropertyResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool WaitForSync { get; set; }

        [System.Obsolete()]
        public long JournalSize { get; set; }

        public int Status { get; set; }

        public CollectionType Type { get; set; }

        public bool IsSystem { get; set; }

        [System.Obsolete()]
        public bool IsVolatile { get; set; }

        [System.Obsolete()]
        public bool DoCompact { get; set; }

        public CollectionKeyOptions KeyOptions { get; set; }

        public string GloballyUniqueId { get; set; }

        /// <summary>
        /// Indicates whether an error occurred
        /// </summary>
        /// <remarks>
        /// Note that in cases where an error occurs, the ArangoDBNetStandard
        /// client will throw an <see cref="ApiErrorException"/> rather than
        /// populating this property. A try/catch block should be used instead
        /// for any required error handling.
        /// </remarks>
        public bool Error { get; set; }

        public HttpStatusCode Code { get; set; }

        public string StatusString { get; set; }

        [System.Obsolete()]
        public int IndexBuckets { get; set; }
    }
}
