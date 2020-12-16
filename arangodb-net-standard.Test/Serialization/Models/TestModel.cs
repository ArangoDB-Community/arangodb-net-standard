namespace ArangoDBNetStandardTest.Serialization.Models
{
    public class TestModel
    {
        public enum Number
        {
            One = 1,
            Two = 2
        }

        public string NullPropertyToIgnore { get; set; }

        public string PropertyToCamelCase { get; set; }

        public Number EnumToConvertToString { get; set; }
    }
}
