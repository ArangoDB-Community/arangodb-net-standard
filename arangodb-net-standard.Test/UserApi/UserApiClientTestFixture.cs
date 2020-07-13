using ArangoDBNetStandard;
using ArangoDBNetStandard.DatabaseApi.Models;
using System;
using System.Threading.Tasks;

namespace ArangoDBNetStandardTest.UserApi
{
    /// <summary>
    /// Provides per-test-class fixture data for <see cref="UserApiClientTest"/>.
    /// </summary>
    public class UserApiClientTestFixture : ApiClientTestFixtureBase
    {
        public ArangoDBClient ArangoClient { get; private set; }

        public string UsernameToDelete { get; private set; }

        public string UsernameToCreate { get; private set; }

        public string UsernameExisting { get; private set; }

        public UserApiClientTestFixture()
        {
            ArangoClient = GetArangoDBClient("_system");
            UsernameToDelete = nameof(UserApiClientTestFixture) + "Delete";
            UsernameToCreate = nameof(UserApiClientTestFixture) + "Post";
            UsernameExisting = nameof(UserApiClientTestFixture) + "Existing";
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            string dbName = nameof(UserApiClientTestFixture);

            await CreateDatabase(dbName, new DatabaseUser[]
            {
                new DatabaseUser()
                {
                    Username = UsernameToDelete
                },
                new DatabaseUser()
                {
                    Username = UsernameExisting
                }
            });

            _users.Add(UsernameToCreate);
        }
    }
}
