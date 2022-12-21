using System.Collections.Generic;

namespace ArangoDBNetStandard.AnalyzerApi.Models.AnalyzerProperties
{
    /// <summary>
    /// Properties for GeoPoint analyzer.
    /// </summary>
    public class GeoPointAnalyzer : AnalyzerPropertiesBase
    {
        /// <summary>
        /// A list of strings that describes the attribute path 
        /// of the latitude value relative to the field for 
        /// which the Analyzer is defined in the View.
        /// </summary>
        public List<string> Latitude { get; set; }

        /// <summary>
        /// A list of  strings that describes the attribute path
        /// of the longitude value relative to the field for 
        /// which the Analyzer is defined in the View.
        /// </summary>
        public List<string> Longitude { get; set; }

        /// <summary>
        /// Options for fine-tuning geo queries. 
        /// These options should generally remain unchanged.
        /// </summary>
        public GeoJSONAnalyzerOptions Options { get; set; }
    }
}
