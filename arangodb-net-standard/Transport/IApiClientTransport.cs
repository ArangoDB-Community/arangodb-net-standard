using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.Transport
{
    /// <summary>
    /// A transport layer for communicating with an ArangoDB host.
    /// </summary>
    public interface IApiClientTransport : IDisposable
    {
        /// <summary>
        /// Flags containing specific driver information.
        /// </summary>
        List<string> DriverFlags { get; set; }

        /// <summary>
        /// Send a POST request.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="content"></param>
        /// <param name="webHeaderCollection">Object containing a dictionary of Header keys and values.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<IApiClientResponse> PostAsync(
            string requestUri, 
            byte[] content, 
            WebHeaderCollection webHeaderCollection = null,
            CancellationToken token = default);

        /// <summary>
        /// Send a DELETE request.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="webHeaderCollection">Object containing a dictionary of Header keys and values.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<IApiClientResponse> DeleteAsync(
            string requestUri,
            WebHeaderCollection webHeaderCollection = null,
            CancellationToken token = default);

        /// <summary>
        /// Send a DELETE request with body content.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="content"></param>
        /// <param name="webHeaderCollection">Object containing a dictionary of Header keys and values.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<IApiClientResponse> DeleteAsync(
            string requestUri,
            byte[] content, 
            WebHeaderCollection webHeaderCollection = null,
            CancellationToken token = default);

        /// <summary>
        /// Send a PUT request.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="content"></param>
        /// <param name="webHeaderCollection">Object containing a dictionary of Header keys and values.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<IApiClientResponse> PutAsync(
            string requestUri, 
            byte[] content, 
            WebHeaderCollection webHeaderCollection = null,
            CancellationToken token = default);

        /// <summary>
        /// Send a GET request.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="webHeaderCollection">Object containing a dictionary of Header keys and values.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<IApiClientResponse> GetAsync(
            string requestUri,
            WebHeaderCollection webHeaderCollection = null,
            CancellationToken token = default);

        /// <summary>
        /// Send a PATCH request.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="content"></param>
        /// <param name="webHeaderCollection">Object containing a dictionary of Header keys and values.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<IApiClientResponse> PatchAsync(
            string requestUri,
            byte[] content,
            WebHeaderCollection webHeaderCollection = null,
            CancellationToken token = default);

        /// <summary>
        /// Send a HEAD Request.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="webHeaderCollection">Object containing a dictionary of Header keys and values.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<IApiClientResponse> HeadAsync(
            string requestUri,
            WebHeaderCollection webHeaderCollection = null,
            CancellationToken token = default);
    }
}
