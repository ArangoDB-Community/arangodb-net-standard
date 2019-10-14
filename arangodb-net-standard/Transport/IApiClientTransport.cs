using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.Transport
{
    public interface IApiClientTransport: IDisposable
    {
        Task<IApiClientResponse> PostAsync(string requestUri, StringContent content);

        Task<IApiClientResponse> DeleteAsync(string requestUri);

        Task<IApiClientResponse> PutAsync(string requestUri, StringContent content);

        Task<IApiClientResponse> GetAsync(string requestUri);

        Task<IApiClientResponse> PatchAsync(string requestUri, StringContent content);
    }
}
