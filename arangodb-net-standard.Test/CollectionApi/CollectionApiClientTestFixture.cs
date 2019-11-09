using System.Threading.Tasks;
using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi;

namespace ArangoDBNetStandardTest.CollectionApi
{
    public class CollectionApiClientTestFixture : ApiClientTestFixtureBase
    {
        public ArangoDBClient ArangoDBClient { get; internal set; }

        public string TestCollection { get; } = "TestCollection";

        public CollectionApiClientTestFixture()
        {
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            string dbName = nameof(CollectionApiClientTestFixture);

            await CreateDatabase(dbName);

            ArangoDBClient = GetArangoDBClient(dbName);

            await ArangoDBClient.Collection.PostCollectionAsync(
                new PostCollectionRequest
                {
                    Name = TestCollection
                });
        }
    }
}