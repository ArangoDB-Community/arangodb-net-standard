using System.Collections.Generic;

namespace ArangoDBNetStandard.ViewApi.Models
{
    /// <summary>
    /// Describes which document attributes 
    /// to store in a View index.
    /// </summary>
    public class ViewStoredValue
    {
        /// <summary>
        /// One or more document attribute paths. 
        /// The specified attributes are placed 
        /// into a single column of the index. 
        /// A column with all fields that are 
        /// involved in common search queries 
        /// is ideal for performance. The column 
        /// should not include too many unneeded
        /// fields.
        /// </summary>
        public List<string> Fields { get; set; }

        /// <summary>
        /// Optional when creating views. Defines 
        /// the compression type used for the 
        /// internal column-store, which can be 
        /// "lz4" (LZ4 fast compression, default) 
        /// OR "none" (no compression).
        /// </summary>
        public string Compression { get; set; }
    }
}