using System.Collections.Generic;

namespace ArangoDBNetStandard.IndexApi.Models
{
    public class InvertedIndexSort
    {
        public IEnumerable<InvertedIndexSortItem> Fields { get; set; }
        public string Compression { get; set; }
    }
}