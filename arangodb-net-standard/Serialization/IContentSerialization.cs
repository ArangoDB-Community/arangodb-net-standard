using System.IO;

namespace ArangoDBNetStandard.Serialization
{
    public interface IContentSerialization
    {
        T DeserializeJsonFromStream<T>(Stream stream);

        string SerializeToJson<T>(T item, bool useCamelCasePropertyNames, bool ignoreNullValues);
    }
}
