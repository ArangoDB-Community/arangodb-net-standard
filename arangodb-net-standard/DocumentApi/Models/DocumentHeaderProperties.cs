using System.Net;

namespace ArangoDBNetStandard.DocumentApi.Models
{
    public class DocumentHeaderProperties:ApiHeaderProperties
    {
        public string IfMatch { get; set; }

        public string IfNoneMatch { get; set; }

        public new WebHeaderCollection ToWebHeaderCollection()
        {
            WebHeaderCollection collection = base.ToWebHeaderCollection();
            
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