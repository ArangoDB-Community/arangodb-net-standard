using System.Collections.Generic;

namespace ArangoDBNetStandard.IndexApi.Models
{
    public class InvertedIndexStoredValue
    {
        public IEnumerable<string> Fields { get; set; }
        public string Compression { get; set; }
    }
}