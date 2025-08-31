using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ArangoDBNetStandard.IndexApi.Converters
{
    public class IndexFieldsConverter : JsonConverter<IEnumerable<string>>
    {
        public override IEnumerable<string> ReadJson(JsonReader reader, Type objectType, IEnumerable<string> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
            {
                var first = token.First;
                if (first != null && first.Type == JTokenType.Object)
                {
                    // This is an array of objects, extract name properties
                    return token.Select(o => o["name"]?.ToString()).Where(n => !string.IsNullOrEmpty(n));
                }
                else
                {
                    // This is an array of strings
                    return token.ToObject<List<string>>();
                }
            }
            else if (token.Type == JTokenType.Null)
            {
                return null;
            }

            throw new JsonSerializationException("Unexpected token type for index fields.");
        }

        public override void WriteJson(JsonWriter writer, IEnumerable<string> value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteStartArray();
                foreach (var item in value)
                {
                    writer.WriteValue(item);
                }
                writer.WriteEndArray();
            }
        }
    }
}