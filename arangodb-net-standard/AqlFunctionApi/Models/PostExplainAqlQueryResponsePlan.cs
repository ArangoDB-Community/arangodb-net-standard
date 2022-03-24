using System.Collections.Generic;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Represents a query execution plan
    /// </summary>
    public class PostExplainAqlQueryResponsePlan
    {
        /// <summary>
        /// The array of execution nodes of the plan.
        /// </summary>
        public IList<PostExplainAqlQueryResponseNode> Nodes { get; set; }

        /// <summary>
        /// An array of rules the optimizer applied.
        /// </summary>
        public IList<string> Rules { get; set; }

        /// <summary>
        /// An array of collections used in the query
        /// </summary>
        public IList<PostExplainAqlQueryResponseCollection> Collections { get; set; }

        /// <summary>
        /// Array of variables used in the query 
        /// (note: this may contain internal 
        /// variables created by the optimizer)
        /// </summary>
        public IList<PostExplainAqlQueryResponseVariable> Variables { get; set; }

        /// <summary>
        /// The total estimated cost for the plan. 
        /// If there are multiple plans, the optimizer
        /// will choose the plan with the lowest total cost.
        /// </summary>
        public int? EstimatedCost { get; set; }

        /// <summary>
        /// See https://www.arangodb.com/docs/stable/http/aql-query.html#explain-an-aql-query
        /// </summary>
        public int? EstimatedNrItems { get; set; }

        /// <summary>
        /// See https://www.arangodb.com/docs/stable/http/aql-query.html#explain-an-aql-query
        /// </summary>
        public bool? IsModificationQuery { get; set; }
    }
}