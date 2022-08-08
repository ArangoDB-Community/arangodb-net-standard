using ArangoDBNetStandard.ViewApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.ViewApi
{
    /// <summary>
    /// Defines a client to access the ArangoDB API for Views.
    /// </summary>
    public interface IViewApiClient
    {
        /// <summary>
        /// Gets a list of all views in a database,
        /// regardless of their type. 
        /// GET /_api/view
        /// </summary>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<GetAllViewsResponse> GetAllViewsAsync(
            CancellationToken token = default);

        /// <summary>
        /// Create a new View
        /// POST /_api/view
        /// </summary>
        /// <param name="body">The body of the request containing required properties.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<ViewResponse> PostCreateViewAsync(ViewDetails body,
            CancellationToken token = default);

        /// <summary>
        /// Delete / drop a view
        /// DELETE /_api/view/{view-name}
        /// </summary>
        /// <param name="viewNameOrId">The name or identifier of the view to drop.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<DeleteViewResponse> DeleteViewAsync(string viewNameOrId,
            CancellationToken token = default);

        /// <summary>
        /// Get information about a view
        /// GET /_api/view/{view-name}
        /// </summary>
        /// <param name="viewNameOrId">The name or identifier of the view to drop.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<GetViewResponse> GetViewAsync(string viewNameOrId,
            CancellationToken token = default);

        /// <summary>
        /// Get the properties of a view
        /// GET /_api/view/{view-name}/properties
        /// </summary>
        /// <param name="viewNameOrId">The name or identifier of the view to drop.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<GetViewPropertiesResponse> GetViewPropertiesAsync(string viewNameOrId,
            CancellationToken token = default);

        /// <summary>
        /// Partially changes properties of a view
        /// PATCH /_api/view/{view-name}/properties
        /// </summary>
        /// <param name="viewNameOrId">The name or identifier of the view.</param>
        /// <param name="body">The body of the request containing required properties.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<ViewResponse> PatchViewPropertiesAsync(string viewNameOrId, ViewDetails body,
            CancellationToken token = default);

        /// <summary>
        /// Changes all properties of a view
        /// PUT /_api/view/{view-name}/properties
        /// </summary>
        /// <param name="viewName">The name of the view.</param>
        /// <param name="body">The body of the request containing required properties.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<ViewResponse> PutViewPropertiesAsync(string viewName, ViewDetails body,
            CancellationToken token = default);

        /// <summary>
        /// Renames a view
        /// PUT /_api/view/{view-name}/rename
        /// </summary>
        /// <param name="viewName">The name of the view.</param>
        /// <param name="body">The body of the request containing required properties.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        /// <remarks>Note: This method is not available in a cluster.</remarks>
        Task<PutRenameViewResponse> PutRenameViewAsync(string viewName, PutRenameViewBody body,
            CancellationToken token = default);
    }
}