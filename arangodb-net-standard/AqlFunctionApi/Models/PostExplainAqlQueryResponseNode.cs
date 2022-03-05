using System.Collections.Generic;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Represents a node in a query execution plan
    /// See https://www.arangodb.com/docs/stable/http/aql-query.html#explain-an-aql-query
    /// </summary>
    public class PostExplainAqlQueryResponseNode
    {
        public string Type { get; set; }
        public IList<int> Dependencies { get; set; }
        public int? Id { get; set; }
        public decimal? EstimatedCost { get; set; }
        public int? EstimatedNrItems { get; set; }
        public bool? Random { get; set; }
        public PostExplainAqlQueryResponseIndexHint IndexHint { get; set; }
        public PostExplainAqlQueryResponseVariable OutVariable { get; set; }
        public IList<string> Projections { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
        public bool? Count { get; set; }
        public bool? ProducesResult { get; set; }
        public bool? ReadOwnWrites { get; set; }
        public bool? Satellite { get; set; }
        public bool? IsSatellite { get; set; }
        public string IsSatelliteOf { get; set; }
        public PostExplainAqlQueryResponseVariable InVariable { get; set; }
        public bool? NeedsGatherNodeSort { get; set; }
        public bool? IndexCoversProjections { get; set; }
        public IList<PostExplainAqlQueryResponseIndex> Indexes { get; set; }
        public PostExplainAqlQueryResponseCondition Condition { get; set; }
        public bool? Sorted { get; set; }
        public bool? Ascending { get; set; }
        public bool? Reverse { get; set; }
        public bool? EvalFCalls { get; set; }
        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public bool? FullCount { get; set; }
    }






}
