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
    }
}