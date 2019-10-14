using ArangoDBNetStandard.CollectionApi;
using ArangoDBNetStandard.CursorApi;
using ArangoDBNetStandard.DatabaseApi;
using ArangoDBNetStandard.DocumentApi;
using ArangoDBNetStandard.TransactionApi;
using ArangoDBNetStandard.Transport;
using ArangoDBNetStandard.Transport.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ArangoDBNetStandard
{
    public class ArangoDBClient : IDisposable
    {
        private IApiClientTransport _transport;

        public CursorApiClient Cursor { get; private set; }

        public DatabaseApiClient Database { get; private set; }

        public DocumentApiClient Document { get; private set; }

        public CollectionApiClient Collection { get; private set; }

        public TransactionApiClient Transaction { get; private set; }

        public ArangoDBClient(HttpClient client)
        {
            _transport = new HttpApiTransport(client);
            Cursor = new CursorApiClient(_transport);
            Database = new DatabaseApiClient(_transport);
            Document = new DocumentApiClient(_transport);
            Collection = new CollectionApiClient(_transport);
            Transaction = new TransactionApiClient(_transport);
            //Graph = new GraphApiClient(client);
        }

        public ArangoDBClient(IApiClientTransport transport)
        {
            _transport = transport;
            Cursor = new CursorApiClient(_transport);
            Database = new DatabaseApiClient(_transport);
            Document = new DocumentApiClient(_transport);
            Collection = new CollectionApiClient(_transport);
            Transaction = new TransactionApiClient(_transport);
        }

        public void Dispose()
        {
            _transport.Dispose();
        }
    }
}
