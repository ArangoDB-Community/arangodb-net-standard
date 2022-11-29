namespace ArangoDBNetStandard.AdminApi.Models
{
    /// <summary>
    /// Returned by <see cref="IAdminApiClient.GetServerRoleAsync"/>
    /// </summary>
    public class PutLicenseResponse
    {
        /// <summary>
        /// Result of the license update operation.
        /// </summary>
        public ResponseBase Result { get; set; }
    }
}