using System.Net;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class GetCollectionResponse
    {
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

        /// <summary>
        /// The HTTP status code.
        /// </summary>
        public HttpStatusCode Code { get; set; }

        /// <summary>
        /// The type of the collection.
        /// </summary>
        public CollectionType Type { get; set; }

        /// <summary>
        /// If true then the collection is a system collection.
        /// </summary>
        public bool IsSystem { get; set; }

        public string GloballyUniqueId { get; set; }

        /// <summary>
        /// The identifier of the collection.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the collection.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The status of the collection as number.
        /// </summary>
        /// <remarks>
        /// 1: new born collection
        /// 2: unloaded
        /// 3: loaded
        /// 4: in the process of being unloaded
        /// 5: deleted
        /// 6: loading
        /// ?: Every other status indicates a corrupted collection.
        /// </remarks>
        public int Status { get; set; }
    }
}
