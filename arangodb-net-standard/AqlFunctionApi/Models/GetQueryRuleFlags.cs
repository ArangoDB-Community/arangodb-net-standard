namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    public class GetQueryRuleFlags
    {
        public bool Hidden { get; set; }
        public bool ClusterOnly { get; set; }
        public bool CanBeDisabled { get; set; }
        public bool CanCreateAdditionalPlans { get; set; }
        public bool DisabledByDefault { get; set; }
        public bool EnterpriseOnly { get; set; }
    }
}