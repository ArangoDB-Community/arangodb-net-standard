namespace ArangoDBNetStandard.AnalyzerApi.Models
{
    /// <summary>
    /// Response from <see cref="IAnalyzerApiClient.DeleteAnalyzerAsync(string)"/>
    /// </summary>
    public class DeleteAnalyzerResponse : ResponseBase
    {
        public string Name { get; set; }
    }
}
