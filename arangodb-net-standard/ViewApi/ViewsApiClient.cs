using ArangoDBNetStandard.ViewApi.Models;
using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

namespace ArangoDBNetStandard.ViewApi
{
    /// <summary>
    /// A client to interact with ArangoDB HTTP API endpoints
    /// for views management.
    /// </summary>
    public class ViewApiClient : ApiClientBase, IViewApiClient
    {
        /// <summary>
        /// The transport client used to communicate with the ArangoDB host.
        /// </summary>
        protected readonly IApiClientTransport _transport;

        /// <summary>
        /// The root path of the API.
        /// </summary>
        protected readonly string _apiPath = "_api/view";

        /// <summary>
        /// Create an instance of <see cref="ViewApiClient"/>
        /// using the provided transport layer and the default JSON serialization.
        /// </summary>
        /// <param name="transport"></param>
        public ViewApiClient(IApiClientTransport transport)
            : base(new JsonNetApiClientSerialization())
        {
            _transport = transport;
        }

        /// <summary>
        /// Create an instance of <see cref="ViewApiClient"/>
        /// using the provided transport and serialization layers.
        /// </summary>
        /// <param name="transport"></param>
        /// <param name="serializer"></param>
        public ViewApiClient(IApiClientTransport transport, IApiClientSerialization serializer)
            : base(serializer)
        {
            _transport = transport;
        }

        /// <summary>
        /// Gets a list of all views in a database,
        /// regardless of their type. 
        /// GET /_api/view
        /// </summary>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<GetAllViewsResponse> GetAllViewsAsync(
            CancellationToken token = default)
        {
            string uri = _apiPath;
            using (var response = await _transport.GetAsync(uri,token:token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<GetAllViewsResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Create a new View
        /// POST /_api/view
        /// </summary>
        /// <param name="body">The body of the request containing required properties.</param>
        /// <param name="ignoreNullValuesOnSerialization">
        /// Indicates whether to ignore null values during serialization.
        /// This parameter can be used together with <see cref="LinkProperties.IncludeAllFields"/>
        /// set to false to control if fields with null values are included.
        /// </param>  
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<ViewResponse> PostCreateViewAsync(ViewDetails body, bool ignoreNullValuesOnSerialization=true, CancellationToken token = default)
        {
            string uri = _apiPath;

            var content = await GetContentAsync(body, new ApiClientSerializationOptions(useCamelCasePropertyNames: true,
                                                                                        ignoreNullValues: ignoreNullValuesOnSerialization,
                                                                                        applySerializationOptionsToDictionaryValues: true)).ConfigureAwait(false);

            using (var response = await _transport.PostAsync(uri, content, token: token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<ViewResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Delete / drop a view
        /// DELETE /_api/view/{view-name}
        /// </summary>
        /// <param name="viewNameOrId">The name or identifier of the view to drop.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<DeleteViewResponse> DeleteViewAsync(string viewNameOrId,
            CancellationToken token = default)
        {
            string uri = _apiPath + '/' + viewNameOrId;
            using (var response = await _transport.DeleteAsync(uri, token: token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<DeleteViewResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Get information about a view
        /// GET /_api/view/{view-name}
        /// </summary>
        /// <param name="viewNameOrId">The name or identifier of the view to drop.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<GetViewResponse> GetViewAsync(string viewNameOrId,
            CancellationToken token = default)
        {
            string uri = _apiPath + '/' + viewNameOrId;
            using (var response = await _transport.GetAsync(uri, token: token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<GetViewResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Get the properties of a view
        /// GET /_api/view/{view-name}/properties
        /// </summary>
        /// <param name="viewNameOrId">The name or identifier of the view to drop.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<GetViewPropertiesResponse> GetViewPropertiesAsync(string viewNameOrId,
            CancellationToken token = default)
        {
            string uri = $"{_apiPath}/{viewNameOrId}/properties";
            using (var response = await _transport.GetAsync(uri, token: token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<GetViewPropertiesResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Partially changes properties of a view
        /// PATCH /_api/view/{view-name}/properties
        /// </summary>
        /// <param name="viewNameOrId">The name or identifier of the view.</param>
        /// <param name="body">The body of the request containing required properties.</param>
        /// <param name="ignoreNullValuesOnSerialization">
        /// Indicates whether to ignore null values during serialization.
        /// This parameter can be used together with <see cref="LinkProperties.IncludeAllFields"/>
        /// set to false to control if fields with null values are included.
        /// </param>  
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<ViewResponse> PatchViewPropertiesAsync(string viewNameOrId, ViewDetails body, 
            bool ignoreNullValuesOnSerialization = true, CancellationToken token = default)
        {
            string uri = $"{_apiPath}/{viewNameOrId}/properties";

            var content = await GetContentAsync(body, new ApiClientSerializationOptions(useCamelCasePropertyNames: true,
                                                                                        ignoreNullValues: ignoreNullValuesOnSerialization,
                                                                                        applySerializationOptionsToDictionaryValues: true)).ConfigureAwait(false);
            using (var response = await _transport.PatchAsync(uri, content, token: token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<ViewResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Changes all properties of a view
        /// PUT /_api/view/{view-name}/properties
        /// </summary>
        /// <param name="viewName">The name of the view.</param>
        /// <param name="body">The body of the request containing required properties.</param>
        /// <param name="ignoreNullValuesOnSerialization">
        /// Indicates whether to ignore null values during serialization.
        /// This parameter can be used together with <see cref="LinkProperties.IncludeAllFields"/>
        /// set to false to control if fields with null values are included.
        /// </param>  
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<ViewResponse> PutViewPropertiesAsync(string viewName, ViewDetails body, 
            bool ignoreNullValuesOnSerialization = true, CancellationToken token = default)
        {
            string uri = $"{_apiPath}/{viewName}/properties";

            var content = await GetContentAsync(body, new ApiClientSerializationOptions(useCamelCasePropertyNames: true,
                                                                                        ignoreNullValues: ignoreNullValuesOnSerialization,
                                                                                        applySerializationOptionsToDictionaryValues: true)).ConfigureAwait(false);
            using (var response = await _transport.PutAsync(uri, content, token: token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<ViewResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Renames a view
        /// PUT /_api/view/{view-name}/rename
        /// </summary>
        /// <param name="viewName">The name of the view.</param>
        /// <param name="body">The body of the request containing required properties.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        public virtual async Task<PutRenameViewResponse> PutRenameViewAsync(string viewName, PutRenameViewBody body,
            CancellationToken token = default)
        {
            string uri = $"{_apiPath}/{viewName}/rename";
            var content = await GetContentAsync(body, new ApiClientSerializationOptions(true, true)).ConfigureAwait(false);
            using (var response = await _transport.PutAsync(uri, content, token: token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await DeserializeJsonFromStreamAsync<PutRenameViewResponse>(stream).ConfigureAwait(false);
                }
                throw await GetApiErrorExceptionAsync(response).ConfigureAwait(false);
            }
        }
    }
}