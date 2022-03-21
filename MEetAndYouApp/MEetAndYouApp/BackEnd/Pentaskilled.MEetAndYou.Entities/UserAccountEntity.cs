using System;

namespace Pentaskilled.MEetAndYou.Entities
{
    public class UserAccountEntity
    {
        // Unique identifier of user
        public int UserID { get; set; }

        // The email of a registered user
        public string Email { get; set; }
        
        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        // The date user account's was registered
        public string RegisterDate { get; set; }

        public int Active { get; set; }

        public UserAccountEntity()
        {

        }

        public UserAccountEntity(string email, string password, string phoneNumber)
        {
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
        }

    }

}
