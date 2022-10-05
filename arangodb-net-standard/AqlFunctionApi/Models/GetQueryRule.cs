using System.Collections.Generic;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Represents a response item returned by 
    /// <see cref="IAqlFunctionApiClient.GetQueryRulesAsync(System.Threading.CancellationToken)"/>
    /// </summary>
    public class GetQueryRule
    {
        public string Name { get; set; }
        public GetQueryRuleFlags Flags { get; set; }
    }
}