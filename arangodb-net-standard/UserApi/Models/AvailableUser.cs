using System.Collections.Generic;

namespace ArangoDBNetStandard.UserApi.Models
{
    /// <summary>
    /// Represents information about a user available in ArangoDB.
    /// </summary>
    public class AvailableUser
    {
        /// <summary>
        /// The name of the user.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Whether the user is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Object with arbitrary extra data about the user.
        /// </summary>
        public Dictionary<string, object> Extra { get; set; }
    }
}
