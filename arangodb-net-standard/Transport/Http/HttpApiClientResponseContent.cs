using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.Transport.Http
{
    public class HttpApiClientResponseContent : IApiClientResponseContent
    {
        private HttpContent content;

        public HttpApiClientResponseContent(HttpContent content)
        {
            this.content = content;
        }

        public HttpContentHeaders Headers => content.Headers;

        public async Task<Stream> ReadAsStreamAsync()
        {
            return await content.ReadAsStreamAsync();
        }

        public async Task<string> ReadAsStringAsync()
        {
            return await content.ReadAsStringAsync();
        }
    }
}
