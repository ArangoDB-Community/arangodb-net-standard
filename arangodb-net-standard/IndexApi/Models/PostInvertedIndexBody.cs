using System.Collections.Generic;

namespace ArangoDBNetStandard.IndexApi.Models
{
    public class PostInvertedIndexBody : PostIndexBody
    {
        public PostInvertedIndexBody()
        {
            Type = IndexTypes.Inverted;
        }
        public int? Parallelism { get; set; }
        public IEnumerable<InvertedIndexStoredValue> StoredValues { get; set; }
        public InvertedIndexSort PrimarySort { get; set; }
        public string Analyzer { get; set; }
        public IEnumerable<string> Features { get; set; }
        public bool? IncludeAllFields { get; set; }
        public bool? TrackListPositions { get; set; }
        public bool? SearchField { get; set; }
        public new IEnumerable<InvertedIndexField> Fields { get; set; }
    }
}