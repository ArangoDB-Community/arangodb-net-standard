namespace ArangoDBNetStandard.DatabaseApi.Models
{
    /// <summary>
    /// Additional options when creating databases.
    /// </summary>
    public class PostDatabaseOptions
    {
        /// <summary>
        ///  The sharding method to use for new collections 
        ///  in the database. Valid values are: “”, “flexible”, 
        ///  or “single”. The first two are equivalent.
        ///  (Optional and cluster only)
        /// </summary>
        public string Sharding { get; set; }

        /// <summary>
        ///  Default replication factor for new collections created in 
        ///  the database. Special values include “satellite”, which 
        ///  will replicate the collection to every DB-Server 
        ///  (Enterprise Edition only), and 1, which disables replication.
        ///  (Optional and cluster only)
        /// </summary>
        public int? ReplicationFactor { get; set; }

        /// <summary>
        /// Default write concern for new collections created in
        /// the database. It determines how many copies of each 
        /// shard are required to be in sync on the different 
        /// DB-Servers. If there are less then these many copies
        /// in the cluster a shard will refuse to write. Writes to 
        /// shards with enough up-to-date copies will succeed at 
        /// the same time however. This value cannot be larger 
        /// than <see cref="ReplicationFactor"/>
        /// (Optional and cluster only)
        /// </summary>
        public int? WriteConcern { get; set; }
    }
}