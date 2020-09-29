namespace ArangoDBNetStandard.Serialization
{
    /// <summary>
    /// Define the options.
    /// </summary>
    public interface IApiClientSerializationOptions
    {
        bool IgnoreNullValues { get; }

        bool UseCamelCasePropertyNames { get; }
    }
}
