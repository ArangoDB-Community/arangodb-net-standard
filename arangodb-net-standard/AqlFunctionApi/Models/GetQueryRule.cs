using System.Collections.Generic;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Describes an AQL optimizer rule. 
    /// This is a response item returned by 
    /// <see cref="IAqlFunctionApiClient.GetQueryRulesAsync(System.Threading.CancellationToken)"/>
    /// </summary>
    public class GetQueryRule
    {
        /// <summary>
        /// The name of the optimizer rule as seen in query explain outputs.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// An object with the properties of the rule.
        /// </summary>
        public GetQueryRuleFlags Flags { get; set; }
    }
}