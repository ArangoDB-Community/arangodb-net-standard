using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi.Models;
using ArangoDBNetStandard.AqlFunctionApi;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ArangoDBNetStandard.AdminApi.Models;

namespace ArangoDBNetStandardTest.AqlFunctionApi
{
    /// <summary>
    /// Provides per-test-class fixture data for <see cref="AqlFunctionApiClientTest"/>.
    /// </summary>
    public class AqlFunctionApiClientTestFixture : ApiClientTestFixtureBase
    {
        public AqlFunctionApiClient AqlFunctionClient { get; set; }
        public string TestCollectionName { get; internal set; } = "Pets";
        public string TestAqlQuery { get; internal set; }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            string dbName = nameof(AqlFunctionApiClientTestFixture);
            await CreateDatabase(dbName);
            var arangoClient = GetArangoDBClient(dbName);
            AqlFunctionClient = arangoClient.AqlFunction;
            await GetVersionAsync(arangoClient);

            try
            {
                //create one collection for our tests...
                var collectionRes = await arangoClient.Collection.PostCollectionAsync(
                    new PostCollectionBody()
                    {
                        Name = TestCollectionName
                    });
                if (collectionRes == null)
                {
                    throw new Exception("PostCollectionAsync failed.");
                }
                else if (collectionRes.Error)
                {
                    throw new Exception("PostCollectionAsync failed. Details: " + collectionRes.Code.ToString());
                }
                else
                {
                    var docRes = await arangoClient.Document.PostDocumentsAsync(
                        TestCollectionName,
                        new List<object>()
                        {
                            new { Name = "Kiko", PetType = "Dog", Breed = "German Shepherd" },
                            new { Name = "Bibi", PetType = "Dog", Breed = "English Cocker Spaniel" },
                            new { Name = "Bruno", PetType = "Dog", Breed = "Rottweiler" }
                        });
                    if (docRes == null || docRes.Count == 0)
                    {
                        throw new Exception($"PostDocumentsAsync failed. Doc count: {docRes?.Count ?? 0}");
                    }
                    else
                    {
                        TestAqlQuery = $@"FOR doc IN {TestCollectionName}
                                            RETURN doc";

                        Console.WriteLine($"TestAqlQuery is: {TestAqlQuery}");
                    }
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