using ArangoDBNetStandard.IndexApi.Models;
using System.Collections.Generic;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    public class PostExplainAqlQueryResponseIndex
    {
        /// <summary>
        /// Id of the index
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Type of index. See <see cref="IndexTypes"/>.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Name of the index
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The attributes that are part of the indexed.
        /// Depending on the index type, a single attribute or multiple attributes can be indexed.
        /// </summary>
        public IEnumerable<string> Fields { get; set; }

        /// <summary>
        /// The index selectivity estimate value for the index if <see cref="Estimates"/> is set to True.
        /// </summary>
        public double? SelectivityEstimate { get; set; }

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
        public bool? Unique { get; set; }

        /// <summary>
        /// Applies to indexes of type <see cref="IndexTypes.Persistent"/>.
        /// Indicates whether the index is a sparse index or not.
        /// Sparse indexes do not index documents for which any of the index attributes
        /// is either not set or is null.
        /// </summary>
        public bool? Sparse { get; set; }

        /// <summary>
        /// Supported by array indexes of type <see cref="IndexTypes.Persistent"/>. 
        /// Indicates whether inserting duplicate index values from the same document
        /// into a unique array index will lead to a unique constraint error or not.
        /// </summary>
        public bool? Deduplicate { get; set; }

        /// <summary>
        /// Supported by indexes of type <see cref="IndexTypes.Persistent"/>.
        /// Indicates whether index selectivity estimates are maintained for the index.
        /// </summary>
        public bool? Estimates { get; set; }
    }
}