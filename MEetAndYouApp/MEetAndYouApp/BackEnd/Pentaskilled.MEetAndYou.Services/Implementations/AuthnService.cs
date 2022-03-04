using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Services.Contracts;

namespace Pentaskilled.MEetAndYou.Services.Implementations
{
    public class AuthnService : IAuthnService
    {
        public string generateOTP()
        {
            throw new NotImplementedException();
        }

        public string generateToken()
        {
            throw new NotImplementedException();
        }

        public bool validateOTP()
        {
            throw new NotImplementedException();
        }

        public bool validateUserInput(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
