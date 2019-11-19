using System.Collections.Generic;

namespace ArangoDBNetStandard.DocumentApi
{
    public class PatchDocumentQuery
    {
        public bool? KeepNull { get; set; }

        public bool? MergeObjects { get; set; }

        public bool? WaitForSync { get; set; }

        public bool? IgnoreRevs { get; set; }

        public bool? ReturnOld { get; set; }

        public bool? ReturnNew { get; set; }

        public bool? Silent { get; set; }

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
                queryParams.Add("keepNull=" + Silent.ToString().ToLower());
            }
            if (MergeObjects != null)
            {
                queryParams.Add("mergeObjects=" + Silent.ToString().ToLower());
            }
            if (ReturnNew != null)
            {
                queryParams.Add("returnNew=" + Silent.ToString().ToLower());
            }
            if (IgnoreRevs != null)
            {
                queryParams.Add("ignoreRevs=" + Silent.ToString().ToLower());
            }
            return string.Join("&", queryParams);
        }
    }
}