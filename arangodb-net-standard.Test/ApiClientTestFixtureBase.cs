using ArangoDBNetStandard;
using ArangoDBNetStandard.DatabaseApi;
using ArangoDBNetStandard.Transport.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ArangoDBNetStandardTest
{
    public abstract class ApiClientTestFixtureBase : IDisposable, IAsyncLifetime
    {
        private List<string> _databases = new List<string>();

        private List<HttpApiTransport> _transports = new List<HttpApiTransport>();

        protected HttpApiTransport GetHttpTransport(string dbName)
        {
            var transport = new HttpApiTransport(
                new Uri("http://localhost:8529/"),
                dbName,
                "root",
                "root");
            _transports.Add(transport);
            return transport;
        }

        protected ArangoDBClient GetArangoDBClient(string dbName)
        {
            var httpClient = GetHttpTransport(dbName);
            return new ArangoDBClient(httpClient);
        }

        protected async Task DropDatabase(string dbName)
        {
            using (var systemDbClient = GetHttpTransport("_system"))
            {
                var dbApiClient = new DatabaseApiClient(systemDbClient);
                var response = await dbApiClient.DeleteDatabaseAsync(dbName);
            }
        }

        protected async Task CreateDatabase(string dbName)
        {
            // Create the test database
            using (var systemDbClient = GetHttpTransport("_system"))
            {
                var dbApiClient = new DatabaseApiClient(systemDbClient);
                try
                {
                    var postDatabaseResponse = await dbApiClient.PostDatabaseAsync(new PostDatabaseRequest
                    {
                        Name = dbName
                    });
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

        public virtual void Dispose()
        {   
            foreach (var transport in _transports)
            {
                try
                {
                    transport.Dispose();
                }
                catch (ObjectDisposedException)
                {
                    continue;
                }
            }
        }

        public virtual Task InitializeAsync()
        {
            return Task.FromResult(0);
        }

        public virtual async Task DisposeAsync()
        {
            foreach (var dbName in _databases)
            {
                try
                {
                    await DropDatabase(dbName);
                }
                catch (ApiErrorException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
        }
    }
}
