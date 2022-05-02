using System.Collections.Generic;
using System.Threading.Tasks;
using ArangoDBNetStandard.AdminApi.Models;

namespace ArangoDBNetStandard.AdminApi
{
    /// <summary>
    /// Defines a client to access the ArangoDB Admin API.
    /// </summary>
    public interface IAdminApiClient
    {
        /// <summary>
        /// Retrieves log messages from the server.
        /// GET /_admin/log/entries
        /// </summary>
        /// <param name="query">Query string parameters</param>
        /// <returns></returns>
        Task<GetLogsResponse> GetLogsAsync(GetLogsQuery query = null);

        /// <summary>
        /// Reloads the routing table.
        /// POST /_admin/routing/reload
        /// </summary>
        /// <returns></returns>
        Task<bool> PostReloadRoutingInfoAsync();

        /// <summary>
        /// Retrieves the internal id of the server.
        /// The method will fail if the server is not running in cluster mode.
        /// GET /_admin/server/id
        /// </summary>
        /// <returns></returns>
        Task<GetServerIdResponse> GetServerIdAsync();

        /// <summary>
        /// Retrieves the role of the server in a cluster.
        /// GET /_admin/server/role
        /// </summary>
        /// <returns></returns>
        Task<GetServerRoleResponse> GetServerRoleAsync();

        /// <summary>
        /// Retrieves the server database engine type.
        /// GET /_api/engine
        /// </summary>
        /// <returns></returns>
        Task<GetServerEngineTypeResponse> GetServerEngineTypeAsync();

        /// <summary>
        /// Retrieves the server version.
        /// GET /_api/version
        /// </summary>
        /// <returns></returns>
        Task<GetServerVersionResponse> GetServerVersionAsync();
    }
}