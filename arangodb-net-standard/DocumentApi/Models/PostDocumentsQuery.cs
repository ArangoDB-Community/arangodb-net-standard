using System.Collections.Generic;

namespace ArangoDBNetStandard.DocumentApi.Models
{
    /// <summary>
    /// Options used when calling ArangoDB POST document endpoint
    /// to create one or multiple documents.
    /// </summary>
    public class PostDocumentsQuery
    {
        /// <summary>
        /// Wait until document has been synced to disk.
        /// </summary>
        public bool? WaitForSync { get; set; }

        /// <summary>
        /// Whether to return a full copy of the new document.
        /// </summary>
        public bool? ReturnNew { get; set; }

        /// <summary>
        /// Whether to return a full copy of the old document.
        /// </summary>
        public bool? ReturnOld { get; set; }

        /// <summary>
        /// If set to true, an empty object will be returned as response.
        /// No meta-data will be returned for the created document.
        /// This option can be used to save some network traffic.
        /// </summary>
        public bool? Silent { get; set; }

        /// <summary>
        /// If a document already exists, whether to overwrite (replace) 
        /// the document rather than respond with error.
        /// </summary>
        public bool? Overwrite { get; set; }

        /// <summary>
        /// This option supersedes <see cref="Overwrite"/> and offers 
        /// the several modes listed in <see cref="OverwriteModes"/>.
        /// </summary>
        public string OverwriteMode { get; set; }

        /// <summary>
        /// If the intention is to delete existing attributes 
        /// with the update-insert command, the URL query 
        /// parameter keepNull can be used with a value of 
        /// false. This modifies the behavior of the patch 
        /// command to remove any attributes from the 
        /// existing document that are contained in the patch 
        /// document with an attribute value of null. 
        /// This option controls the update-insert behavior only.
        /// </summary>
        public bool? KeepNull { get; set; }

        /// <summary>
        /// Controls whether objects (not arrays) are 
        /// merged if present in both, the existing and
        /// the update-insert document. If set to false,
        /// the value in the patch document overwrites 
        /// the existing document’s value. If set to true, 
        /// objects are merged. The default is true. 
        /// This option controls the update-insert 
        /// behavior only.
        /// </summary>
        public bool? MergeObjects { get; set; }

        /// <summary>
        /// Whether to add a new entry to the in-memory
        /// edge cache if an edge document is inserted.
        /// </summary>
        public bool? RefillIndexCaches { get; set; }

        /// <summary>
        /// Get the set of options in a format suited to a URL query string.
        /// </summary>
        /// <returns></returns>
        internal string ToQueryString()
        {
            List<string> query = new List<string>();
            if (WaitForSync != null)
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
                query.Add("overwriteMode=" + OverwriteMode.ToLower());
            }
            if (KeepNull != null)
            {
                query.Add("keepNull=" + KeepNull.ToString().ToLower());
            }
            if (MergeObjects != null)
            {
                query.Add("mergeObjects=" + MergeObjects.ToString().ToLower());
            }
            if (RefillIndexCaches != null)
            {
                query.Add("refillIndexCaches=" + RefillIndexCaches.ToString().ToLower());
            }
            return string.Join("&", query);
        }
    }
}
