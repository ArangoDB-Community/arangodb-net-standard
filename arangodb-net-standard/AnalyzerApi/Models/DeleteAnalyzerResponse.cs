namespace ArangoDBNetStandard.AnalyzerApi.Models
{
    /// <summary>
    /// Response from <see cref="IAnalyzerApiClient.DeleteAnalyzerAsync"/>
    /// </summary>
    public class DeleteAnalyzerResponse : ResponseBase
    {
        public string Name { get; set; }
    }
}
