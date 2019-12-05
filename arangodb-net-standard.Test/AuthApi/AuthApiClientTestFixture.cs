using ArangoDBNetStandard;
using ArangoDBNetStandard.DatabaseApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArangoDBNetStandardTest.AuthApi
{
    public class AuthApiClientTestFixture : ApiClientTestFixtureBase
    {
        public ArangoDBClient ArangoDBClient { get; private set; }

        public static string DbName = nameof(AuthApiClientTest);

        public string Username = "abcxyz";
        public string Password = "abczyx";

        public override async Task InitializeAsync()
        {
            await CreateDatabase(DbName, new List<DatabaseUser>
            {
                new DatabaseUser
                {
                    Username = Username,
                    Passwd = Password,
                    Active = true
                }
            });

            ArangoDBClient = GetArangoDBClient(DbName);
        }
    }

}
