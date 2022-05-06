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
        public string TestCollectionName { get; internal set; } = "OurIndexTestCollection";
        public string TestIndexName { get; internal set; } = "OurIndexTestCollection_FirstIndex";
        public string TestIndexId { get; internal set; }

        public IndexApiClientTestFixture()
        {
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            string dbName = nameof(IndexApiClientTestFixture);
            await CreateDatabase(dbName);
            Console.WriteLine("Database " + dbName + " created successfully");
            ArangoDBClient = GetArangoDBClient(dbName);

            var dbRes = await ArangoDBClient.Database.GetCurrentDatabaseInfoAsync();

            Console.WriteLine("In database " + dbRes.Result.Name);
            var colRes = await ArangoDBClient.Collection.PostCollectionAsync(
                new PostCollectionBody() { Name = TestCollectionName });

            Console.WriteLine("Collection " + TestCollectionName + " created successfully");
            var idxRes = await ArangoDBClient.Index.PostIndexAsync(
                IndexType.Persistent,
                new PostIndexQuery()
                {
                    CollectionName = TestCollectionName,
                },
                new PostIndexBody()
                {
                    Name = TestIndexName,
                    Fields = new string[] { "TestName" },
                    Unique = true
                });

            TestIndexId = idxRes.Id;
            TestIndexName = idxRes.Name;

            Console.WriteLine("DB: " + dbRes.Result.Name);
            Console.WriteLine("Collection: " + TestCollectionName);
            Console.WriteLine("Index: " + string.Format("{0} - {1}", TestIndexId, TestIndexName));
        }
    }
}
