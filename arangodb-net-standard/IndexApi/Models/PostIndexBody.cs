using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.IndexApi.Models
{
    /// <summary>
    /// Base class for the request body for creating an index for a collection.
    /// </summary>
    public class PostIndexBody
    {
        /// <summary>
        /// The name of the new index. If you do not specify a name, one will be auto-generated.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of index to create. 
        /// Supported index types can be found in <see cref="IndexTypes"/>.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The attributes to be indexed.
        /// Depending on the index type, a single attribute or multiple attributes can be indexed.
        /// </summary>
        public IEnumerable<string> Fields { get; set; }

        /// <summary>
        /// Can be set to true to create the index in the background,
        /// which will not write-lock the underlying collection for
        /// as long as if the index is built in the foreground.
        /// </summary>
        public bool? InBackground { get; set; }
    }
}