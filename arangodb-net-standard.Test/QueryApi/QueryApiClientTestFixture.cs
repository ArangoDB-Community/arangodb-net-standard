using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi.Models;
using System;
using System.Threading.Tasks;

namespace ArangoDBNetStandardTest.QueryApi
{
    public class QueryApiClientTestFixture : ApiClientTestFixtureBase
    {
        public ArangoDBClient ArangoDBClient { get; private set; }

        public string TestCollection { get; } = "TestCollection";

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            string dbName = nameof(QueryApiClientTest);
            await CreateDatabase(dbName);

            ArangoDBClient = GetArangoDBClient(dbName);
            await GetVersionAsync(ArangoDBClient);

            try
            {
                var response = await ArangoDBClient.Collection.PostCollectionAsync(
                    new PostCollectionBody
                    {
                        Name = TestCollection
                    });
            }
            catch (ApiErrorException ex) when (ex.ApiError.ErrorNum == 1207)
            {
                // The collection must exist already, carry on...
                Console.WriteLine(ex.Message);
            }
        }
    }
}
