using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi.Models;
using ArangoDBNetStandard.BulkOperationsApi.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ArangoDBNetStandardTest.BulkOperationsApi
{
    public class BulkOperationsApiClientTestFixture : ApiClientTestFixtureBase
    {
        public ArangoDBClient ArangoDBClient { get; internal set; }
        public string TestCollectionName { get; internal set; } = "OurBulkTestCollection";
        public ImportDocumentArraysBody TestImportDocumentArraysBody { get; internal set; }
        public ImportDocumentObjectsBody<object> TestImportDocumentObjectsBody { get; internal set; }
        public string TestImportDocumentArraysJSON { get; internal set; }
        public string TestImportDocumentObjectsJSON { get; internal set; }
        public int TestImportDocumentArrayJSONCount { get; internal set; }
        public int TestImportDocumentObjectJSONCount { get; internal set; }
        public string TestImportDocumentObjectsType { get; internal set; }

        public BulkOperationsApiClientTestFixture()
        {
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            string dbName = nameof(BulkOperationsApiClientTestFixture);
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
                    var colRes = await ArangoDBClient.Collection.PostCollectionAsync(new PostCollectionBody() { Name = TestCollectionName });
                    if (colRes.Error)
                        throw new Exception("PostCollectionAsync failed: " + colRes.Code.ToString());
                    else
                    {
                        Console.WriteLine("Collection " + TestCollectionName + " created successfully");
                        
                        TestImportDocumentArraysBody = new ImportDocumentArraysBody()
                        {
                            DocumentAttributes = new List<string> { "name", "gender", "age" },
                            ValueArrays = new List<object[]>
                            {
                                new object[] { "James", "M", "21" },
                                new object[] { "Jack", "M", "23" },
                                new object[] { "Jim", "M", "32" },
                            }
                        };
                        TestImportDocumentArraysJSON = "[\"name\",\"gender\",\"age\"]" + Environment.NewLine +
                                                        "[\"Craig\",\"M\",\"23\"]" + Environment.NewLine +
                                                        "[\"Kurt\",\"M\",\"33\"]" + Environment.NewLine +
                                                        "[\"Kevin\",\"M\",\"44\"]";
                        TestImportDocumentArrayJSONCount = 3;


                        TestImportDocumentObjectsBody = new ImportDocumentObjectsBody<object>()
                        {
                             Documents = new List<object>()
                             {
                                 new { name = "Alfredine", gender = "F", age = "43" },
                                 new { name = "Ronia", gender = "F", age = "36" },
                                 new { name = "Jeremy", gender = "M", age = "33" },
                             }
                        };
                        TestImportDocumentObjectsType = "documents";
                        TestImportDocumentObjectsJSON = "{ name:\"Josy\", gender:\"F\", age:\"43\" }" + Environment.NewLine +
                                                        "{ name:\"Marie\", gender:\"F\", age:\"34\" }" + Environment.NewLine +
                                                        "{ name:\"Carinne\", gender:\"F\", age:\"53\" }" + Environment.NewLine;
                        TestImportDocumentObjectJSONCount = 3;

                        Console.WriteLine("Test data created successfully");
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
