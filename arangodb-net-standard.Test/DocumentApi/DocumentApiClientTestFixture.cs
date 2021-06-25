using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi.Models;
using System;
using System.Threading.Tasks;

namespace ArangoDBNetStandardTest.DocumentApi
{
    public class DocumentApiClientTestFixture : ApiClientTestFixtureBase
    {
        public ArangoDBClient ArangoDBClient { get; private set; }

        public string TestCollection { get; } = "TestCollection";

        public DocumentApiClientTestFixture()
        {
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync().ConfigureAwait(false);

            string dbName = nameof(DocumentApiClientTest);

            await CreateDatabase(dbName).ConfigureAwait(false);

            ArangoDBClient = GetArangoDBClient(dbName);

            try
            {
                var response = await ArangoDBClient.Collection.PostCollectionAsync(
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
