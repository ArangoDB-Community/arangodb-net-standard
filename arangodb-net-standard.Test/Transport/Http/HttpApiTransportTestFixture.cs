using ArangoDBNetStandard.DatabaseApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArangoDBNetStandardTest.Transport.Http
{
    public class HttpApiTransportTestFixture : ApiClientTestFixtureBase
    {
        public string DatabaseName { get; } = nameof(HttpApiTransportTest);

        public string Username { get; } = "xyzabc";
        public string Password { get; } = "abcxyz";

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            await CreateDatabase(
                DatabaseName,
                new List<DatabaseUser>
                {
                    new DatabaseUser
                    {
                        Username = Username,
                        Passwd = Password
                    }
                });
        }
    }
}
