using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.IndexApi.Models
{
    public interface IIndex
    {
        /// <summary>
        /// Fields
        /// </summary>
        IEnumerable<string> Fields { get; set; }

        /// <summary>
        /// Id of the index
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Name of the index
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Sparse flag
        /// </summary>
        bool? Sparse { get; set; }

        /// <summary>
        /// Type of index
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Unique flag
        /// </summary>
        bool? Unique { get; set; }

        /// <summary>
        /// SelectivityEstimate
        /// </summary>
        int? SelectivityEstimate { get; set; }

        /// <summary>
        /// Estimates flag
        /// </summary>
        bool? Estimates { get; set; }

        /// <summary>
        /// Deduplicate flag
        /// </summary>
        bool? Deduplicate { get; set; }
        bool? GeoJson { get; set; }
        int? BestIndexedLevel { get; set; }
        bool? IsNewlyCreated { get; set; }
        int? MaxNumCoverCells { get; set; }
        int? WorstIndexedLevel { get; set; }
        int? ExpireAfter { get; set; }
        bool? InBackground { get; set; }
        int? MinLength { get; set; }

    }
}
