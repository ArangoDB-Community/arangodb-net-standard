namespace ArangoDBNetStandard.QueryApi.Models
{
    /// <summary>
    /// Additional information that reflects original ArangoDB response.
    /// </summary>
    public class ResultExtra
    {
        /// <summary>
        /// Stats related to the response.
        /// </summary>
        public ResponseStats Stats { get; set; }
    }
}
