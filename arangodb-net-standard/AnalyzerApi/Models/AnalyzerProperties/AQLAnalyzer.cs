using System;

namespace ArangoDBNetStandard.AnalyzerApi.Models.AnalyzerProperties
{
    /// <summary>
    /// Properties for AQL analyzer
    /// </summary>
    public class AQLAnalyzer : AnalyzerPropertiesBase
    {
        /// <summary>
        /// Convert emitted tokens to strings. (default)
        /// </summary>
        public static string ReturnTypeString = "string";

        /// <summary>
        /// Convert emitted tokens to numbers
        /// </summary>
        public static string ReturnTypeNumber = "number";

        /// <summary>
        /// Convert emitted tokens to booleans
        /// </summary>
        public static string ReturnTypeBool = "bool";

        /// <summary>
        /// AQL query to be executed
        /// </summary>
        public string QueryString { get; set; }

        /// <summary>
        /// When true: set the position to 0 for all members
        /// of the query result array.
        /// When false: (default): set the position corresponding
        /// to the index of the result array member
        /// </summary>
        public bool CollapsePositions { get; set; }

        /// <summary>
        /// When true: (default): treat null like an empty string.
        /// When false: discard nulls from View index. Can be used
        /// for index filtering (i.e. make your query return null 
        /// for unwanted data). Note that empty results are always
        /// discarded.
        /// </summary>
        public bool KeepNull { get; set; }

        /// <summary>
        /// A number between 1 and 1000 (default = 1) that determines 
        /// the batch size for reading data from the query. In general, 
        /// a single token is expected to be returned. However, if the 
        /// query is expected to return many results, then increasing 
        /// <see cref="BatchSize"/> trades memory for performance.
        /// </summary>
        public int BatchSize { get; set; }

        /// <summary>
        /// Memory limit for query execution in bytes. 
        /// (default is 1048576 = 1Mb) 
        /// Maximum is 33554432U (32Mb)
        /// </summary>
        public int MemoryLimit { get; set; }

        /// <summary>
        /// The Data type of the returned tokens. If the indicated 
        /// type does not match the actual type then an implicit 
        /// type conversion is applied. For possible values, see
        /// <see cref="ReturnTypeBool"/>, <see cref="ReturnTypeNumber"/>
        /// and <see cref="ReturnTypeString"/>
        /// </summary>
        public string ReturnType { get; set; }
    }
}
