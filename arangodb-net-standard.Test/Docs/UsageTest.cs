using ArangoDBNetStandard;
using ArangoDBNetStandard.CollectionApi;
using ArangoDBNetStandard.DatabaseApi;
using ArangoDBNetStandard.Transport.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ArangoDBNetStandardTest.Docs
{
    public class UsageTest: IClassFixture<UsageTestFixture>
    {
        private string _arangoDbHost;

        public UsageTest(UsageTestFixture fixture)
        {
            _arangoDbHost = fixture.ArangoDbHost;
        }

        class MyClass
        {
            public long ItemNumber { get; set; }

            public string Description { get; set; }
        }

        class MyClassDocument: MyClass
        {
            public string _key { get; set; }

            public string _id { get; set; }

            public string _rev { get; set; }
        }

        /// <summary>
        /// The function body here is intended for use in "Quick Start" usage documentation.
        /// </summary>
        /// <returns></returns>
        private async Task QuickStartDoc()
        {
            // You must use the system database to create databases!
            using (var systemDbTransport = HttpApiTransport.UsingBasicAuth(
                new Uri($"http://{_arangoDbHost}:8529/"),
                "_system",
                "root",
                "root"))
            {
                var systemDb = new DatabaseApiClient(systemDbTransport);

                // Create a new database with one user.
                await systemDb.PostDatabaseAsync(
                    new PostDatabaseBody
                    {
                        Name = "arangodb-net-standard",
                        Users = new List<DatabaseUser>
                        {
                            new DatabaseUser
                            {
                                Username = "jlennon",
                                Passwd = "yoko123"
                            }
                        }
                    });
            }

            // Use our new database, with basic auth credentials for the user jlennon.
            var transport = HttpApiTransport.UsingBasicAuth(
                new Uri($"http://{_arangoDbHost}:8529"),
                "arangodb-net-standard",
                "jlennon",
                "yoko123");

            var adb = new ArangoDBClient(transport);

            // Create a collection in the database
            await adb.Collection.PostCollectionAsync(
                new PostCollectionBody
                {
                    Name = "MyCollection"
                    // A whole heap of other options exist to define key options, 
                    // sharding options, etc
                });

            // Create document in the collection using anonymous type
            await adb.Document.PostDocumentAsync(
                "MyCollection",
                new
                {
                    MyProperty = "Value"
                });

            // Create document in the collection using strong type
            await adb.Document.PostDocumentAsync(
                "MyCollection",
                new MyClass
                {
                    ItemNumber = 123456,
                    Description = "Some item"
                });

            // Run AQL query (create a query cursor)
            var response = await adb.Cursor.PostCursorAsync<MyClassDocument>(
                @"FOR doc IN MyCollection 
                  FILTER doc.ItemNumber == 123456 
                  RETURN doc");

            MyClassDocument item = response.Result.First();

            // Partially update document
            await adb.Document.PatchDocumentAsync<object, object>(
                "MyCollection",
                item._key,
                new { Description = "More description" });

            // Fully update document
            item.Description = "Some item with some more description";
            await adb.Document.PutDocumentAsync(
                $"MyCollection/{item._key}",
                item);
        }

        [Fact]
        public async Task QuickStart_ShouldSucceed()
        {
            try
            {
                await QuickStartDoc();
            }
            finally
            {
                using (var systemDbTransport = HttpApiTransport.UsingBasicAuth(
                    new Uri($"http://{_arangoDbHost}:8529/"),
                    "_system",
                    "root",
                    "root"))
                {
                    var systemDb = new DatabaseApiClient(systemDbTransport);

                    try
                    {
                        await systemDb.DeleteDatabaseAsync("arangodb-net-standard");
                    }
                    catch (ApiErrorException ex)
                    {
                        // assume database didn't exist
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
