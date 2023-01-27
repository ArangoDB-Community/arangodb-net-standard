namespace ArangoDBNetStandard.IndexApi.Models
{
    /// <summary>
    /// Request body to create a Geo-Spatial index
    /// </summary>
    public class PostGeoSpatialIndexBody : PostIndexBody
    {
        public PostGeoSpatialIndexBody()
        {
            Type = IndexTypes.Geo;
        }

        /// <summary>
        /// Optional. If a geo-spatial index on a location 
        /// is constructed and geoJson is true, then the 
        /// order within the array is longitude followed 
        /// by latitude. 
        /// </summary>
        /// <remarks>
        /// This corresponds to the format described in
        /// http://geojson.org/geojson-spec.html#positions
        /// </remarks>
        public bool? GeoJson { get; set; }

        /// <summary>
        /// Introduced in v3.10.
        /// A geo index with legacyPolygons set to true
        /// will use the old, pre-3.10 rules for the parsing
        /// GeoJSON polygons. This allows you to let old 
        /// indexes produce the same, potentially wrong 
        /// results as before an upgrade. A geo index with
        /// legacyPolygons set to false will use the new, 
        /// correct and consistent method for parsing of
        /// GeoJSON polygons.
        /// </summary>
        public bool? LegacyPolygons { get; set; }
    }
}