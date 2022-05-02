namespace ArangoDBNetStandard.AdminApi.Models
{
    /// <summary>
    /// Returned by <see cref="IAdminApiClient.GetServerVersionAsync"/>
    /// </summary>
    public class GetServerVersionResponse
    {
        /// <summary>
        /// The type of server.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// The type of license.
        /// </summary>
        public string License { get; set; }

        /// <summary>
        /// The version of the server.
        /// </summary>
        public string Version { get; set; }
    }
}