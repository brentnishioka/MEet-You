using System;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Services.Contracts;
using Pentaskilled.MEetAndYou.Services.Implementation;
using Pentaskilled.MEetAndYou.Services.Contracts;

namespace Pentaskilled.MEetAndYou.Managers
{
    public class AccountCreationManager
    {
        private IUMService _UMService;

        /// <summary>
        /// Checks for user account in the database
        /// </summary>
        /// <param name="email">User given email</param>
        /// <param name="password">User generated password</param>
        /// <param name="phoneNumber">User given phone number</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string CheckAccountAvailability(string email)
        {
            try
            {
                UserAccountEntity user = new UserAccountEntity();

                UMManager _UMManager = new UMManager();

                AccountCreationDAO ACManager = new AccountCreationDAO();

                user.Email = email;

                if (!ACManager.DoesEmailExist(user).Result)
                {
                    return "Username is available.";
                }
                else
                {
                    return "Username is not available.";
                }




            }
            catch (Exception)
            {
                throw new Exception();
            }
         
        }

        /// <summary>
        /// Creates user based off account registration
        /// </summary>
        /// <param name="email">User given email</param>
        /// <param name="password">User generated password</param>
        /// <param name="phoneNumber">User given phone number</param>
        /// <returns>Successful account creation</returns>
        /// <exception cref="Exception"></exception>
        public async Task<BaseResponse> BeginAccountCreation(string email, string password, string phoneNumber)
        {

            try
            {
                UserAccountEntity user = new UserAccountEntity();
                IAuthnService _authnService = new AuthnService();
                UMManager _UMManager = new UMManager();
                AccountCreationDAO _accountCreation = new AccountCreationDAO();
                UMDAO uMDAO = new UMDAO();

                user.Email = email;
                user.Password = password;
                user.PhoneNumber = phoneNumber;
                user.RegisterDate = DateTime.UtcNow.ToString();
                user.Active = Convert.ToInt32("0");
                string accountCreated = "";
                /*string userOTP = _authnService.generateOTP();*/

             /*   bool isUnActivated = false;
                string result = "";
*/
                //bool IsAccountCreated = false;

                if (CheckAccountAvailability(email) == "Username is available.")
                {

                    if (_UMManager.VerifyUserInfo(email, password, phoneNumber) != "User info is successfully verified.")
                    {
                        return new BaseResponse(_UMManager.VerifyUserInfo(email, password, phoneNumber), false);
                    }
                    else
                    {
                        accountCreated = UMManager.BeginCreateUser(email, password, phoneNumber, user.RegisterDate, user.Active.ToString());
/*
                        if (accountCreated != "User account was successfully created")
                        {
                            IsAccountCreated = uMDAO.IsUserCreated(user);
                        }*/
                    }


                }
                else
                {
                    return new BaseResponse("Username is not available.", false);
                }


            }
            catch (Exception)
            {
                throw new Exception();
            }

            return new BaseResponse("Account Creation Successful", true);
        }



    }
}
