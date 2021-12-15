using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.Services;
using System.Text.RegularExpressions;

namespace Pentaskilled.MEetAndYou.Managers
{
    public class UMManager
    {
        private IUMService _UMService;

        public UMManager()
        {
            _UMService = new UMService();
        }

        public string BeginCreateUserProcess(string email, string password, string phoneNumber, string registerDate, int active)
        {
            try
            {
                UserAccountEntity user = new UserAccountEntity();

                user.Email = email;
                user.Password = password;
                user.PhoneNumber = phoneNumber;
                user.RegisterDate = registerDate;
                user.Active = active;

                if (VerifyUserInfo(email, password, phoneNumber) != "User info is successfully verified.")
                {
                    return VerifyUserInfo(email, password, phoneNumber);
                }

                if (!_UMService.isUserAccountCreated(user))
                {
                    return "User Account was not successfully created";
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

            return "User Account was successfully created";
        }

        public string BeginUpdateUserEmailProcess(int id, string email)
        {
            try
            {
                if (!IsUserEmailVerified(email))
                {
                    return "Invalid email";
                }

                if (!_UMService.isUserAccountEmailUpdated(id, email))
                {
                    return "User Account was not successfully created";
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

            return "User Account was successfully created";
        }

        public bool IsAdminVerified(string adminEmail, string adminPassword)
        {
            try
            {
                bool isAdminVerified = _UMService.isAdminInDBVerified(adminEmail, adminPassword);
                if (!isAdminVerified)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
            return true;
        }

        public string VerifyUserInfo(string email, string password, string phoneNumber)
        {
            try
            {
                string invalidParameters = "Invalid parameter(s): ";
                if (IsUserEmailVerified(email) && IsUserPasswordVerified(password) && IsUserPhoneNumVerified(phoneNumber))
                {
                    return "User info is successfully verified.";
                }
                else
                {
                    if (!IsUserEmailVerified(email))
                    {
                        invalidParameters += "email  ";
                    }
                    if (!IsUserPasswordVerified(password))
                    {
                        invalidParameters += "password  ";
                    }
                    if (!IsUserPhoneNumVerified(phoneNumber))
                    {
                        invalidParameters += "phone number  ";
                    }
                    return invalidParameters;
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool IsUserEmailVerified(string email)
        {
            var validEmail = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return validEmail.IsMatch(email);
        }

        public bool IsUserPasswordVerified(string password)
        {
            var hasMinimum8Char = new Regex(@".{8,}");
            var hasValidChars = new Regex(@"[A-Za-z0-9\s.,@!]");

            return (hasMinimum8Char.IsMatch(password) && hasValidChars.IsMatch(password));
        }

        public bool IsUserPhoneNumVerified(string phoneNum)
        {
            var validPhoneNum = new Regex(@"^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$");
            return validPhoneNum.IsMatch(phoneNum);
        }
    }
}