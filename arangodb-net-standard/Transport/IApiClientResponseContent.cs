using System.IO;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.Transport
{
    public interface IApiClientResponseContent
    {
        Task<Stream> ReadAsStreamAsync();

        Task<string> ReadAsStringAsync();
    }
}