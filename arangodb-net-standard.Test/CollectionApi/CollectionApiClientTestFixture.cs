using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi.Models;
using System;
using System.Threading.Tasks;

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
            await base.InitializeAsync().ConfigureAwait(false);

            string dbName = nameof(CollectionApiClientTestFixture);

            await CreateDatabase(dbName).ConfigureAwait(false);

            ArangoDBClient = GetArangoDBClient(dbName);
            try
            {
                await ArangoDBClient.Collection.PostCollectionAsync(
                    new PostCollectionBody
                    {
                        Name = TestCollection
                    }).ConfigureAwait(false);
            }
            catch (ApiErrorException ex) when (ex.ApiError.ErrorNum == 1207)
            {
                // The collection must exist already, carry on...
                Console.WriteLine(ex.Message);
            }
        }
    }
}
