using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.Transport
{
    public interface IApiClientResponseContent
    {
        HttpContentHeaders Headers { get; }

        Task<Stream> ReadAsStreamAsync();

        Task<string> ReadAsStringAsync();
    }
}