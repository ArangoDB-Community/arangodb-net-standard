using System.Collections.Generic;

namespace ArangoDBNetStandard.AnalyzerApi.Models
{
    public class Analyzer
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public List<AnalyzerProperties> Properties { get; set; }
        public List<string> Features { get; set; }
    }

}
