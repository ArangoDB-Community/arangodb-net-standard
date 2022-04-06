using System.Collections.Generic;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Response from <see cref="AqlFunctionApiClient.PostExplainAqlQueryAsync(PostExplainAqlQueryBody)"/>
    /// See https://www.arangodb.com/docs/stable/http/aql-query.html#explain-an-aql-query
    /// </summary>
    public class PostExplainAqlQueryResponse:ResponseBase
    {
        /// <summary>
        /// The query execution plan.
        /// </summary>
        public PostExplainAqlQueryResponsePlan Plan { get; set; }

        /// <summary>
        /// All query execution plans. This is returned only when
        /// <see cref="PostExplainAqlQueryBodyOptions.AllPlans"/>
        /// is set to true.
        /// </summary>
        public IList<PostExplainAqlQueryResponsePlan> Plans { get; set; }

        /// <summary>
        /// Indicates whether the query results can be 
        /// cached on the server if the query result
        /// cache were used.
        /// </summary>
        public bool? Cacheable { get; set; }

        /// <summary>
        /// An array of warnings that occurred during 
        /// optimization or execution plan creation.
        /// </summary>
        public IList<string> Warnings { get; set; }

        /// <summary>
        /// Optimizer statistics.
        /// </summary>
        public PostExplainAqlQueryResponseStats Stats { get; set; }
    }
}