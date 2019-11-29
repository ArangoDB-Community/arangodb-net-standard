using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.Transport
{
    /// <summary>
    /// A transport layer for communicating with an ArangoDB host.
    /// </summary>
    public interface IApiClientTransport: IDisposable
    {
        /// <summary>
        /// Send a POST request.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<IApiClientResponse> PostAsync(string requestUri, StringContent content);

        /// <summary>
        /// Send a DELETE request.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        Task<IApiClientResponse> DeleteAsync(string requestUri);

        /// <summary>
        /// Send a DELETE request with body content.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<IApiClientResponse> DeleteAsync(string requestUri, StringContent content);

        /// <summary>
        /// Send a PUT request.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<IApiClientResponse> PutAsync(string requestUri, StringContent content);

        /// <summary>
        /// Send a GET request.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        Task<IApiClientResponse> GetAsync(string requestUri);

        /// <summary>
        /// Send a PATCH request.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<IApiClientResponse> PatchAsync(string requestUri, StringContent content);

        /// <summary>
        /// Send a Head Request
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        Task<IApiClientResponse> HeadAsync(string requestUri, WebHeaderCollection httpRequestHeaders);
    }
}
