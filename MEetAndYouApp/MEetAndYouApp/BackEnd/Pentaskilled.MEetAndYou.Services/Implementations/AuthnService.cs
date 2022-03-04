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
            char[] az = Enumerable.Range('a', 'z' - 'a' + 1).Select(i => (Char)i).ToArray();
            char[] AZ = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (Char)i).ToArray();
            char[] zeroNine = Enumerable.Range('0', '9' - '0' + 1).Select(i => (Char)i).ToArray();

            char[][] passwordChoices = { az, AZ, zeroNine };
            char[] otp = new char[8];

            for (int i = 0; i < 8; i++)
            {
                Random rangeChoice = new Random();
                int index = rangeChoice.Next(0, 3);
                char otpChar = passwordChoices[index][rangeChoice.Next(0, passwordChoices[index].Length)];
                otp[i] = otpChar;
            }

            return new string(otp);
        }

        public string generateToken()
        {
            throw new NotImplementedException();
        }

        public bool validateOTP(string OTP)
        {
            Regex regexOTP = new Regex("^[a-zA-Z0-9]{8,8}$");
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
