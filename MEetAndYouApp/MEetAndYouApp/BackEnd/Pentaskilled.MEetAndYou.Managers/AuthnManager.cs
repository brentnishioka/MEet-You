using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Services.Contracts;
using Pentaskilled.MEetAndYou.Services.Implementation;
using Pentaskilled.MEetAndYou.DataAccess;

namespace Pentaskilled.MEetAndYou.Managers
{
    public class AuthnManager
    {
        private readonly IAuthnService _authnService;
        private readonly IAuthnDAO _authnDAO;

        public AuthnManager()
        {
            _authnService = new AuthnService();
            _authnDAO = new AuthnDAO();
        }

        public string AuthenticateUser(string userEmail, string userPassword)
        {
            string userToken;
            bool isInputValid = true;
            bool isCredsValid = true;
            bool isOTPValid = true;
            try
            {
                isInputValid = _authnService.validateUserInput(userEmail, userPassword);
                isCredsValid = _authnDAO.ValidateCredentials(userEmail, userPassword).Result;
                string oneTimePw = _authnService.generateOTP();
                string phoneNum = _authnDAO.GetPhoneNum(userEmail, userPassword).Result;
                isOTPValid = _authnService.validateOTP(oneTimePw);

                userToken = _authnService.generateToken();

            }
            catch (ArgumentException ex) when (!isInputValid)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex) when (!isCredsValid)
            {
                throw new Exception(ex.Message);
            }
            catch(Exception ex) when (!isOTPValid)
            {
                throw new Exception(ex.Message);
            }

            if (userToken != null)
            {
                return userToken;
            }
            throw new NullReferenceException();
        }
    }
}
