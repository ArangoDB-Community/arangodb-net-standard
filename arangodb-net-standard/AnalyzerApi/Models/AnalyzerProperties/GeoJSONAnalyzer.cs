using System;

namespace ArangoDBNetStandard.AnalyzerApi.Models.AnalyzerProperties
{
    /// <summary>
    /// Properties for GeoJSON analyzer.
    /// </summary>
    public class GeoJSONAnalyzer : AnalyzerPropertiesBase
    {
        /// <summary>
        /// Index all GeoJSON geometry types (Point, Polygon etc.)
        /// (default).
        /// </summary>
        public static string TypeShape = "shape";

        /// <summary>
        /// Compute and only index the centroid of the input 
        /// geometry
        /// </summary>
        public static string TypeCentroid = "centroid";

        /// <summary>
        /// Only index GeoJSON objects of type Point, ignore all 
        /// other geometry types
        /// </summary>
        public static string TypePoint = "point";

        /// <summary>
        /// Determines the type of indexing to use.
        /// Possible values are <see cref="TypeCentroid"/>, 
        /// <see cref="TypeShape"/> and <see cref="TypePoint"/>
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Options for fine-tuning geo queries. 
        /// These options should generally remain unchanged.
        /// </summary>
        public GeoJSONAnalyzerOptions Options { get; set; }
    }
}
