using System.Collections.Generic;

namespace ArangoDBNetStandard.UserApi.Models
{
    /// <summary>
    /// Represents a response returned after listing
    /// the databases accesible by a user.
    /// </summary>
    public class GetAccessibleDatabasesResponse : ResponseBase
    {
        /// <summary>
        /// Contains the databases names as keys,
        /// and the associated privileges for the database as <see cref="string"/> values.
        /// In case you specified <see cref="GetAccessibleDatabasesQuery.Full"/>,
        /// it will contain <see cref="object"/> values with the permissions for the databases
        /// ('permission') as well as the permissions for the collections ('collections').
        /// </summary>
        /// <remarks>
        /// The type of the full <see cref="object"/> values will depend on the serializer used.
        /// When using the default <see cref="Serialization.JsonNetApiClientSerialization"/>,
        /// values will be <see cref="Newtonsoft.Json.Linq.JObject"/>.
        /// </remarks>
        public Dictionary<string, object> Result { get; set; }
    }
}
