using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public bool validateOTP(string OTP)
        {
            Regex regexOTP = new Regex("^[a-zA-Z0-9]+$");
            return regexOTP.IsMatch(OTP);
        }

        public bool validateUserInput(string email, string password)
        {
            Regex regexEmail = new Regex("^[a-z0-9.,@!]+$");
            Regex regexPassword = new Regex("^[a-zA-Z0-9.,@! ]+$");
            return regexEmail.IsMatch(email) && regexPassword.IsMatch(password); 
        }
    }
}
