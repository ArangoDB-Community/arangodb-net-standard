using System;

namespace ArangoDBNetStandard.Transport
{
    public interface IApiClientResponse : IDisposable
    {
        IApiClientResponseContent Content { get; }

        bool IsSuccessStatusCode { get; }

        int StatusCode { get; }
    }
}