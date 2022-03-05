using System.Collections.Generic;

namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Data node providing information about a parsed query
    /// See https://www.arangodb.com/docs/stable/http/aql-query.html#parse-an-aql-query
    /// </summary>
    public class PostParseAqlQueryResponseAstNode
    {
        public int? Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public IList<PostParseAqlQueryResponseAstNode> SubNodes { get; set; }
    }




}
