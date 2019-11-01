using ArangoDBNetStandard;

namespace ArangoDBNetStandardTest.CursorApi
{
    public class CursorApiClientTestFixture: ApiClientTestFixtureBase
    {
        public ArangoDBClient ArangoDBClient { get; private set; }

        public CursorApiClientTestFixture()
        {
            string dbName = nameof(CursorApiClientTest);
            CreateDatabase(dbName);
            ArangoDBClient = GetArangoDBClient(dbName);
        }
    }
}
