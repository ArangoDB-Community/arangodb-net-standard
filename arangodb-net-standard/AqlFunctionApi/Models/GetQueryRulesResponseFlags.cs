namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    public class GetQueryRulesResponseFlags
    {
        public bool Hidden { get; set; }
        public bool ClusterOnly { get; set; }
        public bool CanBeDisabled { get; set; }
        public bool CanCreateAdditionalPlans { get; set; }
        public bool DisabledByDefault { get; set; }
        public bool EnterpriseOnly { get; set; }
    }
}