using ArangoDBNetStandard.DatabaseApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArangoDBNetStandardTest
{
    public class ArangoDBClientTestFixture : ApiClientTestFixtureBase
    {
        public string DatabaseName { get; } = nameof(ArangoDBClientTestFixture);

        public string Username { get; } = Guid.NewGuid().ToString();

        public string Password { get; } = "abcxyz";

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            await CreateDatabase(
                DatabaseName,
                new List<DatabaseUser>
                {
                    new()
                    {
                        Username = Username,
                        Passwd = Password
                    }
                });
        }
    }
}
