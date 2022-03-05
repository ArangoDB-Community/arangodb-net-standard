using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    public class PostExplainAqlQueryResponseIndex
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Fields { get; set; }
        public int? SelectivityEstimate { get; set; }
        public bool? Unique { get; set; }
        public bool? Sparse { get; set; }
        public bool? Deduplicate { get; set; }
        public bool? Estimates { get; set; }    
    }
}
