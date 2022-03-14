using ArangoDBNetStandard.BulkOperationsApi.Models;
using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.BulkOperationsApi
{
    /// <summary>
    /// A client for interacting with ArangoDB Bulk Operations API endpoints,
    /// implementing <see cref="IBulkOperationsApiClient"/>.
    /// </summary>
    public class BulkOperationsApiClient:ApiClientBase, IBulkOperationsApiClient
    {
        /// <summary>
        /// The transport client used to communicate with the ArangoDB host.
        /// </summary>
        protected IApiClientTransport _transport;

        /// <summary>
        /// The root path of the API.
        /// </summary>
        protected string _collectionApiPath = "_api/collection";

        /// <summary>
        /// Creates an instance of <see cref="BulkOperationsApiClient"/>
        /// using the provided transport layer and the default JSON serialization.
        /// </summary>
        /// <param name="transport"></param>
        public BulkOperationsApiClient(IApiClientTransport transport)
            : base(new JsonNetApiClientSerialization())
        {
            _transport = transport;
        }

        /// <summary>
        /// Creates an instance of <see cref="BulkOperationsApiClient"/>
        /// using the provided transport and serialization layers.
        /// </summary>
        /// <param name="transport"></param>
        /// <param name="serializer"></param>
        public BulkOperationsApiClient(IApiClientTransport transport, IApiClientSerialization serializer)
            : base(serializer)
        {
            _transport = transport;
        }

        public virtual async Task<ImportDocumentsResponse> PostImportDocumentArraysAsync(ImportDocumentsQuery query, ImportDocumentArraysBody body)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<ImportDocumentsResponse> PostImportDocumentArraysAsync(ImportDocumentsQuery query, string body)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<ImportDocumentsResponse> PostImportDocumentObjectsAsync(ImportDocumentsQuery query, ImportDocumentObjectsBody body)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<ImportDocumentsResponse> PostImportDocumentObjectsAsync(ImportDocumentsQuery query, string body)
        {
            throw new NotImplementedException();
        }
    }
}
