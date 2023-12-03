using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace ArangoDBNetStandard.QueryApi.Models
{
    /// <summary>
    /// Response after posting executing non query.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ExecuteNonQueryResponse
    {
        public bool Error { get; set; }

        public int Code { get; set; }

        public ResultExtra Extra { get; set; }

        public bool Cached { get; set; }

        public bool HasMore { get; set; }

    }
}
