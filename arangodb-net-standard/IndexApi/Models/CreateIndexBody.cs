using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.IndexApi.Models
{
    public class CreateIndexBody : IIndex
    {
        public IEnumerable<string> Fields { get ; set ; }
        public string Id { get ; set ; }
        public string Name { get ; set ; }
        public bool? Sparse { get ; set ; }
        public string Type { get ; set ; }
        public bool? Unique { get ; set ; }
        public int? SelectivityEstimate { get ; set ; }
        public bool? Estimates { get ; set ; }
        public bool? Deduplicate { get ; set ; }
        public bool? GeoJson { get ; set ; }
        public int? BestIndexedLevel { get ; set ; }
        public bool? IsNewlyCreated { get ; set ; }
        public int? MaxNumCoverCells { get ; set ; }
        public int? WorstIndexedLevel { get ; set ; }
        public int? ExpireAfter { get ; set ; }
        public bool? InBackground { get; set; }
        public int? MinLength { get; set; }
    }
}
