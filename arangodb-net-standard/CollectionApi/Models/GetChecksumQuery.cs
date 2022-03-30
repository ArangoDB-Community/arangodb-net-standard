using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    /// <summary>
    /// Generates query for <see cref="ICollectionApiClient.GetChecksumAsync(string, GetChecksumQuery)"/>
    /// </summary>
    public class GetChecksumQuery
    {
        /// <summary>
        /// Optional. Indicates whether or not to include document 
        /// revision ids in the checksum calculation.
        /// When true, then revision ids (_rev system attributes)
        /// are included in the checksumming.
        /// </summary>
        public bool? WithRevisions { get; set; }

        /// <summary>
        /// Optional. Indicates whether or not to include document 
        /// body data in the checksum calculation.
        /// When true, the user-defined document attributes will
        /// be included in the calculation too.
        /// Note: Including user-defined attributes will 
        /// make the checksumming slower.
        /// </summary>
        public bool? WithData { get; set; }

        internal string ToQueryString()
        {
            List<string> query = new List<string>();
            if (WithRevisions != null)
            {
                query.Add("withRevisions=" + WithRevisions.ToString().ToLower());
            }
            if (WithData != null)
            {
                query.Add("withData=" + WithData.ToString().ToLower());
            }
            return string.Join("&", query);
        }
    }
}