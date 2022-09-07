using ArangoDBNetStandard.IndexApi.Models;
using System.Collections.Generic;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Represents an index when returned by <see cref="AqlFunctionApiClient.PostExplainAqlQueryAsync"/>.
    /// Some attributes will depend on the type of index the object represents.
    /// </summary>
    /// <inheritdoc/>
    public class PostExplainAqlQueryResponseIndex : IIndexResponse
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Fields { get; set; }

        public double? SelectivityEstimate { get; set; }

        public bool? Unique { get; set; }

        public bool? Sparse { get; set; }

        public bool? Deduplicate { get; set; }

        public bool? Estimates { get; set; }

        public bool? GeoJson { get; set; }

        public int? ExpireAfter { get; set; }

        public int? MinLength { get; set; }

        public bool? IsNewlyCreated { get; set; }

        public int? MaxNumCoverCells { get; set; }

        public int? BestIndexedLevel { get; set; }

        public int? WorstIndexedLevel { get; set; }

        public List<string> StoredValues { get; set; }
        public bool? CacheEnabled { get; set; }
    }
}