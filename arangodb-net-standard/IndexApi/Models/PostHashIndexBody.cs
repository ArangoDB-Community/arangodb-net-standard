namespace ArangoDBNetStandard.IndexApi.Models
{
    /// <summary>
    /// Request body to create a hash index
    /// </summary>
    public class PostHashIndexBody : PostIndexBody
    {
        public PostHashIndexBody()
        {
            Type = IndexTypes.Hash;
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
    }





}
