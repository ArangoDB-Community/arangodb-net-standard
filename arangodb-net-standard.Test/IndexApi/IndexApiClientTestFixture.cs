using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi.Models;
using ArangoDBNetStandard.IndexApi.Models;
using System;
using System.Threading.Tasks;

namespace ArangoDBNetStandardTest.IndexApi
{
    public class IndexApiClientTestFixture : ApiClientTestFixtureBase
    {
        public ArangoDBClient ArangoDBClient { get; internal set; }
        public string TestCollectionName { get; internal set; }
        public string TestIndexName { get; internal set; }
        public string TestIndexId { get; internal set; } 

        public IndexApiClientTestFixture()
        {
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            string dbName = nameof(IndexApiClientTestFixture);
            await CreateDatabase(dbName);
            ArangoDBClient = GetArangoDBClient(dbName);
            try
            {
                var colRes = await ArangoDBClient.Collection.PostCollectionAsync(new PostCollectionBody() { Name = TestCollectionName });
                if (colRes.Error)
                    throw new Exception("PostCollectionAsync failed: " + colRes.Code.ToString());
                else
                {
                    var idxRes = await ArangoDBClient.Index.PostIndexAsync(
                        IndexType.Persistent,
                        new PostIndexQuery()
                        {
                            CollectionName = TestCollectionName,
                        },
                        new PostIndexBody()
                        {
                            Fields = new string[] { "TestName" },
                            Unique = true
                        });
                    if (idxRes.Error)
                        throw new Exception("PostIndexAsync failed: " + idxRes.Code.ToString());
                    else
                    {
                        TestIndexId = idxRes.Id;
                        TestCollectionName = idxRes.Name;
                    }
                }
            }
            catch (ApiErrorException ex) 
            {
                // The collection must exist already, carry on...
                Console.WriteLine(ex.Message);
            }

        }
    }
}
