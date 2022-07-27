using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Converters;

namespace ArangoDBNetStandard.Serialization
{
    /// <summary>
    /// Provides special handling for dictionaries where we do not want to camel-case convert 
    /// nor ignore null values upon serialization.
    /// </summary>
    public class DictionaryValueConverter : JsonConverter
    {
        private ApiClientSerializationOptions _serializationOptions;

        public DictionaryValueConverter(ApiClientSerializationOptions serializationOptions)
        {
            _serializationOptions = serializationOptions;
        }

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
            // Use a local serializer for writing instead of the passed-in serializer         
            JsonSerializer mySerializer = new JsonSerializer
            {
                MissingMemberHandling = _serializationOptions.IgnoreMissingMember ? MissingMemberHandling.Ignore : MissingMemberHandling.Error,
                NullValueHandling = _serializationOptions.IgnoreNullValues ? NullValueHandling.Ignore : NullValueHandling.Include
            };
            if (_serializationOptions.UseStringEnumConversion)
            {
                var stringEnumConverter = new StringEnumConverter();
                mySerializer.Converters.Add(stringEnumConverter);
            }
            if (_serializationOptions.UseCamelCasePropertyNames)
            {
                mySerializer.ContractResolver = new CamelCasePropertyNamesExceptDictionaryContractResolver(_serializationOptions);
            }
            mySerializer.Serialize(writer, value);
        }
    }

}
