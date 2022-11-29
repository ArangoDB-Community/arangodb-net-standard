using System.Collections.Generic;

namespace ArangoDBNetStandard.IndexApi.Models
{
    public class InvertedIndexField
    {
        public string Name { get; set; }
        public string Analyzer { get; set; }
        public bool? IncludeAllFields { get; set; }
        public bool? SearchField { get; set; }
        public bool? TrackListPositions { get; set; }
        public IEnumerable<string> Features { get; set; }
        public IEnumerable<InvertedIndexField> Nested { get; set; }
    }
}