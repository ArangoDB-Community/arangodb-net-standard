using ArangoDBNetStandard;
using System.Threading.Tasks;

namespace ArangoDBNetStandardTest.CursorApi
{
    public class CursorApiClientTestFixture : ApiClientTestFixtureBase
    {
        public ArangoDBClient ArangoDBClient { get; private set; }

        public CursorApiClientTestFixture()
        {
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            string dbName = nameof(CursorApiClientTest);

            await CreateDatabase(dbName);

            ArangoDBClient = GetArangoDBClient(dbName);
        }
    }
}
