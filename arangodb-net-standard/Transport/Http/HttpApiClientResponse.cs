using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ArangoDBNetStandard.Transport.Http
{
    internal class HttpApiClientResponse : IApiClientResponse
    {
        private readonly HttpResponseMessage response;

        internal HttpApiClientResponse(HttpResponseMessage response)
        {
            this.response = response;
            Headers = response.Headers;
            Content = new HttpApiClientResponseContent(response.Content);
            IsSuccessStatusCode = response.IsSuccessStatusCode;
            StatusCode = (int)response.StatusCode;

        }

        public IApiClientResponseContent Content { get; private set; }

        public bool IsSuccessStatusCode { get; private set; }

        public int StatusCode { get; private set; }

        public HttpResponseHeaders Headers { get; set; }

        public void Dispose()
        {
            response.Dispose();
        }
    }
}
