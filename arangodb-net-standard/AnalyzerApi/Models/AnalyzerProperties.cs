using System.Collections.Generic;

namespace ArangoDBNetStandard.AnalyzerApi.Models
{
    public class AnalyzerProperties
    {
        public string Locale { get; set; }
        public string Case { get; set; }
        public List<string> StopWords { get; set; }
        public bool Accent { get; set; }
        public bool Stemming { get; set; }
    }

}
