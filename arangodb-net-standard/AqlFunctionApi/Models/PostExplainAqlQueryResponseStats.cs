namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Statistics for Explain Query
    /// </summary>
    public class PostExplainAqlQueryResponseStats
    {
        /// <summary>
        /// Number of rules that were executed.
        /// </summary>
        public int? RulesExecuted { get; set; }

        /// <summary>
        /// Number of rules that were skipped.
        /// </summary>
        public int? RulesSkipped { get; set; }

        /// <summary>
        /// Number of plans that were created.
        /// </summary>
        public int? PlansCreated { get; set; }
    }
}