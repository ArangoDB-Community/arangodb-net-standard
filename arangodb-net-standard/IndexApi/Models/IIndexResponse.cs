using System.Collections.Generic;

namespace ArangoDBNetStandard.IndexApi.Models
{
    /// <summary>
    /// Represents information about an index returned in responses.
    /// </summary>
    public interface IIndexResponse
    {
        /// <summary>
        /// Id of the index
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Name of the index
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The attributes that are part of the indexed.
        /// Depending on the index type, a single attribute or multiple attributes can be indexed.
        /// </summary>
        IEnumerable<string> Fields { get; set; }

        /// <summary>
        /// Indicates whether the index is a sparse index or not.
        /// </summary>
        bool? Sparse { get; set; }

        /// <summary>
        /// Type of index. See <see cref="IndexTypes"/>.
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Indicates whether the index is a unique or non-unique index.
        /// </summary>
        bool? Unique { get; set; }

        /// <summary>
        /// Indicates whether index selectivity estimates are maintained for the index.
        /// </summary>
        bool? Estimates { get; set; }

        /// <summary>
        /// The index selectivity estimate value for the index if <see cref="Estimates"/> is set to True.
        /// </summary>
        double? SelectivityEstimate { get; set; }

        /// <summary>
        /// Indicates whether inserting duplicate index values from the same document
        /// into a unique array index will lead to a unique constraint error or not.
        /// </summary>
        bool? Deduplicate { get; set; }

        /// <summary>
        /// Is Geo Json enabled/disabled for the index.
        /// </summary>
        /// <remarks>
        /// This corresponds to the format described in http://geojson.org/geojson-spec.html#positions
        /// </remarks>
        bool? GeoJson { get; set; }

        /// <summary>
        /// The time interval (in seconds) from the point in time stored in the <see cref="Fields"/> property
        /// after which the documents count as expired.
        /// Can be set to 0 to let documents expire as soon as the server time
        /// passes the point in time stored in the document attribute,
        /// or to a higher number to delay the expiration.
        /// </summary>
        int? ExpireAfter { get; set; }

        /// <summary>
        /// Minimum character length of words to index.
        /// </summary>
        int? MinLength { get; set; }

        /// <summary>
        /// Indicates a newly created index.
        /// </summary>
        bool? IsNewlyCreated { get; set; }

        /// <summary>
        /// For more information,
        /// see https://www.arangodb.com/docs/stable/http/indexes-geo.html
        /// </summary>
        int? MaxNumCoverCells { get; set; }

        /// <summary>
        /// For more information,
        /// see https://www.arangodb.com/docs/stable/http/indexes-geo.html
        /// </summary>
        int? BestIndexedLevel { get; set; }

        /// <summary>
        /// For more information,
        /// see https://www.arangodb.com/docs/stable/http/indexes-geo.html
        /// </summary>
        int? WorstIndexedLevel { get; set; }

        /// <summary>
        /// Introduced in v3.10.
        /// An array of additional index attribute paths in a persistent index.
        /// These additional attributes cannot be used for index lookups or
        /// sorts, but they can be used for projections.
        /// </summary>
        List<string> StoredValues { get; set; }

        /// <summary>
        /// Introduced in v3.10.
        /// If true, an in-memory cache is enabled for 
        /// index values for persistent indexes.
        /// </summary>
        bool? CacheEnabled { get; set; }

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
        bool? LegacyPolygons { get; set; }
    }
}