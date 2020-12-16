using System.Collections.Generic;

namespace ArangoDBNetStandard.UserApi.Models
{
    /// <summary>
    /// Represents a response returned after fetching all users.
    /// </summary>
    public class GetUsersResponse : ResponseBase
    {
        /// <summary>
        /// The users that were found.
        /// </summary>
        public IEnumerable<AvailableUser> Result { get; set; }
    }
}
