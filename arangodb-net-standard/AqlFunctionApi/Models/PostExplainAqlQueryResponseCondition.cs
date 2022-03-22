using System.Collections.Generic;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    public class PostExplainAqlQueryResponseCondition
    {
        public string Type { get; set; }
        public int? TypeId { get; set; }
        public string Name { get; set; }
        public bool? ExcludesNull { get; set; }
        public int? Value { get; set; }
        public string VType { get; set; }
        public int? VTypeId { get; set; }
        public IList<PostExplainAqlQueryResponseCondition> SubNodes { get; set; }
    }
}