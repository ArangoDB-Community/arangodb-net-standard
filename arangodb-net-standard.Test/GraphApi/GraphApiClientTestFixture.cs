using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi.Models;
using ArangoDBNetStandard.GraphApi.Models;
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

        public ArangoDBClient PutEdgeDefinitionAsync_ShouldSucceed_ArangoDBClient { get; private set; }

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

            // A separate database is required as a workaround for a bug discovered
            // in ArangoDB 3.4.6-3.4.8 (and possibly other versions), affecting Windows only.
            // This is reportedly fixed in ArangoDB 3.5.3.
            await CreateDatabase(nameof(GraphApiClientTest.PutEdgeDefinitionAsync_ShouldSucceed));
            PutEdgeDefinitionAsync_ShouldSucceed_ArangoDBClient = GetArangoDBClient(
                nameof(GraphApiClientTest.PutEdgeDefinitionAsync_ShouldSucceed));

            await PutEdgeDefinitionAsync_ShouldSucceed_ArangoDBClient.Graph.PostGraphAsync(new PostGraphBody
            {
                Name = TestGraph,
                EdgeDefinitions = new List<EdgeDefinition>
                {
                    new EdgeDefinition
                    {
                        From = new string[] { "fromclx"},
                        To = new string[] { "toclx" },
                        Collection = nameof(GraphApiClientTest.PutEdgeDefinitionAsync_ShouldSucceed)
                    }
                }
            });
        }
    }
}
