using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.Services.Contracts
{
    public interface IAuthnService
    {
        bool validateUserInput(string email, string password);
        string generateOTP();
        bool validateOTP(string OTP);

        // Not sure if the token should b a string or other datatype

        void sendOTP(string phoneNum, string otp);
        string generateToken();

    }
}
