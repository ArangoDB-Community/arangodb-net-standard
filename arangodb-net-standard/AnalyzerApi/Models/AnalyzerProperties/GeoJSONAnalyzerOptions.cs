using System;

namespace ArangoDBNetStandard.AnalyzerApi.Models.AnalyzerProperties
{
    /// <summary>
    /// Options for fine-tuning geo queries.
    /// </summary>
    public class GeoJSONAnalyzerOptions
    {
        /// <summary>
        /// Maximum number of S2 cells (default: 20)
        /// </summary>
        public int? MaxCells { get; set; }

        /// <summary>
        /// The least precise S2 level (default: 4)
        /// </summary>
        public int? MinLevel { get; set; }

        /// <summary>
        /// The most precise S2 level (default: 23)
        /// </summary>
        public int? MaxLevel { get; set; }
    }
}
