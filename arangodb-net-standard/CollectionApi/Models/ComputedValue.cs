using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    /// <summary>
    /// Represents a computed value in a collection.
    /// </summary>
    public class ComputedValue
    {
        /// <summary>
        /// Required. The name of the target attribute. 
        /// Can only be a top-level attribute, but you 
        /// may return a nested object. 
        /// Cannot be _key, _id, _rev, _from, _to, or 
        /// a shard key attribute.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Required. An AQL RETURN operation with an
        /// expression that computes the desired value. 
        /// <see cref="https://www.arangodb.com/docs/devel/data-modeling-documents-computed-values.html#computed-value-expressions"/>
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// Required. Indicates whether the computed value 
        /// shall take precedence over a user-provided 
        /// or existing attribute.
        /// </summary>
        public bool Overwrite { get; set; }

        /// <summary>
        /// Optional. An list of strings to define on which write
        /// operations the value shall be computed. 
        /// The possible values are "insert", "update",
        /// and "replace". 
        /// The default is ["insert", "update", "replace"]
        /// </summary>
        public List<string> ComputedOn { get; set; } = new List<string>() { "insert", "update", "replace" };

        /// <summary>
        /// Optional. Indicates whether the target attribute shall 
        /// be set if the expression evaluates to null. 
        /// You can set the option to false to not set
        /// (or unset) the target attribute if the 
        /// expression returns null. 
        /// The default is True.
        /// </summary>
        public bool KeepNull { get; set; } = true;

        /// <summary>
        /// Optional. Indicates whether to let the write operation 
        /// fail if the expression produces a warning.
        /// The default is False.
        /// </summary>
        public bool FailOnWarning { get; set; } = false;
    }
}