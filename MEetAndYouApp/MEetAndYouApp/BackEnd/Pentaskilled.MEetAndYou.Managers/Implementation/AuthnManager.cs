using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Services.Contracts;
using Pentaskilled.MEetAndYou.Services.Implementation;
using Pentaskilled.MEetAndYou.DataAccess;
using System.Data.SqlClient;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Logging;
using Pentaskilled.MEetAndYou.Entities;

namespace Pentaskilled.MEetAndYou.Managers
{
    public class AuthnManager
    {
        private readonly IAuthnService _authnService;
        private readonly IAuthnDAO _authnDAO;
        private readonly IUMDAO _umDAO;                 //Needed to get userID using Email
        private readonly IAuthorizationDAO _authzDAO;
        private readonly LoggingManager _loggingManager;

        public AuthnManager()
        {
            _authnService = new AuthnService();
            _authnDAO = new AuthnDAO();
            _umDAO = new UMDAO();
            _authzDAO = new AuthorizationDAO();
            _loggingManager = new LoggingManager(new LoggingService(new LogDAO(), new Log()));
            
        }

        // this method always give the user a token with or without the correct credentials
        public AuthnResponse AuthenticateUser(string userEmail, string userPassword)
        {
            string userToken;
            AuthnResponse errorMessage = new AuthnResponse();
            bool isInputValid = true;
            bool isCredsValid = true;
            bool isOTPValid = true;
            AuthnResponse authnResponse = new AuthnResponse();
            
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
                    //int userID = 2;
                    bool saveResult = _authnDAO.SaveToken(userID, userToken).Result;
                    List<string> roles = _authzDAO.GetRoles(userID);

                    // Instantiate the object AutnResponse to return to the front
                    authnResponse = new AuthnResponse(userID, userToken, roles);
                    bool logResult = _loggingManager.BeginLogProcess("View", LogLevel.Info, userID, "User Log in to account").Result;

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
            catch(Exception ex) when (!isOTPValid)
            {
                throw new Exception(ex.Message);
            }

            if (userToken != null)
            {
                return authnResponse;
            }
            throw new Exception("Hello World");
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
                Console.WriteLine("Exception when Signing Out" + "\n" + ex.Message );
                return false;
            }
            return isSignOut;
        }
    }
}
