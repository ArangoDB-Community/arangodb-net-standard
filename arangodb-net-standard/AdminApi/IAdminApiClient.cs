using System.Collections.Generic;
using System.Threading;
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
        /// Works on ArangoDB 3.8 or later.
        /// </summary>
        /// <param name="query">Query string parameters</param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        /// <remarks>
        /// For further information see 
        /// https://www.arangodb.com/docs/stable/http/administration-and-monitoring.html#read-global-logs-from-the-server
        /// </remarks>
        Task<GetLogsResponse> GetLogsAsync(GetLogsQuery query = null, ApiHeaderProperties headers = null, CancellationToken token = default);

        /// <summary>
        /// Reloads the routing table.
        /// POST /_admin/routing/reload
        /// </summary>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        /// <remarks>
        /// For further information see 
        /// https://www.arangodb.com/docs/stable/http/administration-and-monitoring.html#reloads-the-routing-information
        /// </remarks>
        Task<bool> PostReloadRoutingInfoAsync(ApiHeaderProperties headers = null, CancellationToken token = default);

        /// <summary>
        /// Retrieves the internal id of the server.
        /// The method will fail if the server is not running in cluster mode.
        /// GET /_admin/server/id
        /// </summary>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        /// <remarks>
        /// For further information see 
        /// https://www.arangodb.com/docs/stable/http/administration-and-monitoring.html#return-id-of-a-server-in-a-cluster
        /// </remarks>
        Task<GetServerIdResponse> GetServerIdAsync(ApiHeaderProperties headers = null, CancellationToken token = default);

        /// <summary>
        /// Retrieves the role of the server in a cluster.
        /// GET /_admin/server/role
        /// </summary>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        /// <remarks>
        /// For further information see
        /// https://www.arangodb.com/docs/stable/http/administration-and-monitoring.html#return-the-role-of-a-server-in-a-cluster
        /// </remarks>
        Task<GetServerRoleResponse> GetServerRoleAsync(ApiHeaderProperties headers = null, CancellationToken token = default);

        /// <summary>
        /// Retrieves the server database engine type.
        /// GET /_api/engine
        /// </summary>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        /// <remarks>
        /// For further information see 
        /// https://www.arangodb.com/docs/stable/http/miscellaneous-functions.html#return-server-database-engine-type
        /// </remarks>
        Task<GetServerEngineTypeResponse> GetServerEngineTypeAsync(ApiHeaderProperties headers = null, CancellationToken token = default);

        /// <summary>
        /// Retrieves the server version.
        /// GET /_api/version
        /// </summary>
        /// <param name="query">Query string parameters</param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        /// <remarks>
        /// For further information see 
        /// https://www.arangodb.com/docs/stable/http/miscellaneous-functions.html#return-server-version
        /// </remarks>
        Task<GetServerVersionResponse> GetServerVersionAsync(GetServerVersionQuery query = null, ApiHeaderProperties headers = null, CancellationToken token = default);

        /// <summary>
        /// Retrieves the server license information.
        /// GET /_admin/license
        /// </summary>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        /// <remarks>
        /// For further information see 
        /// https://www.arangodb.com/docs/3.9/administration-license.html
        /// </remarks>
        Task<GetLicenseResponse> GetLicenseAsync(ApiHeaderProperties headers = null, CancellationToken token = default);

        /// <summary>
        /// Sets a new license key.
        /// PUT /_admin/license
        /// </summary>
        /// <param name="licenseKey">The new license key</param>
        /// <param name="query">Query string parameters</param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        /// <remarks>
        /// For further information see 
        /// https://www.arangodb.com/docs/3.9/administration-license.html
        /// </remarks>
        Task<PutLicenseResponse> PutLicenseAsync(string licenseKey, PutLicenseQuery query = null, ApiHeaderProperties headers = null, CancellationToken token = default);
    }
}