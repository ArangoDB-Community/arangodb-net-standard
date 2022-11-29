using System.Collections.Generic;
using System.Net;

namespace ArangoDBNetStandard.BulkOperationsApi.Models
{
    public class ImportDocumentsResponse
    {
        /// <summary>
        /// The number of documents imported.
        /// </summary>
        public int? Created { get; set; }

        /// <summary>
        /// The number of documents that were 
        /// not imported due to an error.
        /// </summary>
        public int? Errors { get; set; }

        /// <summary>
        /// The number of empty lines found in the input 
        /// (will only contain a value greater zero
        /// for types documents or auto).
        /// </summary>
        public int? Empty { get; set; } 

        /// <summary>
        /// The number of updated/replaced documents
        /// (in case onDuplicate was set to either
        /// update or replace).
        /// </summary>
        public int? Updated { get; set; } 

        /// <summary>
        /// The number of failed but ignored
        /// insert operations 
        /// (in case onDuplicate was set to ignore).
        /// </summary>
        public int? Ignored { get; set; }

        /// <summary>
        /// If query parameter details is set to true,
        /// this attribute is an array with more detailed
        /// information about which documents could not
        /// be inserted.
        /// </summary>
        public IList<string> Details { get; set; }

        /// <summary>
        /// The HTTP status code.
        /// </summary>
        public HttpStatusCode Code { get; set; }

        /// <summary>
        /// Indicates whether an error occurred
        /// </summary>
        /// <remarks>
        /// Note that in cases where an error occurs, the ArangoDBNetStandard
        /// client will throw an <see cref="ApiErrorException"/> rather than
        /// populating this property. A try/catch block should be used instead
        /// for any required error handling.
        /// </remarks>
        public bool Error { get; set; }

        /// <summary>
        /// If Error occured, this is the Error Message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// If Error occured, this is the Error Number
        /// </summary>
        public int? ErrorNum { get; set; }
    }
}
