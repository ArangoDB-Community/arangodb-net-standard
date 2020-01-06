using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Represents an AQL user function returned in a response results.
    /// </summary>
    public class AqlFunctionResult
    {
        /// <summary>
        /// The fully qualified name of the user function.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A string representation of the function body.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Whether the function results are fully deterministic
        /// (function return value solely depends on the input value
        /// and return value is the same for repeated calls with same input).
        /// </summary>
        public bool IsDeterministic { get; set; }
    }
}
