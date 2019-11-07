using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Net;

namespace ArangoDBNetStandard.CursorApi
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CursorResponse<T>
    {
        public bool Error { get; set; }

        public long Count { get; set; }

        public HttpStatusCode Code { get; set; }

        public CursorResultExtra Extra { get; set; }

        public bool Cached { get; set; }

        public bool HasMore { get; set; }

        [JsonProperty("result", NamingStrategyType = typeof(DefaultNamingStrategy))]
        public IList<T> Result { get; set; }

        public string Id { get; set; }
    }
}
