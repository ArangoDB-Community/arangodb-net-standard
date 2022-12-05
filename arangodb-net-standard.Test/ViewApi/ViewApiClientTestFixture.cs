using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi.Models;
using ArangoDBNetStandard.ViewApi.Models;
using System;
using System.Threading.Tasks;

namespace ArangoDBNetStandardTest.ViewApi
{
    public class ViewApiClientTestFixture : ApiClientTestFixtureBase
    {
        public ArangoDBClient ArangoDBClient { get; internal set; }

        public ViewApiClientTestFixture()
        {
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            Console.WriteLine("ViewApiClientTestFixture.InitializeAsync() started.");

            string dbName = nameof(ViewApiClientTestFixture);
            await CreateDatabase(dbName);
            Console.WriteLine("Database " + dbName + " created successfully");

            ArangoDBClient = GetArangoDBClient(dbName);
            await GetVersionAsync(ArangoDBClient);
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
