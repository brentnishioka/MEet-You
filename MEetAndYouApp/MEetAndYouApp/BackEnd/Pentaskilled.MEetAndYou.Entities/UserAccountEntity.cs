﻿using System;

namespace Pentaskilled.MEetAndYou.Entities
{
    public class UserAccountEntity
    {

        // Unique identifier of user
        public string UserID { get; }

        // The email of a registered user
        public string Email { get; set; }
        
        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        // User's role ( Administrator | Regular User)
        public string Role { get; set; }

        // The date user account's was registered
        public DateTime RegisterDate { get; set; }

        public int Active { get; set; }
    }
}
