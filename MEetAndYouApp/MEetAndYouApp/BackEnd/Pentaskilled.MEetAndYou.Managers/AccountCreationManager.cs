using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Services.Contracts;
using Pentaskilled.MEetAndYou.Services.Implementations;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;

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
        public string CheckAccountAvailability(string email, string password, string phoneNumber)
        {
            try
            {
                UserAccountEntity user = new UserAccountEntity();

                UMManager _UMManager = new UMManager();

                AccountCreationDAO ACManager =  new AccountCreationDAO();   

                user.Email = email;
                user.Password = password;
                user.PhoneNumber = phoneNumber;
                user.RegisterDate = DateTime.UtcNow.ToString();
                user.Active = Convert.ToInt32("0");

                if (_UMManager.VerifyUserInfo(user.Email, user.Password, user.PhoneNumber) == "User info is successfully verified.")
                {
                    if (!ACManager.DoesEmailExist(user).Result)
                    {
                        return "Username is available.";
                    }

                }
             

               
            }
            catch (Exception)
            {
                throw new Exception();
            }
            return "Username is not available.";
        }

       /// <summary>
       /// Creates user based off account registration
       /// </summary>
       /// <param name="email">User given email</param>
       /// <param name="password">User generated password</param>
       /// <param name="phoneNumber">User given phone number</param>
       /// <returns>Successful account creation</returns>
       /// <exception cref="Exception"></exception>
        public string BeginAccountCreation(string email, string password, string phoneNumber)
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
                string userOTP = _authnService.generateOTP();

                bool isUnActivated = false;
                string result = "";

                bool IsAccountCreated = false;

                if (CheckAccountAvailability(email, password, phoneNumber) == "Username is available.")
                {
                    accountCreated = UMManager.BeginCreateUser(email, password, phoneNumber, user.RegisterDate, user.Active.ToString());

                    if (accountCreated != "User account was successfully created")
                    {
                        IsAccountCreated = uMDAO.IsUserCreated(user);
                    }
                }
                else if (CheckAccountAvailability(email, password, phoneNumber) == "Username is not available.")
                {
                    return "Username not available";

                }

                //_UMManager._UMService.IsUserCreated(user);
                if (IsAccountCreated)
                {
                        
                    _authnService.sendOTP(user.PhoneNumber, userOTP);

                    string OTP = Console.ReadLine();

                    _accountCreation.UpdateAccountActivity(user);
                    if (userOTP.Equals(OTP)) ///&& !(isUnActivated = _accountCreation.RemoveUnActivatedAccount(user).Result))
                    {
                        return "Registration successful";
                    }
                    else
                    {
                        _accountCreation.RemoveUnActivatedAccount(user);
                        return "Account creation timed out. Please try again";
                    }
                }

              
            }
            catch (Exception)
            {
                throw new Exception();
            }

            return "Account Creation Successful";
        }


        
    }
}
