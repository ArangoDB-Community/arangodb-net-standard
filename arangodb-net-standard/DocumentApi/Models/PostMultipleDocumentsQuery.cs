using System.Collections.Generic;

namespace ArangoDBNetStandard.DocumentApi.Models
{
    /// <summary>
    /// Query options for 
    /// <see cref="IDocumentApiClient.PostMultipleDocumentsAsync(string, PostMultipleDocumentsQuery, IList{object})"/>
    /// </summary>
    public class PostMultipleDocumentsQuery
    {
        /// <summary>
        /// The name of the collection. 
        /// This is only for backward compatibility. 
        /// In ArangoDB versions lower than 3.0, 
        /// the URL path was /_api/document and this 
        /// query parameter was required. 
        /// This combination still works, but the 
        /// recommended way is to specify the 
        /// collection name as a parameter to 
        /// <see cref="IDocumentApiClient.PostMultipleDocumentsAsync(string, PostMultipleDocumentsQuery, IList{object})"/>
        /// </summary>
        public string Collection { get; set; }

        /// <summary>
        /// Wait until document has been synced to disk.
        /// </summary>
        public bool? WaitForSync { get; set; }

        /// <summary>
        ///  Additionally return the complete new document 
        ///  under the attribute new in the result.
        /// </summary>
        public bool? ReturnNew { get; set; }

        /// <summary>
        /// Additionally return the complete old document
        /// under the attribute old in the result. 
        /// Only available if the 
        /// <see cref="PostMultipleDocumentsQuery.Overwrite"/>
        /// option is used.
        /// </summary>
        public bool? ReturnOld { get; set; }

        /// <summary>
        ///  If set to true, an empty object will be returned as response. No meta-data will be returned for the created document. This option can be used to save some network traffic.
        /// </summary>
        public bool? Silent { get; set; }

        /// <summary>
        ///  If set to true, the insert becomes a replace-insert. If a document with the same _key already exists the new document is not rejected with unique constraint violated but will replace the old document. Note that operations with overwrite parameter require a _key attribute in the request payload, therefore they can only be performed on collections sharded by _key.
        /// </summary>
        public bool? Overwrite { get; set; }

        /// <summary>
        ///  This option supersedes
        ///  <see cref="PostMultipleDocumentsQuery.Overwrite"/>
        ///  and offers several overwrite modes.
        /// </summary>
        public PostMultipleDocumentsOverwriteMode? OverwriteMode { get; set; }

        /// <summary>
        /// If the intention is to delete existing attributes
        /// with the update-insert command, this query
        /// parameter can be used with a value of false. 
        /// This will modify the behavior of the patch command
        /// to remove any attributes from the existing document
        /// that are contained in the patch document with an 
        /// attribute value of null. This option controls the
        /// update-insert behavior only.
        /// </summary>
        public bool? KeepNull { get; set; }

        /// <summary>
        ///  Controls whether objects (not arrays)
        ///  will be merged if present in both the 
        ///  existing and the update-insert document. 
        ///  If set to false, the value in the patch 
        ///  document will overwrite the existing 
        ///  document’s value. If set to true, 
        ///  objects will be merged. The default is true. 
        ///  This option controls the update-insert behavior only.
        /// </summary>
        public bool? MergeObjects { get; set; }

        /// <summary>
        /// Get the set of options in a format suited to a URL query string.
        /// </summary>
        /// <returns></returns>
        internal string ToQueryString()
        {
            List<string> query = new List<string>();
            //onlyget is always true
            query.Add("onlyget=true");
            if (!string.IsNullOrWhiteSpace(Collection))
            {
                query.Add("collection=" + Collection.Trim());
            }
            if (WaitForSync!=null)
            {
                query.Add("waitForSync=" + WaitForSync.ToString().ToLower());
            }
            if (ReturnNew != null)
            {
                query.Add("returnNew=" + ReturnNew.ToString().ToLower());
            }
            if (ReturnOld != null)
            {
                query.Add("returnOld=" + ReturnOld.ToString().ToLower());
            }
            if (Silent != null)
            {
                query.Add("silent=" + Silent.ToString().ToLower());
            }
            if (Overwrite != null)
            {
                query.Add("overwrite=" + Overwrite.ToString().ToLower());
            }
            if (OverwriteMode != null)
            {
                query.Add("overwriteMode=" + OverwriteMode.ToString().ToLower());
            }
            if (KeepNull != null)
            {
                query.Add("keepNull=" + KeepNull.ToString().ToLower());
            }
            if (MergeObjects != null)
            {
                query.Add("mergeObjects=" + MergeObjects.ToString().ToLower());
            }
            return string.Join("&", query);
        }
    }
}