namespace ArangoDBNetStandard.DatabaseApi
{
    public class CurrentDatabaseResult
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public string Path { get; set; }

        public bool? IsSystem { get; set; }
    }
}
