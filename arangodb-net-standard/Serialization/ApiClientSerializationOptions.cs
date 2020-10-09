namespace ArangoDBNetStandard.Serialization
{
    /// <summary>
    /// The API client serialization options class.
    /// </summary>
    public class ApiClientSerializationOptions
    {
        /// <summary>
        /// Use camel case if true, otherwise depends on
        /// the action will be implemented in the serializer.
        /// </summary>
        public bool UseCamelCasePropertyNames { get; set; }
        
        /// <summary>
        /// True to ignore null values, otherwise false.
        /// </summary>
        public bool IgnoreNullValues { get; set; }

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
