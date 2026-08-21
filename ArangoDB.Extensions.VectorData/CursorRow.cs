using System.Text.Json.Serialization;

namespace ArangoDB.Extensions.VectorData;

// Cursor row type matching the AQL RETURN {{ doc: <object>, score: score }}
public class CursorRow<TRecord>
{
    [JsonPropertyName("doc")]
    public TRecord? Doc { get; set; }

    [JsonPropertyName("score")]
    public double Score { get; set; }
}
