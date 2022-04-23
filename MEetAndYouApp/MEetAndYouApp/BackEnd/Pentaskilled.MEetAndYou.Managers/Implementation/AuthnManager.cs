using System;
using System.Data.SqlClient;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Services.Contracts;
using Pentaskilled.MEetAndYou.Services.Implementation;

namespace Pentaskilled.MEetAndYou.Managers
{
    public class AuthnManager
    {
        private readonly IAuthnService _authnService;
        private readonly IAuthnDAO _authnDAO;
        private readonly IUMDAO _umDAO;                 //Needed to get userID using Email

        public AuthnManager()
        {
            _authnService = new AuthnService();
            _authnDAO = new AuthnDAO();
            _umDAO = new UMDAO();
        }

        // this method always give the user a token with or without the correct credentials
        public string AuthenticateUser(string userEmail, string userPassword)
        {
            string userToken;
            string errorMessage = "Invalid Username or Password";
            bool isInputValid = true;
            bool isCredsValid = true;
            bool isOTPValid = true;
            try
            {
                isInputValid = _authnService.validateUserInput(userEmail, userPassword);

                if (isInputValid == false)
                {
                    return errorMessage;
                }

                isCredsValid = _authnDAO.ValidateCredentials(userEmail, userPassword).Result;

                if (isCredsValid == true)
                {
                    string oneTimePw = _authnService.generateOTP();
                    //string phoneNum = _authnDAO.GetPhoneNum(userEmail, userPassword).Result;
                    isOTPValid = _authnService.validateOTP(oneTimePw);

                    userToken = _authnService.generateToken();

                    // Save the token to the database using userID
                    int userID = _umDAO.GetUserIDByEmail(userEmail).Result;
                    bool saveResult = _authnDAO.SaveToken(userID, userToken).Result;
                }
                else
                {
                    userToken = "Wrong Username or Password";
                }
            }
            catch (ArgumentException ex) when (!isInputValid)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex) when (!isCredsValid)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex) when (!isOTPValid)
            {
                throw new Exception(ex.Message);
            }

            if (userToken != null)
            {
                return userToken;
            }
            throw new NullReferenceException();
        }

        public bool SignOut(int userID)
        {
            bool isSignOut = false;
            try
            {
                isSignOut = _authnDAO.DeleteToken(userID).Result;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Sql Exception when Signing Out" + "\n" + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception when Signing Out" + "\n" + ex.Message);
                return false;
            }
            return isSignOut;
        }
    }
}
