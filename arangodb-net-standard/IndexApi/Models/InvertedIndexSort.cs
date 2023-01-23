using System.Collections.Generic;

namespace ArangoDBNetStandard.IndexApi.Models
{
    public class InvertedIndexSort
    {
        public IEnumerable<InvertedIndexSortItem> Fields { get; set; }

        /// <summary>
        /// Optional. Defines how to compress the 
        /// primary sort data. Possible values:
        /// "lz4" (default): use LZ4 fast compression.
        /// "none": disable compression to trade space for speed.
        /// </summary>
        public string Compression { get; set; }

        /// <summary>
        /// Optional. Enable this option to always cache the 
        /// primary sort columns in memory. This can improve
        /// the performance of queries that utilize the primary 
        /// sort order.
        /// Default: false
        /// </summary>
        public bool? Cache { get; set; }
    }
}