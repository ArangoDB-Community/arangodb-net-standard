using System.Collections;
using System.Collections.Generic;

namespace ArangoDBNetStandard.BulkOperationsApi.Models
{
    /// <summary>
    /// Represents a request body to Import documents as JSON-encoded lists
    /// </summary>
    public class ImportDocumentArraysBody
    {
        /// <summary>
        /// List containing document attributes that are
        /// being imported.
        /// </summary>
        public IEnumerable<string> DocumentAttributes { get; set; }

        /// <summary>
        /// List containing value arrays to be imported.
        /// Each array is a document. Attribute values will 
        /// be mapped to the attribute names by positions
        /// defined in <see cref="DocumentAttributes"/> .
        /// </summary>
        public IEnumerable<IEnumerable<object>> ValueArrays { get; set; }
    }

}
