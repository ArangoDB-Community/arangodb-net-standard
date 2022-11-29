using System.Collections.Generic;

namespace ArangoDBNetStandard.DatabaseApi.Models
{
    public class PostDatabaseBody
    {
        /// <summary>
        /// A valid database name. The name must conform to the selected 
        /// naming convention for databases. If the name contains Unicode
        /// characters, the name must be NFC-normalized. 
        /// Non-normalized names will be rejected by arangod.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Optional attributes that can be specified for the new database.
        /// </summary>
        public PostDatabaseOptions Options { get; set; }

        /// <summary>
        /// Users to be created in the new database. The users will be granted
        /// Administrate permissions for the new database. If a value is not
        /// specified for this property or a zero-length array or list is specified, 
        /// the default user root will be created. This will ensure that the new 
        /// database will be accessible after it is created. The root user is 
        /// created with an empty password. 
        /// </summary>
        public IEnumerable<DatabaseUser> Users { get; set; }
    }
}