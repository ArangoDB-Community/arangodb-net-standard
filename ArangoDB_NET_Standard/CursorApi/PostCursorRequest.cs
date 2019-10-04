using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDB_NET_Standard.CursorApi
{
    public class PostCursorRequest
    {
        public string Query { get; set; }

        public Dictionary<string, object> BindVars { get; set; }

        public PostCursorOptions Options { get; set; }

        public bool? Count { get; set; }

        public long? BatchSize { get; set; }

        public bool? Cache { get; set; }

        public long? MemoryLimit { get; set; }

        public int? Ttl { get; set; }
    }

}
