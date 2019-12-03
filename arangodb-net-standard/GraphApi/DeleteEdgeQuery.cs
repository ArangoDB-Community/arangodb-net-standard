using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi
{
    public class DeleteEdgeQuery
    {
        public bool? WaitForSync { get; set; }

        public bool? ReturnOld { get; set; }

        public string ToQueryString()
        {
            List<string> query = new List<string>();
            if (ReturnOld != null)
            {
                query.Add("returnOld=" + ReturnOld.ToString().ToLower());
            }
            if (WaitForSync != null)
            {
                query.Add("waitForSync=" + WaitForSync.ToString().ToLower());
            }
            return string.Join("&", query);
        }
    }
}
