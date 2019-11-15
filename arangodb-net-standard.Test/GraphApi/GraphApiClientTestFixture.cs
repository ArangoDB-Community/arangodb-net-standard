using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi;
using ArangoDBNetStandard.GraphApi;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArangoDBNetStandardTest.GraphApi
{
    public class GraphApiClientTestFixture: ApiClientTestFixtureBase
    {
        public ArangoDBClient ArangoDBClient { get; private set; }

        public string TestCollection { get; } = "TestCollection";

        public string TestGraph { get; } = "TestGraph";

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            string dbName = nameof(GraphApiClientTest);
            await CreateDatabase(dbName);

            ArangoDBClient = GetArangoDBClient(dbName);

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

            // create a graph
            await ArangoDBClient.Graph.PostGraphAsync(new PostGraphBody
            {
                Name = TestGraph,
                EdgeDefinitions = new List<EdgeDefinition>
                {
                    new EdgeDefinition
                    {
                        From = new string[] { "fromclx" },
                        To = new string[] { "toclx" },
                        Collection = "clx"
                    }
                }
            });

        }
    }
}
