namespace ArangoDBNetStandard.AdminApi.Models
{
    /// <summary>
    /// Response from <see cref="IAdminApiClient.GetServerEngineTypeAsync"/>
    /// </summary>
    public class GetServerEngineTypeResponse
    {
        public string Name { get; set; }
        public EngineSupports Supports { get; set; }
    }
}