namespace ArangoDBNetStandard.DocumentApi
{
    public class AllDocumentsBody
    {
        /// <summary>
        /// The type of the result. The following values are allowed:
        /// id: returns an array of document ids(_id attributes)
        /// key: returns an array of document keys(_key attributes)
        /// path: returns an array of document URI paths.This is the default.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The collection that should be queried
        /// </summary>
        public string Collection { get; set; }
    }
}
