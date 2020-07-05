using System.Collections.Generic;

namespace ArangoDBNetStandard.UserApi.Models
{
    /// <summary>
    /// Represents a request body to create a user.
    /// </summary>
    public class PostUserBody
    {
        /// <summary>
        /// The name of the user.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// The user password.
        /// If no password is specified, the empty string will be used.
        /// If you pass the special value ARANGODB_DEFAULT_ROOT_PASSWORD,
        /// then the password will be set the value stored in the environment variable
        /// ARANGODB_DEFAULT_ROOT_PASSWORD.
        /// This can be used to pass an instance variable into ArangoDB.
        /// </summary>
        public string Passwd { get; set; }

        /// <summary>
        /// An optional flag that specifies whether the user is active.
        /// If not specified, this will default to true.
        /// </summary>
        public bool? Active { get; set; }

        /// <summary>
        /// Optional object with arbitrary extra data about the user.
        /// </summary>
        public Dictionary<string, object> Extra { get; set; }
    }
}
