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
        private string otp;

        private string getRandString(int n)
        {
            char[] az = Enumerable.Range('a', 'z' - 'a' + 1).Select(i => (Char)i).ToArray();
            char[] AZ = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (Char)i).ToArray();
            char[] zeroNine = Enumerable.Range('0', '9' - '0' + 1).Select(i => (Char)i).ToArray();
            char[][] choiceArr = { az, AZ, zeroNine };
            char[] randString = new char[n];

            for (int i = 0; i < randString.Length; i++)
            {
                Random rangeChoice = new Random();
                int index = rangeChoice.Next(0, 3);
                char randChar = choiceArr[index][rangeChoice.Next(0, choiceArr[index].Length)];
                randString[i] = randChar;
            }

            return new string(randString);
        }

        public string generateOTP()
        {
            this.otp = getRandString(8);
            return this.otp;
        }

        public string generateToken()
        {
            return getRandString(25);
        }

        public bool validateOTP(string OTP)
        {
            return this.otp.Equals(OTP);
        }

        public bool validateUserInput(string email, string password)
        {
            Regex regexEmail = new Regex("^[a-z0-9.,@!]+$");
            Regex regexPassword = new Regex("^[a-zA-Z0-9.,@! ]+$");
            return regexEmail.IsMatch(email) && regexPassword.IsMatch(password); 
        }
    }
}
