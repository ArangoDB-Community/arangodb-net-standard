using ArangoDBNetStandard;
using ArangoDBNetStandard.DatabaseApi;
using ArangoDBNetStandard.Transport.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ArangoDBNetStandardTest
{
    public abstract class ApiClientTestFixtureBase: IDisposable
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

        protected void DropDatabase(string dbName)
        {
            using (var systemDbClient = GetHttpTransport("_system"))
            {
                var dbApiClient = new DatabaseApiClient(systemDbClient);
                var response = dbApiClient.DeleteDatabaseAsync(dbName)
                    .GetAwaiter().GetResult();
            }
        }

        protected void CreateDatabase(string dbName)
        {
            // Create the test database
            using (var systemDbClient = GetHttpTransport("_system"))
            {
                var dbApiClient = new DatabaseApiClient(systemDbClient);
                try
                {
                    var postDatabaseResponse = dbApiClient.PostDatabaseAsync(new PostDatabaseRequest
                    {
                        Name = dbName
                    }).GetAwaiter().GetResult();
                }
                catch (ApiErrorException ex) when (ex.ApiError.ErrorNum == ErrorCode.ARANGO_DUPLICATE_NAME)
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
            foreach(var transport in _transports)
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
    }
}
