using System.Collections.Generic;

namespace Pentaskilled.MEetAndYou.Entities
{
    public class UserRole
    {
        // Unique identifier of user
        public int UserID { get; set; }

        // The email of a registered user
        public IEnumerable<string> Roles { get; set; }

        public UserRole(int userID, IEnumerable<string> roles)
        {
            UserID = userID;
            Roles = roles;
        }
    }
}
