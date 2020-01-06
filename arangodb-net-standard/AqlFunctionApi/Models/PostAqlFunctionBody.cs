namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Represents a request body to create a new AQL user function.
    /// </summary>
    public class PostAqlFunctionBody
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
        /// An optional boolean value to indicate whether the function results
        /// are fully deterministic (function return value solely depends on
        /// the input value and return value is the same for repeated calls with same input).
        /// This attribute is currently not used
        /// but may be used later for optimizations.
        /// </summary>
        public bool? IsDeterministic { get; set; }
    }
}
