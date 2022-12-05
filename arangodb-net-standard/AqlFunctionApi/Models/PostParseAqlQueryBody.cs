namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Request body for 
    /// <see cref="AqlFunctionApiClient.PostParseAqlQueryAsync"/>
    /// </summary>
    public class PostParseAqlQueryBody
    {
        /// <summary>
        /// Query string to parse and validate. This query will not be executed.
        /// </summary>
        public string Query { get; set; }
    }
}