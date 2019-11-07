using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi;
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
            await base.InitializeAsync();

            string dbName = nameof(DocumentApiClientTest);

            await CreateDatabase(dbName);

            ArangoDBClient = GetArangoDBClient(dbName);

            try
            {
                var response = await ArangoDBClient.Collection.PostCollectionAsync(
                    new PostCollectionRequest
                    {
                        Name = TestCollection
                    });
            }
            catch (ApiErrorException ex) when (ex.ApiError.ErrorNum == ErrorCode.ARANGO_DUPLICATE_NAME)
            {
                // The collection must exist already, carry on...
                Console.WriteLine(ex.Message);
            }
        }
    }
}
