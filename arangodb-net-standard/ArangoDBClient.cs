using ArangoDBNetStandard.CollectionApi;
using ArangoDBNetStandard.CursorApi;
using ArangoDBNetStandard.DatabaseApi;
using ArangoDBNetStandard.DocumentApi;
using ArangoDBNetStandard.TransactionApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ArangoDBNetStandard
{
    public class ArangoDBClient : IDisposable
    {
        private HttpClient _client;

        public CursorApiClient Cursor { get; private set; }

        public DatabaseApiClient Database { get; private set; }

        public DocumentApiClient Document { get; private set; }

        public CollectionApiClient Collection { get; private set; }

        public TransactionApiClient Transaction { get; private set; }

        public ArangoDBClient(HttpClient client)
        {
            _client = client;
            Cursor = new CursorApiClient(client);
            Database = new DatabaseApiClient(client);
            Document = new DocumentApiClient(client);
            Collection = new CollectionApiClient(client);
            Transaction = new TransactionApiClient(client);
            //Graph = new GraphApiClient(client);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
