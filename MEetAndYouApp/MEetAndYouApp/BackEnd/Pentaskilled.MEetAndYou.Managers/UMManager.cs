using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.Services.Implementations;
using Pentaskilled.MEetAndYou.Services.Contracts;
using System.Text.RegularExpressions;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace Pentaskilled.MEetAndYou.Managers
{
    public class UMManager
    {
        private IUMService _UMService;

        public UMManager()
        {
            _UMService = new UMService();
        }

        public static string BeginCreateUser(string email, string password, string phoneNumber, string registerDate, string active)
        {
            try
            {
                UserAccountEntity user = new UserAccountEntity();

                UMManager _UMManager = new UMManager();

                user.Email = email;
                user.Password = password;
                user.PhoneNumber = phoneNumber;
                user.RegisterDate = registerDate;
                user.Active = Convert.ToInt32(active);

                if (_UMManager.VerifyUserInfo(email, password, phoneNumber) != "User info is successfully verified.")
                {
                    return _UMManager.VerifyUserInfo(email, password, phoneNumber);
                }

                if (!_UMManager._UMService.IsUserCreated(user))
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

        public string CheckAccountAvailaibilty(string email, string password, string phoneNumber)
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

                if (!_UMManager._UMService.IsUserInDBVerified(user))
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

                user.Email = email;
                user.Password = password;
                user.PhoneNumber = phoneNumber;
                user.RegisterDate = DateTime.UtcNow.ToString();
                user.Active = Convert.ToInt32("0");
                string accountCreated = "";

                if (_UMManager.CheckAccountAvailaibilty(email, password, phoneNumber) == "Username is available.")
                {
                    accountCreated = _UMManager.BeginCreateUser(user.Email, user.Password, user.PhoneNumber, user.RegisterDate, user.Active.ToString());
                }
                else
                {
                    return "Username is not available";
                }

                if()
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

            }catch (Exception)
            {

            }
        }
        public string BeginUpdateUserEmail(string id, string email)
        {
            try
            {
                if (!IsEmailVerified(email))
                {
                    return "Invalid email";
                }

                if (!_UMService.IsUserEmailUpdated(Convert.ToInt32(id), email))
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

        public string BeginUpdateUserPassword(string id, string password)
        {
            try
            {
                if (!IsPasswordVerified(password))
                {
                    return "Invalid password";
                }

                if (!_UMService.IsUserPasswordUpdated(Convert.ToInt32(id), password))
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

        public string BeginUpdateUserPhone(string id, string phoneNum)
        {
            try
            {
                if (!IsPhoneNumVerified(phoneNum))
                {
                    return "Invalid phone number";
                }

                if (!_UMService.IsUserPhoneUpdated(Convert.ToInt32(id), phoneNum))
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

        public string BeginDeleteUserAccount(string id)
        {
            try
            {
                if (!_UMService.IsUserDeleted(Convert.ToInt32(id)))
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

        public string BeginDisableUserAccount(string id)
        {
            try
            {
                if (!_UMService.IsUserDisabled(Convert.ToInt32(id)))
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

        public string BeginEnableUserAccount(string id)
        {
            try
            {
                if (!_UMService.IsUserEnabled(Convert.ToInt32(id)))
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

                if (!_UMService.IsAdminCreated(admin))
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

        public string BeginUpdateAdminEmail(string id, string email)
        {
            try
            {
                if (!IsEmailVerified(email))
                {
                    return "Invalid email";
                }
                if (!_UMService.IsAdminEmailUpdated(Convert.ToInt32(id), email))
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

        public string BeginUpdateAdminPassword(string id, string password)
        {
            try
            {
                if (!IsPasswordVerified(password))
                {
                    return "Invalid password";
                }

                if (!_UMService.IsAdminEmailUpdated(Convert.ToInt32(id), password))
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

        public string BeginDeleteAdminAccount(string id)
        {
            try
            {
                if (!_UMService.IsAdminDeleted(Convert.ToInt32(id)))
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
                bool isAdminVerified = _UMService.IsAdminInDBVerified(adminEmail, adminPassword);
                if (!isAdminVerified)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error: Unauthorized user");
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

        public string BulkOperation(string filePath, string extractedFilePath)
        {
            ZipFile.ExtractToDirectory(filePath, extractedFilePath);
            string path = extractedFilePath + "/request.txt";

            if (!IsFileValid(path)) //checks if file is valid before proceeding w/ bulk operation
            {
                return "File is not valid. It either too big or has too many lines.";
            }

            foreach (string line in File.ReadLines(path))
            {
                string[] array = line.Split(','); //Splitting line into necessary components
                string method = array[0];
                string parenthesis = array[1];
                string parameters = parenthesis.Split('(', ')')[1];
                object[] splitParams = parameters.Split('|');

                Type type = typeof(UMManager); //Dynamically calling functions 
                MethodInfo methodInfo = type.GetMethod(method);
                methodInfo.Invoke(method, splitParams);
            }
            return "Request successful";
        }

        public bool IsFileValid(string filePath)
        {
            long length = new FileInfo(filePath).Length; //checks size of file 
            if (length > 2147483648) //2gb = 2147483648 bytes 
            {
                return false;
            }

            int lineCount = 0;
            using (var reader = File.OpenText(filePath)) //checks if the file has > 10k lines 
            {
                while (reader.ReadLine() != null)
                {
                    lineCount++;
                }
                if (lineCount > 10000)
                {
                    return false;
                }
            }
            return true;
        }
    }
}