using System;
using System.Net.Http.Headers;

namespace ArangoDBNetStandard.Transport
{
    public interface IApiClientResponse : IDisposable
    {
        IApiClientResponseContent Content { get; }

        bool IsSuccessStatusCode { get; }

        int StatusCode { get; }

        HttpResponseHeaders Headers { get; }
    }
}
