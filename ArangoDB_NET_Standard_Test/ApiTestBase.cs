using ArangoDB_NET_Standard;
using ArangoDB_NET_Standard.DatabaseApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ArangoDB_NET_Standard_Test
{
    public abstract class ApiTestBase: IDisposable
    {
        private List<string> _databases = new List<string>();

        private List<HttpClient> _clients = new List<HttpClient>(); 

        protected HttpClient GetHttpClient(string dbName)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8529/_db/" + dbName + "/");
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes("root:root")));
            _clients.Add(client);
            return client;
        }

        protected ArangoDBClient GetArangoDBClient(string dbName)
        {
            var httpClient = GetHttpClient(dbName);
            return new ArangoDBClient(httpClient);
        }

        protected void DropDatabase(string dbName)
        {
            using (var systemDbClient = GetHttpClient("_system"))
            {
                var dbApiClient = new DatabaseApiClient(systemDbClient);
                var response = dbApiClient.DeleteDatabaseAsync(dbName)
                    .GetAwaiter().GetResult();
            }
        }

        protected void CreateDatabase(string dbName)
        {
            // Create the test database
            using (var systemDbClient = GetHttpClient("_system"))
            {
                var dbApiClient = new DatabaseApiClient(systemDbClient);
                try
                {
                    var postDatabaseResponse = dbApiClient.PostDatabaseAsync(new PostDatabaseRequest
                    {
                        Name = dbName
                    }).GetAwaiter().GetResult();
                }
                catch (ApiErrorException ex) when (ex.ApiError.ErrorNum == 1207)
                {
                    // database must exist already
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    _databases.Add(dbName);
                }
            }
        }

        public void Dispose()
        {
            foreach(var dbName in _databases)
            {
                try
                {
                    DropDatabase(dbName);
                }
                catch(ApiErrorException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
            foreach(var client in _clients)
            {
                try
                {
                    client.Dispose();
                }
                catch (ObjectDisposedException)
                {
                    continue;
                }
            }
        }
    }
}
