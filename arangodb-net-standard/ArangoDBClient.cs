using ArangoDBNetStandard.AdminApi;
using ArangoDBNetStandard.AnalyzerApi;
using ArangoDBNetStandard.AqlFunctionApi;
using ArangoDBNetStandard.AuthApi;
using ArangoDBNetStandard.BulkOperationsApi;
using ArangoDBNetStandard.CollectionApi;
using ArangoDBNetStandard.CursorApi;
using ArangoDBNetStandard.DatabaseApi;
using ArangoDBNetStandard.DocumentApi;
using ArangoDBNetStandard.GraphApi;
using ArangoDBNetStandard.IndexApi;
using ArangoDBNetStandard.PregelApi;
using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.TransactionApi;
using ArangoDBNetStandard.Transport;
using ArangoDBNetStandard.Transport.Http;
using ArangoDBNetStandard.UserApi;
using ArangoDBNetStandard.ViewApi;
using System.Net.Http;

namespace ArangoDBNetStandard
{
    /// <summary>
    /// Wrapper class providing access to the complete set of ArangoDB REST resources.
    /// </summary>
    public class ArangoDBClient : IArangoDBClient
    {
        protected readonly bool _suppressTransportDisposal;

        /// <summary>
        /// The transport client used to communicate with the ArangoDB host.
        /// </summary>
        protected IApiClientTransport _transport;

        /// <summary>
        /// AQL user functions management API.
        /// </summary>
        public IAqlFunctionApiClient AqlFunction { get; private set; }

        /// <summary>
        /// Auth API
        /// </summary>
        public IAuthApiClient Auth { get; private set; }

        /// <summary>
        /// Cursor API
        /// </summary>
        public ICursorApiClient Cursor { get; private set; }

        /// <summary>
        /// Database API
        /// </summary>
        public IDatabaseApiClient Database { get; private set; }

        /// <summary>
        /// Document API
        /// </summary>
        public IDocumentApiClient Document { get; private set; }

        /// <summary>
        /// Collection API
        /// </summary>
        public ICollectionApiClient Collection { get; private set; }

        /// <summary>
        /// Transaction API
        /// </summary>
        public ITransactionApiClient Transaction { get; private set; }

        /// <summary>
        /// Graph API
        /// </summary>
        public IGraphApiClient Graph { get; private set; }

        /// <summary>
        /// User management API
        /// </summary>
        public IUserApiClient User { get; private set; }

        /// <summary>
        /// Index management API
        /// </summary>
        public IIndexApiClient Index { get; private set; }

        /// <summary>
        /// Bulk Operations API.
        /// </summary>
        public IBulkOperationsApiClient BulkOperations { get; private set; }

        /// <summary>
        /// View management API.
        /// </summary>          
        public IViewApiClient View { get; private set; }

        /// <summary>
        /// Analyzer management API.
        /// </summary>
        public IAnalyzerApiClient Analyzer { get; private set; }

        /// <summary>
        /// Admin management API
        /// </summary>
        public IAdminApiClient Admin { get; private set; }

        /// <summary>
        /// Pregel management API
        /// </summary>
        public IPregelApiClient Pregel { get; private set; }

        /// <summary>
        /// Create an instance of <see cref="ArangoDBClient"/> from an existing
        /// <see cref="HttpClient"/> instance, using the default JSON serialization.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="suppressClientDisposal">
        /// True to prevent disposal of the provided <see cref="HttpClient"/> instance
        /// when <see cref="ArangoDBClient"/> is disposed.
        /// Default is false, to avoid a breaking change.
        /// </param>
        public ArangoDBClient(HttpClient client, bool suppressClientDisposal = false)
        {
            _transport = new HttpApiTransport(
                client,
                HttpContentType.Json,
                suppressClientDisposal);
            _suppressTransportDisposal = false;

            var serialization = new JsonNetApiClientSerialization();

            InitializeApis(_transport, serialization);
        }

        /// <summary>
        /// Create an instance of <see cref="ArangoDBClient"/>
        /// using the provided transport layer and the default JSON serialization.
        /// </summary>
        /// <param name="transport">The ArangoDB transport layer implementation.</param>
        /// <param name="suppressTransportDisposal">
        /// True to prevent disposal of the provided <see cref="IApiClientTransport"/> instance
        /// when <see cref="ArangoDBClient"/> is disposed.
        /// Default is false, to avoid a breaking change.
        /// </param>
        public ArangoDBClient(
            IApiClientTransport transport,
            bool suppressTransportDisposal = false)
        {
            _transport = transport;
            _suppressTransportDisposal = suppressTransportDisposal;

            var serialization = new JsonNetApiClientSerialization();

            InitializeApis(_transport, serialization);
        }

        /// <summary>
        /// Create an instance of <see cref="ArangoDBClient"/>
        /// using the provided transport and serialization layers.
        /// </summary>
        /// <param name="transport">The ArangoDB transport layer implementation.</param>
        /// <param name="serialization">The serialization layer implementation.</param>
        /// <param name="suppressTransportDisposal">
        /// True to prevent disposal of the provided <see cref="IApiClientTransport"/> instance
        /// when <see cref="ArangoDBClient"/> is disposed.
        /// Default is false, to avoid a breaking change.
        /// </param>
        public ArangoDBClient(
            IApiClientTransport transport,
            IApiClientSerialization serialization,
            bool suppressTransportDisposal = false)
        {
            _transport = transport;
            _suppressTransportDisposal = suppressTransportDisposal;

            InitializeApis(_transport, serialization);
        }

        /// <summary>
        /// Disposes the underlying transport instance.
        /// </summary>
        public void Dispose()
        {
            if (_suppressTransportDisposal)
            {
                return;
            }
            _transport.Dispose();
        }

        private void InitializeApis(
            IApiClientTransport transport,
            IApiClientSerialization serialization)
        {
            AqlFunction = new AqlFunctionApiClient(transport, serialization);
            Auth = new AuthApiClient(transport, serialization);
            Cursor = new CursorApiClient(transport, serialization);
            Database = new DatabaseApiClient(transport, serialization);
            Document = new DocumentApiClient(transport, serialization);
            Collection = new CollectionApiClient(transport, serialization);
            Transaction = new TransactionApiClient(transport, serialization);
            Graph = new GraphApiClient(transport, serialization);
            User = new UserApiClient(transport, serialization);
            Index = new IndexApiClient(transport, serialization);
            BulkOperations = new BulkOperationsApiClient(transport, serialization);
            View = new ViewApiClient(transport, serialization);
            Analyzer = new AnalyzerApiClient(transport, serialization);
            Admin = new AdminApiClient(transport, serialization);
            Pregel = new PregelApiClient(transport, serialization);
        }
    }
}