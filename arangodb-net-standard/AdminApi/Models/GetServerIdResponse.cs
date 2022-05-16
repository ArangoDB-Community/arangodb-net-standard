namespace ArangoDBNetStandard.AdminApi.Models
{
    /// <summary>
    /// Returned by <see cref="IAdminApiClient.GetServerIdAsync"/>
    /// </summary>
    public class GetServerIdResponse : ResponseBase
    {
        /// <summary>
        /// Id of the server in the cluster.
        /// </summary>
        public string Id { get; set; }
    }
}