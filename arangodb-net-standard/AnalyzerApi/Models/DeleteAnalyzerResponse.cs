namespace ArangoDBNetStandard.AnalyzerApi.Models
{
    /// <summary>
    /// Represents a common response class for Analyzer API operations.
    /// </summary>
    public class DeleteAnalyzerResponse : ResponseBase
    {
        public string Name { get; set; }
    }
}
