using System.Collections.Generic;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Response from
    /// <see cref="AqlFunctionApiClient.PostParseAqlQueryAsync"/>
    /// </summary>
    public class PostParseAqlQueryResponse : ResponseBase
    {
        /// <summary>
        /// Indicates that the query was successfully parsed.
        /// </summary>
        public bool? Parsed { get; set; }

        /// <summary>
        /// Contains the names of the collections that are involved in the query.
        /// </summary>
        public IList<string> Collections { get; set; }

        /// <summary>
        /// Contains the binding variables involved in the query.
        /// </summary>
        public IList<string> BindVars { get; set; }

        /// <summary>
        /// Tree of data nodes providing information about the query.
        /// </summary>
        public IList<PostParseAqlQueryResponseAstNode> Ast { get; set; }

        /// <summary>
        /// When the query is invalid this will contain the error number.
        /// </summary>
        public int? ErrorNum { get; set; }

        /// <summary>
        /// When the query is invalid this will contain the error message.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}