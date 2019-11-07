using System;
using System.Collections.Generic;
using System.Text;
using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi;

namespace ArangoDBNetStandardTest.DocumentApi
{
    public class DocumentApiClientTestFixture: ApiClientTestFixtureBase
    {
        public ArangoDBClient ArangoDBClient { get; private set; }

        public string TestCollection { get; } = "TestCollection";

        public DocumentApiClientTestFixture()
        {
            string dbName = nameof(DocumentApiClientTest);
            CreateDatabase(dbName);

            ArangoDBClient = GetArangoDBClient(dbName);

            try
            {
                var response = ArangoDBClient.Collection.PostCollectionAsync(
                    new PostCollectionRequest
                    {
                        Name = TestCollection
                    })
                    .GetAwaiter()
                    .GetResult();
            }
            catch (ApiErrorException ex) when (ex.ApiError.ErrorNum == ErrorCode.ARANGO_DUPLICATE_NAME)
            {
                // The collection must exist already, carry on...
                Console.WriteLine(ex.Message);
            }
        }
    }
}
