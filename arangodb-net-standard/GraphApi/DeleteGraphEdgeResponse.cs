using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ArangoDBNetStandard.GraphApi
{
    public class DeleteGraphEdgeResponse<T>
    {
        public T Old { get; set; }

        public bool Removed { get; set; }

        public HttpStatusCode Code { get; set; }

        public bool Error { get; set; }
    }
}
