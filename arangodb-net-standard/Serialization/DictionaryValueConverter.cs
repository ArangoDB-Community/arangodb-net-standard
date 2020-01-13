using Newtonsoft.Json;
using System;

namespace ArangoDBNetStandard.Serialization
{
    /// <summary>
    /// Provides special handling for dictionaries where we do not want to camel-case convert 
    /// nor ignore null values upon serialization.
    /// </summary>
    public class DictionaryValueConverter : JsonConverter
    {
        private static JsonSerializer _serializer = new JsonSerializer
        {
            NullValueHandling = NullValueHandling.Include
        };

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize(reader);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Use our local serializer for writing instead of the passed-in serializer
            _serializer.Serialize(writer, value);
        }
    }

}
