namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Parameter to
    /// <see cref="AqlFunctionApiClient.PutChangeQueryTrackingConfigurationAsync(PutChangeQueryTrackingConfigurationBody)"/>
    /// </summary>
    public class PutChangeQueryTrackingConfigurationBody
    {
        /// <summary>
        /// Configuration properties.
        /// </summary>
        public QueryTrackingConfiguration Properties { get; set; }
    }
}