using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.DocumentApi.Models
{
    /// <summary>
    /// Represents query parameters used when deleting a single document.
    /// </summary>
    public class DeleteDocumentQuery
    {
        /// <summary>
        /// Wait until deletion operation has been synced to disk.
        /// </summary>
        public bool? WaitForSync { get; set; }

        /// <summary>
        /// Whether to return the complete previous revision of the changed document
        /// under the <see cref="DeleteDocumentResponse{T}.Old"/>.
        /// </summary>
        public bool? ReturnOld { get; set; }

        /// <summary>
        /// If set to true, an empty object will be returned as response.
        /// No meta-data will be returned for the deleted documents.
        /// This option can be used to save some network traffic.
        /// </summary>
        public bool? Silent { get; set; }

        /// <summary>
        /// Whether to delete an existing entry from the in-memory 
        /// edge cache and refill it with another edge if an edge
        /// document is removed.
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
            if (RefillIndexCaches != null)
            {
                queryParams.Add("refillIndexCaches=" + RefillIndexCaches.ToString().ToLower());
            }
            return string.Join("&", queryParams);
        }
    }
}
