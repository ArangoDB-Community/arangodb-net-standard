using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi.Models;
using System.Threading.Tasks;

namespace ArangoDBNetStandardTest.TransactionApi
{
    public class TransactionApiClientTestFixture : ApiClientTestFixtureBase
    {
        public ArangoDBClient ArangoDBClient { get; private set; }

        public string TestCollection1 { get; } = "TestCollection1";

        public string TestCollection2 { get; } = "TestCollection2";

        public TransactionApiClientTestFixture()
        {
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            string dbName = nameof(TransactionApiClientTest);

            await CreateDatabase(dbName);

            ArangoDBClient = GetArangoDBClient(dbName);

            await Task.WhenAll(
                ArangoDBClient.Collection.PostCollectionAsync(new PostCollectionBody
                {
                    Name = TestCollection1
                }),
                ArangoDBClient.Collection.PostCollectionAsync(new PostCollectionBody
                {
                    Name = TestCollection2
                }));
        }

    }
}
