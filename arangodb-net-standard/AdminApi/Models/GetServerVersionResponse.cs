namespace ArangoDBNetStandard.AdminApi.Models
{
    /// <summary>
    /// Response from <see cref="IAdminApiClient.GetServerVersionAsync"/>
    /// </summary>
    public class GetServerVersionResponse
    {
        public string Server { get; set; }
        public string License { get; set; }
        public string Version { get; set; }
    }
}