using ArangoDBNetStandard.BulkOperationsApi.Models;
using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
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
        protected string _bulkOperationsApiPath = "_api/import";

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

        /// <summary>
        /// Imports data arrays as documents into a collection.
        /// POST /_api/import
        /// </summary>
        /// <param name="query">Options for the import.</param>
        /// <param name="body">The body of the request containing required properties.</param>
        /// <returns></returns>
        public virtual async Task<ImportDocumentsResponse> PostImportDocumentArraysAsync(
            ImportDocumentsQuery query,
            ImportDocumentArraysBody body)
        {
            if (body == null)
            {
                throw new ArgumentException("body is required", nameof(body));
            }
            else
            {
                if (body.DocumentAttributes == null || body.DocumentAttributes.Count() < 1)
                {
                    throw new ArgumentException("DocumentAttributes is required", nameof(body.DocumentAttributes));
                }

                if (body.ValueArrays == null || body.DocumentAttributes.Count() < 1)
                {
                    throw new ArgumentException("ValueArrays is required", nameof(body.ValueArrays));
                }
                else
                {
                    var maxValueLength = (from l in body.ValueArrays
                                          where l != null
                                          select l.Count()).Max();
                    if (maxValueLength != body.DocumentAttributes.Count())
                    {
                        throw new ArgumentException(
                            "Every array in ValueArrays must have the exact number of elements as the DocumentAttributes array.",
                            nameof(body.ValueArrays));
                    }
                }
            }
            var contentString = string.Empty;
            var options = new ApiClientSerializationOptions(true, true);
            foreach (var valueArr in body.ValueArrays)
            {
                contentString += GetContentString(valueArr, options) + Environment.NewLine;
            }
            return await PostImportDocumentArraysAsync(query, contentString);
        }

        /// <summary>
        /// Imports data arrays as documents into a collection.
        /// POST /_api/import
        /// </summary>
        /// <param name="query">Options for the import.</param>
        /// <param name="jsonBody">The body of the request containing required value arrays.</param>
        /// <returns></returns>
        public virtual async Task<ImportDocumentsResponse> PostImportDocumentArraysAsync(
            ImportDocumentsQuery query,
            string jsonBody)
        {
            if (query == null)
            {
                throw new ArgumentException("query is required", nameof(query));
            }

            if (string.IsNullOrEmpty(query.Collection))
            {
                throw new ArgumentException("Collection name is required", nameof(query.Collection));
            }

            if (string.IsNullOrEmpty(jsonBody))
            {
                throw new ArgumentException("jsonBody is required", nameof(jsonBody));
            }

            string uriString = _bulkOperationsApiPath;
            uriString += "?" + query.ToQueryString();
            var content = Encoding.UTF8.GetBytes(jsonBody);
            using (var response = await _transport.PostAsync(uriString, content).ConfigureAwait(false))
            {
                var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return DeserializeJsonFromStream<ImportDocumentsResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }

        public virtual async Task<ImportDocumentsResponse> PostImportDocumentObjectsAsync(
            ImportDocumentsQuery query, 
            ImportDocumentObjectsBody body)
        {
            if (query == null)
            {
                throw new ArgumentException("query is required", nameof(query));
            }

            if (string.IsNullOrEmpty(query.Type))
            {
                throw new ArgumentException("Type is required", nameof(query.Type));
            }

            if (body == null)
            {
                throw new ArgumentException("body is required", nameof(body));
            }
            else if (body.Documents == null || body.Documents.Count() < 1)
            {
                throw new ArgumentException("Documents is required", nameof(body.Documents));
            }

            var contentString = string.Empty;
            var options = new ApiClientSerializationOptions(true, true);

            if (query.Type == "documents")
            {
                //body should be a list of documents seperated by newline char
                foreach (var doc in body.Documents)
                {
                    contentString += GetContentString(doc, options) + Environment.NewLine;
                }
            }
            else
            {
                //body should be one array of JSON objects
                contentString = GetContentString(body.Documents, options);
            }
            return await PostImportDocumentObjectsAsync(query, contentString);
        }

        public virtual async Task<ImportDocumentsResponse> PostImportDocumentObjectsAsync(
            ImportDocumentsQuery query,
            string jsonBody)
        {
            if (query == null)
            {
                throw new ArgumentException("query is required", nameof(query));
            }

            if (string.IsNullOrEmpty(query.Collection))
            {
                throw new ArgumentException("Collection name is required", nameof(query.Collection));
            }

            if (string.IsNullOrEmpty(query.Type))
            {
                throw new ArgumentException("Type is required", nameof(query.Type));
            }

            if (string.IsNullOrEmpty(jsonBody))
            {
                throw new ArgumentException("jsonBody is required", nameof(jsonBody));
            }

            string uriString = _bulkOperationsApiPath;
            uriString += "?" + query.ToQueryString();
            var content = Encoding.UTF8.GetBytes(jsonBody);
            using (var response = await _transport.PostAsync(uriString, content).ConfigureAwait(false))
            {
                var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return DeserializeJsonFromStream<ImportDocumentsResponse>(stream);
                }
                throw await GetApiErrorException(response).ConfigureAwait(false);
            }
        }





    }
}
