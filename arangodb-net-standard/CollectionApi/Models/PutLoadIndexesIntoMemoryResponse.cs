using System.Net;

namespace ArangoDBNetStandard.CollectionApi.Models
{
    public class PutLoadIndexesIntoMemoryResponse:ResponseBase
    {
        /// <summary>
        /// Indicates whether the operation
        /// was successful or not.
        /// </summary>
        public bool Result { get; set; }
    }
}