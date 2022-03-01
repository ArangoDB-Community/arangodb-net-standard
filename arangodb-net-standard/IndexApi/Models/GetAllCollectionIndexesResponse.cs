using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.IndexApi.Models
{
    /// <summary>
    /// Response from <see cref="IndexApiClient.GetAllCollectionIndexesAsync(GetAllCollectionIndexesQuery)"/>
    /// </summary>
    public class GetAllCollectionIndexesResponse : ResponseBase
    {
        /// <summary>
        /// The list of all indexes for the collection.
        /// </summary>
        public IEnumerable<IndexResponseBase> Indexes { get; set; }

        /// <summary>
        /// All the indexes for the collection as a dictionary where each key is the index ID.
        /// </summary>
        public Dictionary<string, IndexResponseBase> Identifiers { get; set; }
    }
}
