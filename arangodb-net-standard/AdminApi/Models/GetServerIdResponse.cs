namespace ArangoDBNetStandard.AdminApi.Models
{
    /// <summary>
    /// Response from <see cref="IAdminApiClient.GetServerIdAsync"/>
    /// </summary>
    public class GetServerIdResponse : ResponseBase
    {
        public string Id { get; set; }
    }
}