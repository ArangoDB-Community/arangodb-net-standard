using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.DocumentApi.Models
{
    /// <summary>
    /// Options used when calling ArangoDB PUT document endpoint
    /// to replace multiple document.
    /// </summary>
    public class PutDocumentsQuery
    {
        /// <summary>
        /// Whether to wait until the new documents have been synced to disk.
        /// </summary>
        public bool? WaitForSync { get; set; }

        /// <summary>
        /// By default, or if this is set to true, the _rev attributes in
        /// the given documents are ignored. If this is set to false, then
        /// any _rev attribute given in a body document is taken as a
        /// precondition. The document is only replaced if the current revision
        /// is the one specified.
        /// </summary>
        public bool? IgnoreRevs { get; set; }

        /// <summary>
        /// Whether to return the complete previous revision of the changed
        /// documents in the result.
        /// </summary>
        public bool? ReturnOld { get; set; }

        /// <summary>
        /// Whether to return the complete new revision of the changed
        /// documents in the result.
        /// </summary>
        public bool? ReturnNew { get; set; }

        /// <summary>
        /// If set to true, an empty object will be returned as response.
        /// No meta-data will be returned for the created document.
        /// This option can be used to save some network traffic.
        /// </summary>
        public bool? Silent { get; set; }

        /// <summary>
        /// Whether to update an existing entry in the in-memory 
        /// edge cache if an edge document is replaced.
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
            if (IgnoreRevs != null)
            {
                query.Add("ignoreRevs=" + IgnoreRevs.ToString().ToLower());
            }
            if (Silent != null)
            {
                query.Add("silent=" + Silent.ToString().ToLower());
            }
            if (RefillIndexCaches != null)
            {
                query.Add("refillIndexCaches=" + RefillIndexCaches.ToString().ToLower());
            }
            return string.Join("&", query);
        }
    }
}