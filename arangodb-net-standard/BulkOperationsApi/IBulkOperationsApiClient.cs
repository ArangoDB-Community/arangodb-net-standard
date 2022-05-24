using ArangoDBNetStandard.BulkOperationsApi.Models;
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
        /// <returns></returns>
        Task<ImportDocumentsResponse> PostImportDocumentArraysAsync(
            ImportDocumentsQuery query,
            ImportDocumentArraysBody body);

        /// <summary>
        /// Imports data arrays as documents into a collection.
        /// Use this method if you have already structured the
        /// JSON body according to the specifications.
        /// POST /_api/import
        /// </summary>
        /// <param name="query">Options for the import.</param>
        /// <param name="jsonBody">The body of the request containing required value arrays.</param>
        /// <returns></returns>
        Task<ImportDocumentsResponse> PostImportDocumentArraysAsync(
            ImportDocumentsQuery query,
            string jsonBody);


        /// <summary>
        /// Imports objects as documents into a collection.
        /// POST /_api/import
        /// </summary>
        /// <param name="query">Options for the import.</param>
        /// <param name="body">The body of the request containing required objects.</param>
        /// <returns></returns>
        Task<ImportDocumentsResponse> PostImportDocumentObjectsAsync<T>(
            ImportDocumentsQuery query,
            ImportDocumentObjectsBody<T> body);

        /// <summary>
        /// Imports objects as documents into a collection.
        /// Use this method if you have already structured the
        /// JSON body according to the specifications.
        /// POST /_api/import
        /// </summary>
        /// <param name="query">Options for the import.</param>
        /// <param name="jsonBody">The body of the request containing the required JSON objects.</param>
        /// <returns></returns>
        Task<ImportDocumentsResponse> PostImportDocumentObjectsAsync(
            ImportDocumentsQuery query,
            string jsonBody);
    }
}
