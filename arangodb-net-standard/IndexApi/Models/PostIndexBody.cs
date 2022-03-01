using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.IndexApi.Models
{
    /// <summary>
    /// Represents a request body for creating an index for a collection.
    /// </summary>
    /// <remarks>
    /// Some properties are dependent on the type of index.
    /// For more details, refer to ArangoDB's documentation for the index you want to create.
    /// </remarks>
    public class PostIndexBody
    {
        /// <summary>
        /// The name of the new index. If you do not specify a name, one will be auto-generated.
        /// </summary>
         public string Name { get; set; }

        /// <summary>
        /// The type of index to create. 
        /// The method <see cref="IndexApiClient.PostIndexAsync(IndexType, PostIndexQuery, PostIndexBody)"/>
        /// will set this value, so, you can ignore it.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The attributes to be indexed.
        /// Depending on the index type, a single attribute or multiple attributes can be indexed.
        /// </summary>
        /// <remarks>
        /// For more details, refer to ArangoDB's documentation for the index you want to create.
        /// </remarks>
        public IEnumerable<string> Fields { get; set; }

        /// <summary>
        /// Applies to indexes of type <see cref="IndexType.Persistent"/>.
        /// Can be set to true to create a sparse index.
        /// Sparse indexes do not index documents for which any of the index attributes
        /// is either not set or is null.
        /// </summary>
        public bool? Sparse { get; set; }

        /// <summary>
        /// Set this property to true to create a unique index.
        /// Setting it to false or omitting it will create a non-unique index.
        /// </summary>
        /// <remarks>
        /// The following index types do not support uniqueness,
        /// and using the unique attribute with these types may lead to an error:
        /// <see cref="IndexType.Geo"/>, <see cref="IndexType.FullText"/>.
        /// Unique indexes on non-shard keys are not supported in a cluster.
        /// </remarks>
        public bool? Unique { get; set; }

        /// <summary>
        /// Supported by indexes of type <see cref="IndexType.Persistent"/>. Defaults to true if not set.
        /// This property controls whether index selectivity estimates are maintained for the index.
        /// </summary>
        public bool? Estimates { get; set; }

        /// <summary>
        /// Supported by array indexes of type <see cref="IndexType.Persistent"/>. The default value is true.
        /// It controls whether inserting duplicate index values from the same document
        /// into a unique array index will lead to a unique constraint error or not.
        /// </summary>
        public bool? Deduplicate { get; set; }

        /// <summary>
        /// Supported by indexes of type <see cref="IndexType.Geo"/>.
        /// If a geo-spatial index on a location is constructed and geoJson is true,
        /// then the order within the array is longitude followed by latitude.
        /// </summary>
        /// <remarks>
        /// This corresponds to the format described in http://geojson.org/geojson-spec.html#positions
        /// </remarks>
        public bool? GeoJson { get; set; }

        /// <summary>
        /// Supported by indexes of type <see cref="IndexType.TTL"/>.
        /// The time interval (in seconds) from the point in time stored in the <see cref="Fields"/> property
        /// after which the documents count as expired.
        /// Can be set to 0 to let documents expire as soon as the server time
        /// passes the point in time stored in the document attribute,
        /// or to a higher number to delay the expiration.
        /// </summary>
        public int? ExpireAfter { get; set; }

        /// <summary>
        /// Can be set to true to create the index in the background,
        /// which will not write-lock the underlying collection for
        /// as long as if the index is built in the foreground.
        /// </summary>
        public bool? InBackground { get; set; }

        /// <summary>
        /// Supported by indexes of type <see cref="IndexType.FullText"/>.
        /// Minimum character length of words to index.
        /// Will default to a server-defined value if unspecified.
        /// It is thus recommended to set this value explicitly when creating the index.
        /// </summary>
        public int? MinLength { get; set; }
    }
}
