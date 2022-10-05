namespace ArangoDBNetStandard.IndexApi.Models
{
    /// <summary>
    /// Request body to create a Multi-Dimensional index
    /// </summary>
    public class PostMultiDimensionalIndexBody : PostIndexBody
    {
        public PostMultiDimensionalIndexBody()
        {
            Type = IndexTypes.MultiDimensional;
            FieldValueTypes = "double";
        }

        /// <summary>
        /// Required. Always initialized to the value "double". 
        /// Currently only doubles are supported as values.
        /// </summary>
        public string FieldValueTypes { get; set; }
    }
}