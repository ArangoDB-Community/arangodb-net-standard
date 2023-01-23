using System.Collections.Generic;

namespace ArangoDBNetStandard.IndexApi.Models
{
    /// <summary>
    /// Stored value for inverted index.
    /// </summary>
    public class InvertedIndexStoredValue
    {
        /// <summary>
        /// A list of attribute paths. 
        /// The . character denotes sub-attributes.
        /// </summary>
        public IEnumerable<string> Fields { get; set; }

        /// <summary>
        /// Optional. Defines how to compress the 
        /// attribute values. Possible values:
        /// <see cref="CompressionTypes.None"/> and
        /// <see cref="CompressionTypes.LZ4"/>
        /// </summary>
        public string Compression { get; set; }

        /// <summary>
        /// Optional. Enable this option to always cache
        /// stored values in memory. This can improve the
        /// query performance if stored values are involved.
        /// Default: false
        /// </summary>
        public bool? Cache { get; set; }
    }
}