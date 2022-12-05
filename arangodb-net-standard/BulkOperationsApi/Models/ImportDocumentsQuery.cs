using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ArangoDBNetStandard.BulkOperationsApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ImportDocumentsQuery
    {
        /// <summary>
        /// Required. Used to specify the target 
        /// collection for the import. Importing 
        /// data into a non-existing collection 
        /// will produce an error.
        /// </summary>
        public string Collection { get; set; }

        /// <summary>
        /// Required for <see cref="BulkOperationsApiClient.PostImportDocumentObjectsAsync"/>
        /// Determines how the body of the request will be interpreted.
        /// Type can have the following values:
        /// 1) documents: When this type is used, each line in 
        /// the request body is expected to be an individual 
        /// JSON-encoded document. Multiple JSON objects in the 
        /// request body need to be separated by newlines.
        /// 2) list: When this type is used, the request body 
        /// must contain a single JSON-encoded array of
        /// individual objects to import.
        /// 3) auto: if set, this will automatically
        /// determine the body type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Optional. Can be set to true to make the import 
        /// only return if all documents have 
        /// been synced to disk.
        /// </summary>
        public bool? WaitForSync { get; set; }

        /// <summary>
        /// Optional. The complete query parameter can be set
        /// to true to make the entire import fail if 
        /// any of the uploaded documents is invalid 
        /// and cannot be imported. In this case, no 
        /// documents will be imported by the import run,
        /// even if a failure happens at the end of the import.
        /// If complete has a value other than true, 
        /// valid documents will be imported while invalid
        /// documents will be rejected, meaning only 
        /// some of the uploaded documents might have 
        /// been imported.
        /// </summary>
        public bool? Complete { get; set; }

        /// <summary>
        /// Optional. The details query parameter can be set 
        /// to true to make the import API return 
        /// details about documents that could not
        /// be imported. If details is true, then 
        /// the result will also contain a details
        /// attribute which is an array of detailed 
        /// error messages. If the details is set 
        /// to false or omitted, no details will 
        /// be returned.
        /// </summary>
        public bool? Details { get; set; }

        /// <summary>
        /// Optional. An optional prefix for the values
        /// in _from attributes. If specified, the value 
        /// is automatically prepended to each _from input
        /// value. This allows specifying just the keys 
        /// for _from.
        /// </summary>
        public string FromPrefix { get; set; }

        /// <summary>
        /// Optional. An optional prefix for the values 
        /// in _to attributes. If specified, the value is
        /// automatically prepended to each _to input value.
        /// This allows specifying just the keys for _to.
        /// </summary>
        public string ToPrefix { get; set; }

        /// <summary>
        /// If this parameter has a value of true or yes, 
        /// then all data in the collection will be removed
        /// prior to the import. Note that any existing 
        /// index definitions will be preserved.
        /// </summary>
        public bool? Overwrite { get; set; }

        /// <summary>
        /// Controls what action is carried out in case 
        /// of a unique key constraint violation.
        /// </summary>
        public ImportDocumentsOnDuplicate? OnDuplicate { get; set; }

        /// <summary>
        /// Generates the actual query string
        /// </summary>
        /// <returns></returns>
        internal string ToQueryString()
        {
            List<string> query = new List<string>();
            if (Collection != null)
            {
                query.Add("collection=" + Collection);
            }
            if (Type != null)
            {
                query.Add("type=" + Type);
            }
            if (WaitForSync != null)
            {
                query.Add("waitForSync=" + WaitForSync.ToString().ToLower());
            }
            if (Complete != null)
            {
                query.Add("complete=" + Complete.ToString().ToLower());
            }
            if (Details != null)
            {
                query.Add("details=" + Details.ToString().ToLower());
            }
            if (FromPrefix != null)
            {
                query.Add("fromPrefix=" + FromPrefix);
            }
            if (ToPrefix != null)
            {
                query.Add("toPrefix=" + ToPrefix);
            }
            if (Overwrite != null)
            {
                query.Add("overwrite=" + Overwrite.ToString().ToLower());
            }
            if (OnDuplicate != null)
            {
                query.Add("onDuplicate=" + Enum.GetName(typeof(ImportDocumentsOnDuplicate), OnDuplicate).ToString().ToLower());
            }
            return string.Join("&", query);
        }
    }
}
