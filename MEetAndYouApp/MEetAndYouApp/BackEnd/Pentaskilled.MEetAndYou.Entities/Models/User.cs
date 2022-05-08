using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.Entities.Models
{
    public class User
    {

        private string email;
        private string phone;


        public User(string email, string phone) { 

            this.email = email;
            this.phone = phone;

        }
    }
}
