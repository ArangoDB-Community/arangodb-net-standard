using System.Collections.Generic;

namespace ArangoDBNetStandard.BulkOperationsApi.Models
{
    /// <summary>
    /// Represents a request body to Import documents from an array of objects
    /// </summary>
    public class ImportDocumentObjectsBody
    {
        /// <summary>
        /// List of document objects to import.
        /// </summary>
        public IEnumerable<object> Documents { get; set; }
    }

}
