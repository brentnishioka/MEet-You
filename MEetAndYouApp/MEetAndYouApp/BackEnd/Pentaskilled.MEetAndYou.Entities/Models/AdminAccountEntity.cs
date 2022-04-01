using System;

namespace Pentaskilled.MEetAndYou.Entities
{
    public class AdminAccountEntity
    {
        // Unique identifier of user
        public int AdminID { get; set; }

        // The email of a registered user
        public string Email { get; set; }
        
        public string Password { get; set; }
    }
}
