namespace ArangoDBNetStandard.AdminApi.Models
{
    /// <summary>
    /// Returned by <see cref="IAdminApiClient.GetServerRoleAsync"/>
    /// </summary>
    public class GetServerRoleResponse : ResponseBase
    {
        /// <summary>
        /// The server's role.
        /// Possible values for role are:
        /// SINGLE: the server is a standalone server without clustering.
        /// COORDINATOR: the server is a Coordinator in a cluster.
        /// PRIMARY: the server is a DB-Server in a cluster.
        /// SECONDARY: this role is not used anymore.
        /// AGENT: the server is an Agency node in a cluster.
        /// UNDEFINED: the server is in a cluster, but its role cannot be determined.
        /// </summary>
        public string Role { get; set; }
    }
}