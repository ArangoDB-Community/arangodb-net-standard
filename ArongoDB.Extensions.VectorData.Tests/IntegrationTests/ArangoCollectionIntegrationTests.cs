using System.Text.Json.Serialization;

namespace ArangoDB.Extensions.VectorData.Tests.IntegrationTests;

[ExcludeFromCodeCoverage]
public partial class ArangoCollectionIntegrationTests : ArangoDbIntegrationTestBase
{
    public class TestRecord
    {
        [JsonPropertyName("_key")]
        public string Key { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public float[]? Embedding { get; set; }
    }
}
