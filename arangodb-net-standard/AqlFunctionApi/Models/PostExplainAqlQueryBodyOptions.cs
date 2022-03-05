namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Options for a query
    /// <see cref="PostExplainAqlQueryBody.Options"/>
    /// </summary>
    public class PostExplainAqlQueryBodyOptions
    {
        /// <summary>
        /// If set to true, all possible execution plans 
        /// will be returned. The default is false, meaning 
        /// only the optimal plan will be returned.
        /// </summary>
        public bool? AllPlans { get; set; }

        /// <summary>
        /// An optional maximum number of plans that the 
        /// optimizer is allowed to generate. Setting this 
        /// attribute to a low value allows to put a cap on
        /// the amount of work the optimizer does.
        /// </summary>
        public int? MaxNumberOfPlans { get; set; }

        /// <summary>
        /// Options related to the query optimizer.
        /// </summary>
        public PostExplainAqlQueryBodyOptimizer Optimizer { get; set; }        
    }

}
