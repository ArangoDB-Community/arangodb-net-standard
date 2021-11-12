using System.Net;

namespace ArangoDBNetStandard.DocumentApi.Models
{
    public class DocumentHeaderProperties
    {
        public string IfMatch { get; set; }

        public string IfNoneMatch { get; set; }

        /// <summary>
        /// Gets or sets the Id of a stream transaction.
        /// </summary>
        public string TransactionId { get; set; }

        public WebHeaderCollection ToWebHeaderCollection()
        {
            WebHeaderCollection collection = new WebHeaderCollection();
            if (IfMatch != null)
            {
                collection.Add(HttpRequestHeader.IfMatch, $"\"{IfMatch}\"");
            }

            if (IfNoneMatch != null)
            {
                collection.Add(HttpRequestHeader.IfNoneMatch, $"\"{IfNoneMatch}\"");
            }

            if (TransactionId != null)
            {
                collection.Add(CustomHttpHeaders.StreamTransactionHeader, TransactionId);
            }

            return collection;
        }
    }
}
