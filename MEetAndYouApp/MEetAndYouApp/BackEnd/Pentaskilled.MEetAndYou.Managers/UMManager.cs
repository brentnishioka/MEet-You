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

        public string BeginCreateUser(string email, string password, string phoneNumber, string registerDate, int active)
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

                if (!_UMService.isUserCreated(user))
                {
                    return "User account was not successfully created";
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

            return "User account was successfully created";
        }

        public string BeginUpdateUserEmail(int id, string email)
        {
            try
            {
                if (!IsEmailVerified(email))
                {
                    return "Invalid email";
                }

                if (!_UMService.isUserEmailUpdated(id, email))
                {
                    return "User email was not successfully updated";
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

            return "User email was successfully updated";
        }

        public string BeginUpdateUserPassword(int id, string password)
        {
            try
            {
                if (!IsPasswordVerified(password))
                {
                    return "Invalid password";
                }

                if (!_UMService.isUserPasswordUpdated(id, password))
                {
                    return "User email was not successfully updated";
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

            return "User password was successfully updated";
        }

        public string BeginUpdateUserPhone(int id, string phoneNum)
        {
            try
            {
                if (!IsPhoneNumVerified(phoneNum))
                {
                    return "Invalid phone number";
                }

                if (!_UMService.isUserPhoneUpdated(id, phoneNum))
                {
                    return "User phone number was not successfully updated";
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

            return "User phone number was successfully updated"; 
        }

        public string BeginDeleteUserAccount(int id)
        {
            try
            {
                if (!_UMService.isUserDeleted(id))
                {
                    return "User account was not successfully deleted";
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

            return "User account was successfully deleted";
        }

        public string BeginDisableUserAccount(int id)
        {
            try
            {
                if (!_UMService.isUserDisabled(id))
                {
                    return "User account was not successfully disabled";
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

            return "User account was successfully disabled";
        }

        public string BeginEnableUserAccount(int id)
        {
            try
            {
                if (!_UMService.isUserEnabled(id))
                {
                    return "User account was not successfully enabled";
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

            return "User account was successfully enabled";
        }

        public string BeginCreateAdmin(string email, string password)
        {
            try
            {
                AdminAccountEntity admin = new AdminAccountEntity();

                admin.Email = email;
                admin.Password = password;

                if (!IsEmailVerified(email))
                {
                    return "Invalid email.";
                }

                if (!(IsPasswordVerified(password)))
                {
                    return "Invalid password.";
                }

                if (!_UMService.isAdminCreated(admin))
                {
                    return "Admin account was not successfully created";
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
            return "Admin account was successfully created";
        }

        public string BeginUpdateAdminEmail(int id, string email)
        {
            try
            {
                if (!IsEmailVerified(email))
                {
                    return "Invalid email";
                }
                if (!_UMService.isAdminEmailUpdated(id, email))
                {
                    return "Admin email was not successfully updated";
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

            return "Admin email was successfully updated";
        }

        public string BeginUpdateAdminPassword(int id, string password)
        {
            try
            {
                if (!IsPasswordVerified(password))
                {
                    return "Invalid password";
                }

                if (!_UMService.isAdminEmailUpdated(id, password))
                {
                    return "Admin email was not successfully updated";
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

            return "Admin password was successfully updated";
        }

        public string BeginDeleteAdminAccount(int id)
        {
            try
            {
                if (!_UMService.isAdminDeleted(id))
                {
                    return "Admin account was not successfully deleted";
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

            return "Admin account was successfully deleted";
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
                if (IsEmailVerified(email) && IsPasswordVerified(password) && IsPhoneNumVerified(phoneNumber))
                {
                    return "User info is successfully verified.";
                }
                else
                {
                    if (!IsEmailVerified(email))
                    {
                        invalidParameters += "email  ";
                    }
                    if (!IsPasswordVerified(password))
                    {
                        invalidParameters += "password  ";
                    }
                    if (!IsPhoneNumVerified(phoneNumber))
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

        public bool IsEmailVerified(string email)
        {
            var validEmail = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return validEmail.IsMatch(email);
        }

        public bool IsPasswordVerified(string password)
        {
            var hasMinimum8Char = new Regex(@".{8,}");
            var hasValidChars = new Regex(@"[A-Za-z0-9\s.,@!]");

            return (hasMinimum8Char.IsMatch(password) && hasValidChars.IsMatch(password));
        }

        public bool IsPhoneNumVerified(string phoneNum)
        {
            var validPhoneNum = new Regex(@"^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$");
            return validPhoneNum.IsMatch(phoneNum);
        }
    }
}