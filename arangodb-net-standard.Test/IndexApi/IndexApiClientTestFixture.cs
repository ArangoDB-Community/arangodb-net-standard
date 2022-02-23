using ArangoDBNetStandard;
using ArangoDBNetStandard.IndexApi.Models;
using System;
using System.Threading.Tasks;

namespace ArangoDBNetStandardTest.IndexApi
{
    public class IndexApiClientTestFixture : ApiClientTestFixtureBase
    {
        public ArangoDBClient ArangoDBClient { get; internal set; }
        public string TestCollectionName { get; } = "TestCollection";
        public string TestIndexName { get; } = "TestIndex";
        public string TestIndexId { get; } = "TestIndexId";

        public IndexApiClientTestFixture()
        {
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            string dbName = nameof(IndexApiClientTestFixture);

            await CreateDatabase(dbName);

            ArangoDBClient = GetArangoDBClient(dbName);
            //try
            //{
            //    await ArangoDBClient.Index.PostIndexAsync(
            //        IndexType.Persistent,
            //        new PostIndexQuery() 
            //        {
            //             CollectionName = TestCollectionName,
            //        }, 
            //        new PostIndexBody
            //        {
            //            Name = TestIndex,                         
            //        });
            //}
            //catch (ApiErrorException ex) when (ex.ApiError.ErrorNum == 1207)
            //{
            //    // The collection must exist already, carry on...
            //    Console.WriteLine(ex.Message);
            //}
        }
    }
}
