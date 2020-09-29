namespace ArangoDBNetStandard.Serialization
{
    /// <summary>
    /// The API client serilization options class.
    /// </summary>
    public class ApiClientSerializationOptions
    {
        /// <summary>
        /// Use camlel case if true, otherwise depends on
        /// the action will be implemented in the serializer.
        /// </summary>
        public bool UseCamelCasePropertyNames { get; }
        
        /// <summary>
        /// True to ignore values, otherwise false.
        /// </summary>
        public bool IgnoreNullValues { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="useCamelCasePropertyNames"></param>
        /// <param name="ignoreNullValues"></param>
        public ApiClientSerializationOptions(bool useCamelCasePropertyNames, bool ignoreNullValues)
        {
            UseCamelCasePropertyNames = useCamelCasePropertyNames;
            IgnoreNullValues = ignoreNullValues;
        }
    }
}
