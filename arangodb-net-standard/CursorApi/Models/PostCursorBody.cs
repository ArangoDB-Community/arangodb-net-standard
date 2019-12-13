using System.Collections.Generic;

namespace ArangoDBNetStandard.CursorApi.Models
{
    public class PostCursorBody
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
