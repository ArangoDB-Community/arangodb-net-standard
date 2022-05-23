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
        /// Applies to indexes of type <see cref="IndexTypes.Persistent"/>.
        /// Indicates whether the index is a sparse index or not.
        /// Sparse indexes do not index documents for which any of the index attributes
        /// is either not set or is null.
        /// </summary>
        bool? Sparse { get; set; }

        /// <summary>
        /// Type of index. See <see cref="IndexTypes"/>.
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Indicates whether the index is a unique or non-unique index.
        /// Sparse indexes do not index documents for which any of the index attributes
        /// is either not set or is null.
        /// </summary>
        /// <remarks>
        /// The following index types do not support uniqueness,
        /// and using the unique attribute with these types may lead to an error:
        /// <see cref="IndexTypes.Geo"/>, <see cref="IndexTypes.FullText"/>.
        /// Unique indexes on non-shard keys are not supported in a cluster.
        /// </remarks>
        bool? Unique { get; set; }

        /// <summary>
        /// Supported by indexes of type <see cref="IndexTypes.Persistent"/>.
        /// Indicates whether index selectivity estimates are maintained for the index.
        /// </summary>
        bool? Estimates { get; set; }

        /// <summary>
        /// The index selectivity estimate value for the index if <see cref="Estimates"/> is set to True.
        /// </summary>
        double? SelectivityEstimate { get; set; }

        /// <summary>
        /// Supported by array indexes of type <see cref="IndexTypes.Persistent"/>. 
        /// Indicates whether inserting duplicate index values from the same document
        /// into a unique array index will lead to a unique constraint error or not.
        /// </summary>
        bool? Deduplicate { get; set; }

        /// <summary>
        /// Supported by indexes of type <see cref="IndexTypes.Geo"/>.
        /// If a geo-spatial index on a location is constructed and geoJson is true,
        /// then the order within the array is longitude followed by latitude.
        /// </summary>
        /// <remarks>
        /// This corresponds to the format described in http://geojson.org/geojson-spec.html#positions
        /// </remarks>
        bool? GeoJson { get; set; }

        /// <summary>
        /// Supported by indexes of type <see cref="IndexTypes.TTL"/>.
        /// The time interval (in seconds) from the point in time stored in the <see cref="Fields"/> property
        /// after which the documents count as expired.
        /// Can be set to 0 to let documents expire as soon as the server time
        /// passes the point in time stored in the document attribute,
        /// or to a higher number to delay the expiration.
        /// </summary>
        int? ExpireAfter { get; set; }

        /// <summary>
        /// Supported by indexes of type <see cref="IndexTypes.FullText"/>.
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
    }
}
