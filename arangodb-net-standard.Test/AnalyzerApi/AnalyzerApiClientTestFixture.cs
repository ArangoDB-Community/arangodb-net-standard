using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi.Models;
using ArangoDBNetStandard.AnalyzerApi.Models;
using System;
using System.Threading.Tasks;

namespace ArangoDBNetStandardTest.IndexApi
{
    public class AnalyzerApiClientTestFixture : ApiClientTestFixtureBase
    {
        public ArangoDBClient ArangoDBClient { get; internal set; }

        public AnalyzerApiClientTestFixture()
        {
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            Console.WriteLine("AnalyzerApiClientTestFixture.InitializeAsync() started.");
            string dbName = nameof(AnalyzerApiClientTestFixture);
            await CreateDatabase(dbName);
            Console.WriteLine("Database " + dbName + " created successfully");
            ArangoDBClient = GetArangoDBClient(dbName);
            try
            {
                var dbRes = await ArangoDBClient.Database.GetCurrentDatabaseInfoAsync();
                if (dbRes.Error)
                    throw new Exception("GetCurrentDatabaseInfoAsync failed: " + dbRes.Code.ToString());
                else
                {
                    Console.WriteLine("In database " + dbRes.Result.Name);
                }
            }
            catch (ApiErrorException ex) 
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }
    }
}
