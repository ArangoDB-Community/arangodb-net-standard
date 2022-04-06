using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    /// <summary>
    /// Response from <see cref="ICollectionApiClient.GetChecksumAsync(string, GetChecksumQuery)"/>
    /// </summary>
    public class GetChecksumResponse:ResponseBase
    {
        /// <summary>
        /// The globally unique Id of the collection
        /// </summary>
        public string GloballyUniqueId { get; set; }

        /// <summary>
        /// Indicates whether it is a system collection.
        /// </summary>
        public bool? IsSystem { get; set; }

        /// <summary>
        /// The collection type.
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        /// The collection status.
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// The name of the collection.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Id of the collection.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The collection revision id.
        /// </summary>
        public string Revision { get; set; }

        /// <summary>
        /// The calculated checksum.
        /// </summary>
        public string Checksum { get; set; }
    }
}