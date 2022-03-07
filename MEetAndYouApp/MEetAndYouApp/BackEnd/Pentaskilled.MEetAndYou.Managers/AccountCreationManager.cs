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
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string CheckAccountAvailability(string email, string password, string phoneNumber)
        {
            try
            {
                UserAccountEntity user = new UserAccountEntity();

                UMManager _UMManager = new UMManager();

                UMDAO _UMDAO = new UMDAO();

                user.Email = email;
                user.Password = password;
                user.PhoneNumber = phoneNumber;
                user.RegisterDate = DateTime.UtcNow.ToString();
                user.Active = Convert.ToInt32("0");

                if (_UMManager.VerifyUserInfo(email, password, phoneNumber) != "User info is successfully verified.")
                {
                    return _UMManager.VerifyUserInfo(email, password, phoneNumber);
                }

                if (!_UMDAO.IsUserInDBVerified(user))
                {
                    return "";
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

            return "Username is available";
        }

       /// <summary>
       /// Creates user based off account registration
       /// </summary>
       /// <param name="email">User given email</param>
       /// <param name="password">User generated password</param>
       /// <param name="phoneNumber">U</param>
       /// <returns>Successful account creation</returns>
       /// <exception cref="Exception"></exception>
        public string BeginAccountCreation(string email, string password, string phoneNumber)
        {

            try
            {
                UserAccountEntity user = new UserAccountEntity();
                IAuthnService _authnService = new AuthnService();
                UMManager _UMManager = new UMManager();
                AccountCreationDAO _accountCreation = new AccountCreationDAO;

                user.Email = email;
                user.Password = password;
                user.PhoneNumber = phoneNumber;
                user.RegisterDate = DateTime.UtcNow.ToString();
                user.Active = Convert.ToInt32("0");
                string accountCreated = "";
                string userOTP = _authnService.generateOTP();
               


                

                if(CheckAccountAvailability(email, password, phoneNumber) == "Username is available.")
                {
                    accountCreated =  UMManager.BeginCreateUser(user.Email, user.Password, user.PhoneNumber, user.RegisterDate, user.Active.ToString());
                }
                else
                {
                    return "Username is not available";
                }

                if (accountCreated == "User account was successfully created")
                {

                    _authnService.sendOTP(user.PhoneNumber, userOTP);

                    string OTP = Console.ReadLine();

                    _accountCreation.UpdateAccountActivity(user);
                    if (userOTP.Equals(OTP) && !_accountCreation.RemoveUnActivedAccount(user))
                    {
                        return "Registration successful";
                    }
                    else
                    {
                        _accountCreation.RemoveUnActivedAccount(user);
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


        public void RemoveInActiveAccount(UserAccountEntity user)
        {

        }
    }
}
