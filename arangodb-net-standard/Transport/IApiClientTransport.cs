using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.Transport
{
    public interface IApiClientTransport: IDisposable
    {
        Task<IApiClientResponse> PostAsync(string collectionApiPath, StringContent content);

        Task<IApiClientResponse> DeleteAsync(string v);
    }
}
