using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.IndexApi.Models
{
    public class IndexResponseBase : ResponseBase, IIndex
    {

        /// <summary>
        /// Fields
        /// </summary>
        public IEnumerable<string> Fields { get; set; }

        /// <summary>
        /// Id of the index
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the index
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Sparse flag
        /// </summary>
        public bool? Sparse { get; set; }

        /// <summary>
        /// Type of index
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Unique flag
        /// </summary>
        public bool? Unique { get; set; }

        /// <summary>
        /// SelectivityEstimate
        /// </summary>
        public int? SelectivityEstimate { get; set; }

        /// <summary>
        /// Estimates flag
        /// </summary>
        public bool? Estimates { get; set; }

        /// <summary>
        /// Deduplicate flag
        /// </summary>
        public bool? Deduplicate { get; set; }
        public bool? GeoJson { get; set; }
        public int? BestIndexedLevel { get; set; }
        public bool? IsNewlyCreated { get; set; }
        public int? MaxNumCoverCells { get; set; }
        public int? WorstIndexedLevel { get; set; }
        public int? ExpireAfter { get; set; }
        public bool? InBackground { get; set; }
        public int? MinLength { get; set; }
    }
}
