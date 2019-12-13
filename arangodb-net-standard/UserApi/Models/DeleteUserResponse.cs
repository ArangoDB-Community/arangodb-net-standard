using System.Net;

namespace ArangoDBNetStandard.UserApi.Models
{
    public class DeleteUserResponse
    {
        public bool Error { get; set; }

        public HttpStatusCode Code { get; set; }
    }
}
