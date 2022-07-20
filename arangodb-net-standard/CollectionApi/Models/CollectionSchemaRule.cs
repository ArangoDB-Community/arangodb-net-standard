using System.Collections.Generic;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    /// <summary>
    /// Specifies the JSON Schema description for schema validation.
    /// </summary>
    public class CollectionSchemaRule
    {
        /// <summary>
        /// Properties of the schema rule
        /// </summary>
        public CollectionSchemaRuleProperties Properties { get; set; }

        /// <summary>
        /// Any additional properties of the schema rule
        /// </summary>
        public CollectionSchemaRuleAdditionalProperties AdditionalProperties { get; set; }

        /// <summary>
        /// List of required attributes.
        /// </summary>
        public List<string> Required { get; set; }
    }
}
