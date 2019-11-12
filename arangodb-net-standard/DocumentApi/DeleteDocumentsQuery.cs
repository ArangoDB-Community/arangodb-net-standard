using System;
using System.Collections.Generic;

namespace ArangoDBNetStandard.DocumentApi
{
    public class DeleteDocumentsQuery
    {
        public bool? WaitForSync { get; set; }

        public bool? ReturnOld { get; set; }

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
            string queryString = string.Join("&", queryParams);
            return queryString;
        }
    }
}