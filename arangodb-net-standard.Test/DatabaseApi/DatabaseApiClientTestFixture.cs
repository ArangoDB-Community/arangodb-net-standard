using System.Threading.Tasks;
using ArangoDBNetStandard;
using ArangoDBNetStandard.DatabaseApi;

namespace ArangoDBNetStandardTest.DatabaseApi
{
    /// <summary>
    /// Provides per-test-class fixture data for <see cref="DatabaseApiClientTest"/>.
    /// </summary>
    public class DatabaseApiClientTestFixture : ApiClientTestFixtureBase
    {
        /// <summary>
        /// A <see cref="DatabaseApiClient"/> targeting the _system database.
        /// </summary>
        public DatabaseApiClient DatabaseClientSystem { get; internal set; }

        /// <summary>
        /// A <see cref="DatabaseApiClient"/> targeting a database that does not exist.
        /// </summary>
        public DatabaseApiClient DatabaseClientNonExistent { get; internal set; }

        /// <summary>
        /// A <see cref="DatabaseApiClient"/> targeting an existing database other than _system.
        /// </summary>
        public DatabaseApiClient DatabaseClientOther { get; internal set; }

        public string DeletableDatabase { get; } = nameof(DatabaseApiClientTestFixture) + "_ToBeDeleted";

        public DatabaseApiClientTestFixture()
        {
            DatabaseClientSystem = GetArangoDBClient("_system").Database;
            DatabaseClientNonExistent = GetArangoDBClient("DatabaseThatDoesNotExist").Database;
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            string dbName = nameof(DatabaseApiClientTestFixture);

            await CreateDatabase(dbName);

            await CreateDatabase(DeletableDatabase);

            DatabaseClientOther = GetArangoDBClient(dbName).Database;
        }
    }
}
