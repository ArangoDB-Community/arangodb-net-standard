using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.DocumentApi.Models
{
    /// <summary>
    /// Options used when calling ArangoDB PUT document endpoint
    /// to replace one document.
    /// </summary>
    public class PutDocumentQuery
    {
        /// <summary>
        /// Wait until document has been synced to disk.
        /// </summary>
        public bool? WaitForSync { get; set; }

        /// <summary>
        /// By default, or if this is set to true, the _rev attributes in
        /// the given document is ignored. If this is set to false,
        /// then the _rev attribute given in the body document is taken as a precondition.
        /// The document is only replaced if the current revision is the one specified.
        /// </summary>
        public bool? IgnoreRevs { get; set; }

        /// <summary>
        /// Whether to return the complete previous revision of the changed
        /// document under <see cref="PostDocumentResponse{T}.Old"/>.
        /// </summary>
        public bool? ReturnOld { get; set; }

        /// <summary>
        /// Whether to return the complete new revision of the changed
        /// document under <see cref="PostDocumentResponse{T}.New"/>.
        /// </summary>
        public bool? ReturnNew { get; set; }

        /// <summary>
        /// If set to true, an empty object will be returned as response.
        /// No meta-data will be returned for the replaced document.
        /// This option can be used to save some network traffic.
        /// </summary>
        public bool? Silent { get; set; }

        /// <summary>
        /// Get the set of options in a format suited to a URL query string.
        /// </summary>
        /// <returns></returns>
        internal string ToQueryString()
        {
            var query = new List<string>();
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
            return string.Join("&", query);
        }
    }
}
