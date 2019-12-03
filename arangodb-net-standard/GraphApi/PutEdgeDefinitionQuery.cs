using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi
{
    public class PutEdgeDefinitionQuery
    {
        public bool? WaitForSync { get; set; }

        internal string ToQueryString()
        {
            List<string> query = new List<string>();
            if (WaitForSync != null)
            {
                query.Add("waitForSync=" + WaitForSync.ToString().ToLower());
            }
            return string.Join("&", query);
        }
    }
}
