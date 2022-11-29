using System.Collections.Generic;

namespace ArangoDBNetStandard.IndexApi.Models
{
    /// <summary>
    /// Request body to create a persistent index
    /// </summary>
    public class PostPersistentIndexBody : PostIndexBody
    {
        public PostPersistentIndexBody() 
        {
            Type = IndexTypes.Persistent;
        }

        /// <summary>
        /// Set this property to true to create a unique index.
        /// Setting it to false or omitting it will create a non-unique index.
        /// </summary>
        public bool? Unique { get; set; }

        /// <summary>
        /// Can be set to true to create a sparse index.
        /// Sparse indexes do not index documents for
        /// which any of the index attributes
        /// is either not set or is null.
        /// </summary>
        public bool? Sparse { get; set; }

        /// <summary>
        /// The default value is true.
        /// It controls whether inserting duplicate index 
        /// values from the same document into a unique 
        /// array index will lead to a unique constraint
        /// error or not.
        /// </summary>
        public bool? Deduplicate { get; set; }

        /// <summary>
        /// Defaults to true if not set.
        /// This property controls whether index selectivity
        /// estimates are maintained for the index.
        /// </summary>
        public bool? Estimates { get; set; }

        /// <summary>
        /// Introduced in v3.10.
        /// An array of additional index attribute paths in a persistent index.
        /// These additional attributes cannot be used for index lookups or
        /// sorts, but they can be used for projections.
        /// </summary>
        /// <remarks>
        /// There must be no overlap of attribute paths between 
        /// <see cref="PostIndexBody.Fields"/> and <see cref="StoredValues"/>. 
        /// The maximum number of values is 32.
        /// </remarks>
        public IEnumerable<string> StoredValues { get; set; }

        /// <summary>
        /// Introduced in v3.10
        /// Can be set to true to enable an in-memory cache for 
        /// index values for persistent indexes. Otherwise the
        /// index is created without it. Caching is turned off
        /// by default.
        /// </summary>
        public bool? CacheEnabled { get; set; }
    }





}
