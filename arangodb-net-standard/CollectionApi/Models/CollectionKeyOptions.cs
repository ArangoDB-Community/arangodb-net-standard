namespace ArangoDBNetStandard.CollectionApi.Models
{
    /// <summary>
    /// Additional options for key generation of ArangoDB collections.
    /// </summary>
    public class CollectionKeyOptions
    {
        /// <summary>
        /// If set to true, then it is allowed to supply own key values
        /// in the _key attribute of a document.
        /// If set to false, then the key generator will solely be responsible for generating keys
        /// and supplying own key values in the _key attribute of documents is considered an error.
        /// </summary>
        public bool AllowUserKeys { get; set; }

        /// <summary>
        /// Increment value for autoincrement key generator.
        /// Not used for other key generator types.
        /// </summary>
        public long Increment { get; set; }

        /// <summary>
        /// Initial offset value for autoincrement key generator.
        /// Not used for other key generator types.
        /// </summary>
        public long Offset { get; set; }

        /// <summary>
        /// Apecifies the type of the key generator.
        /// The currently available generators are traditional, autoincrement, uuid and padded.
        /// </summary>
        public string Type { get; set; }
    }
}
