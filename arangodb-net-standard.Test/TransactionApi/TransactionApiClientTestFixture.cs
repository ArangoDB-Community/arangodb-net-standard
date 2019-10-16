using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi;

namespace ArangoDBNetStandardTest.TransactionApi
{
    public class TransactionApiClientTestFixture: ApiClientTestFixtureBase
    {
        public ArangoDBClient ArangoDBClient { get; private set; }

        public string TestCollection1 { get; } = "TestCollection1";

        public string TestCollection2 { get; } = "TestCollection2";

        public TransactionApiClientTestFixture()
        {
            string dbName = nameof(TransactionApiClientTest);
            CreateDatabase(dbName);
            ArangoDBClient = GetArangoDBClient(dbName);
            Task.WaitAll(
                ArangoDBClient.Collection.PostCollectionAsync(new PostCollectionOptions
                {
                    Name = TestCollection1
                }),
                ArangoDBClient.Collection.PostCollectionAsync(new PostCollectionOptions
                {
                    Name = TestCollection2
                }));
        }

    }
}
