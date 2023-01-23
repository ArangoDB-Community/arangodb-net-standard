using System.Collections.Generic;

namespace ArangoDBNetStandard.DocumentApi.Models
{
    /// <summary>
    /// Represents query parameters used when updating a document.
    /// </summary>
    public class PatchDocumentQuery
    {
        /// <summary>
        /// If the intention is to delete existing attributes with the patch command,
        /// this query parameter can be used with a value of false.
        /// This will modify the behavior of the patch command to remove any attributes
        /// from the existing document that are contained in the patch document with an attribute value of null.
        /// </summary>
        public bool? KeepNull { get; set; }

        /// <summary>
        /// Controls whether objects (not arrays) will be merged
        /// if present in both the existing and the patch document.
        /// If set to false, the value in the patch document will
        /// overwrite the existing document’s value.
        /// If set to true, objects will be merged.
        /// The default is true.
        /// </summary>
        public bool? MergeObjects { get; set; }

        /// <summary>
        /// Wait until the new documents have been synced to disk.
        /// </summary>
        public bool? WaitForSync { get; set; }

        /// <summary>
        /// By default, or if this is set to true, the _rev attributes
        /// in the given documents are ignored.
        /// If this is set to false, then any _rev attribute given
        /// in a body document is taken as a precondition.
        /// The document is only updated if the current revision is the one specified.
        /// </summary>
        public bool? IgnoreRevs { get; set; }

        /// <summary>
        /// Return additionally the complete previous revision
        /// of the changed documents in the result.
        /// </summary>
        public bool? ReturnOld { get; set; }

        /// <summary>
        /// Return additionally the complete new documents in the result.
        /// </summary>
        public bool? ReturnNew { get; set; }

        /// <summary>
        /// If set to true, an empty object will be returned as response.
        /// No meta-data will be returned for the created document.
        /// This option can be used to save some network traffic.
        /// </summary>
        public bool? Silent { get; set; }

        /// <summary>
        /// Whether to update an existing entry in the in-memory edge 
        /// cache if an edge document is updated.
        /// </summary>
        public bool? RefillIndexCaches { get; set; }

        internal string ToQueryString()
        {
            var queryParams = new List<string>();
            if (WaitForSync != null)
            {
                queryParams.Add("waitForSync=" + WaitForSync.ToString().ToLower());
            }
            if (ReturnOld != null)
            {
                queryParams.Add("returnOld=" + ReturnOld.ToString().ToLower());
            }
            if (Silent != null)
            {
                queryParams.Add("silent=" + Silent.ToString().ToLower());
            }
            if (KeepNull != null)
            {
                queryParams.Add("keepNull=" + KeepNull.ToString().ToLower());
            }
            if (MergeObjects != null)
            {
                queryParams.Add("mergeObjects=" + MergeObjects.ToString().ToLower());
            }
            if (ReturnNew != null)
            {
                queryParams.Add("returnNew=" + ReturnNew.ToString().ToLower());
            }
            if (IgnoreRevs != null)
            {
                queryParams.Add("ignoreRevs=" + IgnoreRevs.ToString().ToLower());
            }
            if (RefillIndexCaches != null)
            {
                queryParams.Add("refillIndexCaches=" + RefillIndexCaches.ToString().ToLower());
            }
            return string.Join("&", queryParams);
        }
    }
}
