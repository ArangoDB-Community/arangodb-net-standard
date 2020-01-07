namespace ArangoDBNetStandard.AqlFunctionApi.Models
{
    /// <summary>
    /// Represents query parameters used when deleting an AQL user function.
    /// </summary>
    public class DeleteAqlFunctionQuery
    {
        /// <summary>
        /// Whether the function name provided is treated as a namespace prefix.
        /// If set to true, all functions in the specified namespace will be deleted.
        /// The returned number of deleted functions may become 0 if none matches the string.
        /// If set to false, the function name provided must be fully qualified, including any namespaces.
        /// If none matches the name, HTTP 404 is returned.
        /// </summary>
        public bool? Group { get; set; }

        internal string ToQueryString()
        {
            if (Group != null)
            {
                return "group=" + Group.ToString().ToLower();
            }
            else
            {
                return "";
            }
        }
    }
}
