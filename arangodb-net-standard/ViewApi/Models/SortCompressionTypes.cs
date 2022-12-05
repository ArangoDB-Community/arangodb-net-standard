using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.ViewApi.Models
{
    /// <summary>
    /// Defines values for ArangoDB sort compression types.
    /// Possible value for <see cref="ViewDetails.PrimarySortCompression"/>
    /// </summary>
    public static class SortCompressionTypes
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