using ArangoDBNetStandard.DatabaseApi;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArangoDBNetStandardTest.Transport.Http
{
    public class HttpApiTransportTestFixture: ApiClientTestFixtureBase
    {
        public string Username { get; } = "xyzabc";
        public string Password { get; } = "abcxyz";

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            await CreateDatabase(
                nameof(HttpApiTransportTest),
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
