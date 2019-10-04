using System.Collections.Generic;
using System.Text;

namespace ArangoDB_NET_Standard.DocumentApi
{
    public class PostDocumentsOptions
    {
        public bool? WaitForSync { get; set; }

        public bool? ReturnNew { get; set; }

        public bool? ReturnOld { get; set; }

        public bool? Silent { get; set; }

        public bool? Overwrite { get; set; }

        public string ToQueryString()
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