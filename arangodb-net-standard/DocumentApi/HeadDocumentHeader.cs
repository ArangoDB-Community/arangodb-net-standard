using System.Net;

namespace ArangoDBNetStandard.DocumentApi
{
    public class HeadDocumentHeader
    {
        public string IfMatch { get; set; }

        public string IfNoneMatch { get; set; }

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
            return collection;
        }
    }
}
