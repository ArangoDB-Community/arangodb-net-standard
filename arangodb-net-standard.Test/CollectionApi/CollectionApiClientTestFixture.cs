using ArangoDBNetStandard;

namespace ArangoDBNetStandardTest.CollectionApi
{
    public class CollectionApiClientTestFixture: ApiClientTestFixtureBase
    {
        public ArangoDBClient ArangoDBClient { get; internal set; }

        public CollectionApiClientTestFixture()
        {
            string dbName = nameof(CollectionApiClientTestFixture);
            CreateDatabase(dbName);
            ArangoDBClient = GetArangoDBClient(dbName);
        }
    }
}