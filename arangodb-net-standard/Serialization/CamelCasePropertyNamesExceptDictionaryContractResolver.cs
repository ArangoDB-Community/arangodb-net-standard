using Newtonsoft.Json.Serialization;
using System;

namespace ArangoDBNetStandard.Serialization
{
    /// <summary>
    /// Custom  <see cref="IContractResolver"></see> implementation designed for special handling of
    /// dictionaries where we do not want to camel-case keys or values, nor ignore null values, 
    /// on serialization.
    /// </summary>
    public class CamelCasePropertyNamesExceptDictionaryContractResolver : DefaultContractResolver
    {
        private ApiClientSerializationOptions _serializationOptions;
        public CamelCasePropertyNamesExceptDictionaryContractResolver(ApiClientSerializationOptions serializationOptions)
        {
            NamingStrategy = new CamelCaseNamingStrategy();
            _serializationOptions = serializationOptions;
        }

        protected override JsonDictionaryContract CreateDictionaryContract(Type objectType)
        {
            JsonDictionaryContract contract = base.CreateDictionaryContract(objectType);
            contract.DictionaryKeyResolver = propertyName => propertyName;
            contract.ItemConverter = new DictionaryValueConverter(_serializationOptions);
            return contract;
        }
    }

}
