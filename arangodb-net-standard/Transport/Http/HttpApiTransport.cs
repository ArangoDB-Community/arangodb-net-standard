using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.Transport.Http
{
    public class HttpApiTransport : IApiClientTransport
    {
        private HttpClient _client;

        /// <summary>
        /// Create <see cref="HttpApiTransport"/> from an existing <see cref="HttpClient"/> instance.
        /// </summary>
        /// <param name="hostUri"></param>
        /// <param name="dbName"></param>
        /// <param name="username"></param>
        /// <param name="passwd"></param>
        public HttpApiTransport(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Create <see cref="HttpApiTransport"/> using basic auth.
        /// </summary>
        /// <param name="hostUri"></param>
        /// <param name="dbName"></param>
        /// <param name="username"></param>
        /// <param name="passwd"></param>
        public HttpApiTransport(
            Uri hostUri,
            string dbName,
            string username,
            string passwd)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(hostUri.AbsoluteUri + "/_db/" + dbName + "/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(
                    Encoding.ASCII.GetBytes($"{username}:{passwd}")));

            _client = client;
        }

        public async Task<IApiClientResponse> DeleteAsync(string requestUri)
        {
            var response = await _client.DeleteAsync(requestUri);
            return new HttpApiClientResponse(response);
        }

        public async Task<IApiClientResponse> PostAsync(string requestUri, StringContent content)
        {
            var response = await _client.PostAsync(requestUri, content);
            return new HttpApiClientResponse(response);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
