using System.Text.Json.Serialization;

namespace ArangoDB.Extensions.VectorData;

// Extended cursor row type for hybrid search results
public class HybridSearchRow<TRecord>
{
    [JsonPropertyName("doc")]
    public TRecord? Doc { get; set; }

    [JsonPropertyName("score")]
    public double Score { get; set; }

    [JsonPropertyName("vectorScore")]
    public double VectorScore { get; set; }

    [JsonPropertyName("keywordScore")]
    public double KeywordScore { get; set; }
}
