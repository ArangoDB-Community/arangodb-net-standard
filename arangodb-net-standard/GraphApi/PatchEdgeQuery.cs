using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.GraphApi
{
    public class PatchEdgeQuery
    {
        public bool? WaitForSync { get; set; }

        public bool? KeepNull { get; set; }

        public bool? ReturnOld { get; set; }

        public bool? ReturnNew { get; set; }

        internal string ToQueryString()
        {
            List<string> query = new List<string>();
            if (ReturnNew != null)
            {
                query.Add("returnNew=" + ReturnNew.ToString().ToLower());
            }
            if (ReturnOld != null)
            {
                query.Add("returnOld=" + ReturnOld.ToString().ToLower());
            }
            if (KeepNull != null)
            {
                query.Add("keepNull=" + KeepNull.ToString().ToLower());
            }
            if (WaitForSync != null)
            {
                query.Add("waitForSync=" + WaitForSync.ToString().ToLower());
            }
            return string.Join("&", query);
        }
    }
}
