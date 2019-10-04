using System.Collections.Generic;

namespace ArangoDB_NET_Standard.CursorApi
{
    public class PutCursorResponse<T>
    {
        public string Id { get; set; }

        public IEnumerable<T> Result { get; set; }

        public bool HasMore { get; set; }

        public long Count { get; set; }

        public bool Error { get; set; }

        public int StatusCode { get; set; }
    }
}