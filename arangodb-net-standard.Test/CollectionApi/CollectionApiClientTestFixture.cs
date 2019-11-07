using System.Threading.Tasks;
using ArangoDBNetStandard;

namespace ArangoDBNetStandardTest.CollectionApi
{
    public class CollectionApiClientTestFixture : ApiClientTestFixtureBase
    {
        public ArangoDBClient ArangoDBClient { get; internal set; }

        public CollectionApiClientTestFixture()
        {
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            string dbName = nameof(CollectionApiClientTestFixture);

            await CreateDatabase(dbName);

            ArangoDBClient = GetArangoDBClient(dbName);
        }
    }
}