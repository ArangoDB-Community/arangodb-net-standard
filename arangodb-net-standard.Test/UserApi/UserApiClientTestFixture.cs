using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi.Models;
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

        public string TestDbName { get; private set; }

        public string UsernameToDelete { get; private set; }

        public string UsernameToCreate { get; private set; }

        public string UsernameExisting { get; private set; }

        public string UsernameToRemoveAccess { get; private set; }

        public string CollectionNameToSetAccess { get; private set; }

        public string CollectionNameToRemoveAccess { get; private set; }

        public UserApiClientTestFixture()
        {
            ArangoClient = GetArangoDBClient("_system");
            TestDbName = nameof(UserApiClientTestFixture);
            UsernameToDelete = nameof(UserApiClientTestFixture) + "Delete";
            UsernameToCreate = nameof(UserApiClientTestFixture) + "Post";
            UsernameExisting = nameof(UserApiClientTestFixture) + "Existing";
            UsernameToRemoveAccess = nameof(UserApiClientTestFixture) + "DeleteAccess";
            CollectionNameToSetAccess = "CollectionToSetAccess";
            CollectionNameToRemoveAccess = "CollectionToRemoveAccess";
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync().ConfigureAwait(false);

            await CreateDatabase(TestDbName, new DatabaseUser[]
            {
                new DatabaseUser()
                {
                    Username = UsernameToDelete
                },
                new DatabaseUser()
                {
                    Username = UsernameExisting
                },
                new DatabaseUser()
                {
                    Username = UsernameToRemoveAccess
                }
            }).ConfigureAwait(false);

            var dbClient = GetArangoDBClient(TestDbName);

            await dbClient.Collection.PostCollectionAsync(new PostCollectionBody()
            {
                Name = CollectionNameToSetAccess
            }).ConfigureAwait(false);

            await dbClient.Collection.PostCollectionAsync(new PostCollectionBody()
            {
                Name = CollectionNameToRemoveAccess
            }).ConfigureAwait(false);

            _users.Add(UsernameToCreate);
        }
    }
}
