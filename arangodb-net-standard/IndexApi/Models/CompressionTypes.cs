using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.IndexApi.Models
{
    /// <summary>
    /// List of compression types for indexes.
    /// </summary>
    public static class CompressionTypes
    {
        /// <summary>
        /// Use LZ4 fast compression.
        /// </summary>
        public const string LZ4 = "lz4";

        /// <summary>
        /// Disable compression to trade space for speed.
        /// </summary>
        public const string None = "none";
    }
}
