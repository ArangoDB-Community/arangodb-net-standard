using System.Collections.Generic;

namespace ArangoDBNetStandard.ViewsApi.Models
{
    public class ViewStoredValue
    {
        public List<string> Fields { get; set; }
        public string Compression { get; set; }
    }

}
