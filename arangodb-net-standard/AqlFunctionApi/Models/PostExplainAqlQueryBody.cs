using System.Collections.Generic;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Request body for 
    /// <see cref="AqlFunctionApiClient.PostExplainAqlQueryAsync"/>
    /// </summary>
    public class PostExplainAqlQueryBody
    {
        /// <summary>
        /// The query which you want explained; If the query references 
        /// any bind variables, these must also be passed in the 
        /// <see cref="PostExplainAqlQueryBody.BindVars"/> property. 
        /// Additional options for the query can be passed in the 
        /// <see cref="PostExplainAqlQueryBody.Options"/> property.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Dictionary of Key/value pairs containing 
        /// the bind parameters for the query.
        /// </summary>
        public Dictionary<string,object> BindVars { get; set; }

        /// <summary>
        /// Options for the query
        /// </summary>
        public PostExplainAqlQueryBodyOptions Options { get; set; }
    }
}