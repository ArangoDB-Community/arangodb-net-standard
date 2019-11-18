using System.Collections.Generic;
using System.Net;

namespace ArangoDBNetStandard.DocumentApi
{
    public class AllDocumentsResponse
    {
        public IEnumerable<string> Result { get; set; }

        public bool HasMore { get; set; }

        public bool Cached { get; set; }

        public AllDocumentsExtra Extra { get; set; }

        public bool Error { get; set; }

        public HttpStatusCode Code { get; set; }
    }
}