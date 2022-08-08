using ArangoDBNetStandard.BulkOperationsApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ArangoDBNetStandard.BulkOperationsApi
{
    /// <summary>
    /// Defines a client to access the ArangoDB API for
    /// Bulk Operations
    /// </summary>
    public interface IBulkOperationsApiClient
    {
        /// <summary>
        /// Imports data arrays as documents into a collection.
        /// POST /_api/import
        /// </summary>
        /// <param name="query">Options for the import.</param>
        /// <param name="body">The body of the request containing required properties.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<ImportDocumentsResponse> PostImportDocumentArraysAsync(
            ImportDocumentsQuery query,
            ImportDocumentArraysBody body,
            CancellationToken token = default);

        /// <summary>
        /// Imports data arrays as documents into a collection.
        /// Use this method if you have already structured the
        /// JSON body according to the specifications.
        /// POST /_api/import
        /// </summary>
        /// <param name="query">Options for the import.</param>
        /// <param name="jsonBody">The body of the request containing required value arrays.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<ImportDocumentsResponse> PostImportDocumentArraysAsync(
            ImportDocumentsQuery query,
            string jsonBody,
            CancellationToken token = default);


        /// <summary>
        /// Imports objects as documents into a collection.
        /// POST /_api/import
        /// </summary>
        /// <param name="query">Options for the import.</param>
        /// <param name="body">The body of the request containing required objects.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<ImportDocumentsResponse> PostImportDocumentObjectsAsync<T>(
            ImportDocumentsQuery query,
            ImportDocumentObjectsBody<T> body,
            CancellationToken token = default);

        /// <summary>
        /// Imports objects as documents into a collection.
        /// Use this method if you have already structured the
        /// JSON body according to the specifications.
        /// POST /_api/import
        /// </summary>
        /// <param name="query">Options for the import.</param>
        /// <param name="jsonBody">The body of the request containing the required JSON objects.</param>
        /// <param name="token">A CancellationToken to observe while waiting for the task to complete or to cancel the task.</param>
        /// <returns></returns>
        Task<ImportDocumentsResponse> PostImportDocumentObjectsAsync(
            ImportDocumentsQuery query,
            string jsonBody,
            CancellationToken token = default);
    }
}
