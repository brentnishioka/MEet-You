using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Services.Contracts;

namespace Pentaskilled.MEetAndYou.Managers
{
    public class AccountCreationManager
    {
        private IUMService _UMService;

        public string CheckAccountAvailability(string email, string password, string phoneNumber)
        {
            try
            {
                UserAccountEntity user = new UserAccountEntity();

                UMManager _UMManager = new UMManager();

                user.Email = email;
                user.Password = password;
                user.PhoneNumber = phoneNumber;
                user.RegisterDate = DateTime.UtcNow.ToString();
                user.Active = Convert.ToInt32("0");

                if (_UMManager.VerifyUserInfo(email, password, phoneNumber) != "User info is successfully verified.")
                {
                    return _UMManager.VerifyUserInfo(email, password, phoneNumber);
                }

                if (!_UMManager. IsUserInDBVerified(user))
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

        public static string BeginAccountCreation(string email, string password, string phoneNumber)
        {

            try
            {
                UserAccountEntity user = new UserAccountEntity();

                UMManager _UMManager = new UMManager();
                AuthnManager _authnService = new AuthnManager();

                user.Email = email;
                user.Password = password;
                user.PhoneNumber = phoneNumber;
                user.RegisterDate = DateTime.UtcNow.ToString();
                user.Active = Convert.ToInt32("0");
                string accountCreated = "";
                string OTP = _authnService.generateOTP();


                Console.WriteLine(otp);
                _authnService.sendOTP(phoneNum, otp)

                if(_UMManager.CheckAccountAvailability(email, password, phoneNumber) == "Username is available.")
                {
                    accountCreated = _UMManager.BeginCreateUser(user.Email, user.Password, user.PhoneNumber, user.RegisterDate, user.Active.ToString());
                }
                else
                {
                    return "Username is not available";
                }

                if (accountCreated == "User account was successfully created")
                {
                    string otp = _authnService.generateOTP();
                    Console.WriteLine(otp);
                    _authnService.sendOTP(phoneNum, otp)
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

            return "Account Creation Successful";
        }

        public void ConfirmOTP(UserAccountEntity user, string OTP)
        {
            try
            {

            }
            catch (Exception)
            {

            }
        }
    }
}
