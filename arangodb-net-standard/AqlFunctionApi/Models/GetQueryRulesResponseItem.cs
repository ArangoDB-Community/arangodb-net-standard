using System.Collections.Generic;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Represents a response item returned by 
    /// <see cref="IAqlFunctionApiClient.GetQueryRulesAsync(System.Threading.CancellationToken)"/>
    /// </summary>
    public class GetQueryRulesResponseItem
    {
        public string Name { get; set; }
        public GetQueryRulesResponseFlags Flags { get; set; }
    }
}