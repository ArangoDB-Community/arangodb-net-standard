using ArangoDBNetStandard.UserApi.Models;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.UserApi
{
    /// <summary>
    /// Defines a client to access the ArangoDB User API.
    /// </summary>
    public interface IUserApiClient
    {
        Task<DeleteUserResponse> DeleteUserAsync(string username);
    }
}
