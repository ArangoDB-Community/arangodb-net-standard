using System.Threading.Tasks;
using ArangoDBNetStandard.VersionApi.Models;

namespace ArangoDBNetStandard.VersionApi
{
    /// <summary>
    /// Defines a client to access the ArangoDB Version API.
    /// </summary>
    public interface IVersionApiClient
    {
        Task<VersionResponse> Version();
    }
}
