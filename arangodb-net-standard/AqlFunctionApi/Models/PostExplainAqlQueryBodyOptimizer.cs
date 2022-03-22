using System.Collections.Generic;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Options related to the query optimizer.
    /// <see cref="PostExplainAqlQueryBodyOptions.Optimizer"/>
    /// </summary>
    public class PostExplainAqlQueryBodyOptimizer
    {
        /// <summary>
        /// A list of to-be-included or to-be-excluded 
        /// optimizer rules can be put into this attribute, 
        /// telling the optimizer to include or exclude 
        /// specific rules. To disable a rule, prefix 
        /// its name with a -, to enable a rule, 
        /// prefix it with a +. There is also a pseudo-rule all, 
        /// which matches all optimizer rules.
        /// -all disables all rules.
        /// </summary>
        public IList<string> Rules { get; set; }
    }
}