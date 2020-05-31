using System.Collections.Generic;

namespace ArangoDBNetStandard.DocumentApi.Models
{
    /// <summary>
    /// Options used when calling ArangoDB POST document endpoint
    /// to create one or multiple documents.
    /// </summary>
    public class PostDocumentsQuery
    {
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
        /// TODO
        /// </summary>
        public bool? Silent { get; set; }

        /// <summary>
        /// If a document already exists, whether to overwrite (replace) the document rather than respond with error.
        /// </summary>
        public bool? Overwrite { get; set; }

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
            return string.Join("&", query);
        }
    }
}