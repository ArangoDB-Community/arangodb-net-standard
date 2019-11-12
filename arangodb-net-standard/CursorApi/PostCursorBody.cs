using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard.CursorApi
{
    public class PostCursorBody
    {
        public string Query { get; set; }

        public Dictionary<string, object> BindVars { get; set; }

        public PostCursorQuery Options { get; set; }

        public bool? Count { get; set; }

        public long? BatchSize { get; set; }

        public bool? Cache { get; set; }

        public long? MemoryLimit { get; set; }

        public int? Ttl { get; set; }
    }

}
