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
            return string.Join("&", queryParams);
        }
    }
}