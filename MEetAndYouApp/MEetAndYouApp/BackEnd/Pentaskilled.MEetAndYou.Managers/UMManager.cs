using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.Services;

namespace Pentaskilled.MEetAndYou.Managers
{
    public class UMManager
    {
        private IUMService _UMService;

        public UMManager()
        {
            _UMService = new UMService();
        }

        public string beginCreateProcess (string email, string password, string phoneNumber, DateTime registerDate, int active)
        {
            try
            {
                UserAccountEntity user = new UserAccountEntity();

                user.Email = email;
                user.Password = password;
                user.PhoneNumber = phoneNumber;
                user.RegisterDate = registerDate;
                user.Active = active;

                bool isUserSuccessfullyCreated = _UMService.CreateUserAccountRecord(user);

                if (!isUserSuccessfullyCreated)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

            return "User Account successfully created";
        }

/*        public bool verifyAdmin(string adminEmail, string adminPassword)
        {
            try
            {
                bool isAdminVerified = _UMService.VerifyAdminRecordInDB(adminEmail, adminPassword);
                if (!isAdminVerified)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return new Exception();
            }
            return true;
        }*/

/*        public bool verifyUserInfo(UserAccountEntity user)
        {
            try
            {
                return (verifyUserEmail(user.Email) && verifyUserPassword(user.Password) && verifyUserPhoneNum(user.PhoneNumber));
            }
            catch (Exception)
            {
                return new Exception();
            }
            
        }*/

/*        public bool verifyUserEmail(string email)
        {
            var validEmail = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return validEmail.IsMatch(email);
        }

        public bool verifyUserPassword(string password)
        {
            var hasMinimum8Char = new Regex(@".{8,}");
            var hasValidChars = new Regex(@"[A-Za-z0-9\s.,@!]{8,}");

            return (hasMinimum8Char.IsMatch(password) && hasValidChars.IsMatch(password)); 
        }

        public bool verifyUserPhoneNum(string phoneNum)
        {
            var validPhoneNum = new Regex(@"^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$");
            return validPhoneNum.IsMatch(phoneNum);
            
        }*/
    }
}