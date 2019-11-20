using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi
{
    public class DeleteEdgeDefinitionQuery
    {
        public bool? WaitForSync { get; set; }

        public bool? DropCollections { get; set; }

        internal string ToQueryString()
        {
            List<string> query = new List<string>();
            if (WaitForSync != null)
            {
                query.Add("waitForSync=" + WaitForSync.ToString().ToLower());
            }
            if (DropCollections != null)
            {
                query.Add("dropCollections=" + DropCollections.ToString().ToLower());
            }
            return string.Join("&", query);
        }
    }
}
