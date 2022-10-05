namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Describes the properties of an AQL optimizer rule.
    /// </summary>
    public class GetQueryRuleFlags
    {
        /// <summary>
        /// Whether the rule is displayed to users. 
        /// Internal rules are hidden.
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// Whether the rule is applicable in the
        /// cluster deployment mode only.
        /// </summary>
        public bool ClusterOnly { get; set; }

        /// <summary>
        ///  Whether users are allowed to disable 
        ///  this rule. A few rules are mandatory.
        /// </summary>
        public bool CanBeDisabled { get; set; }

        /// <summary>
        /// Whether this rule may create additional
        /// query execution plans.
        /// </summary>
        public bool CanCreateAdditionalPlans { get; set; }

        /// <summary>
        /// Whether the optimizer considers this 
        /// rule by default.
        /// </summary>
        public bool DisabledByDefault { get; set; }

        /// <summary>
        /// Whether the rule is available in the 
        /// Enterprise Edition only.
        /// </summary>
        public bool EnterpriseOnly { get; set; }
    }
}