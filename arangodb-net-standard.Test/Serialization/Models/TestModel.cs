using Newtonsoft.Json;
using System.Collections.Generic;

namespace ArangoDBNetStandardTest.Serialization.Models
{
    public class TestModel
    {
        public enum Number
        {
            One = 1,
            Two = 2
        }

        public string NullProperty { get; set; }

        public string AnotherNullProperty { get; set; }

        public string PropertyToCheckIfCamelCase { get; set; }

        public Number EnumToConvert { get; set; }

        [JsonProperty(PropertyName = "nameFromJsonProperty")]
        public string PropertyWithDifferentJsonName { get; set; }

        public Dictionary<string, object> MyStringDict { get; set; }

        public InnerTestModel PropertyWithClassType { get; set; }
    }
}
