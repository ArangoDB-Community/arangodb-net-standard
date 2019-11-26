namespace ArangoDBNetStandard.GraphApi
{
    /// <summary>
    /// Represents the internal attributes of an edge returned in a response.
    /// </summary>
    public class EdgeResult
    {
        public string _id { get; set; }

        public string _key { get; set; }

        public string _rev { get; set; }
    }
}
