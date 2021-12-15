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

        public string BeginCreateUserAccountProcess (string email, string password, string phoneNumber, string registerDate, int active)
        {
            try
            {
                UserAccountEntity user = new UserAccountEntity();

                user.Email = email;
                user.Password = password;
                user.PhoneNumber = phoneNumber;
                user.RegisterDate = registerDate;
                user.Active = active;

                bool isUserSuccessfullyCreated = _UMService.isUserAccountCreated(user);

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

        public string BeginUpdateUserAccountEmailProcess(string email)
        {
            try
            {
                UserAccountEntity user = new UserAccountEntity();

                user.Email = email;
                user.Password = password;
                user.PhoneNumber = phoneNumber;
                user.RegisterDate = registerDate;
                user.Active = active;

                bool isUserSuccessfullyCreated = _UMService.isUserAccountCreated(user);

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

        public bool VerifyAdmin(string adminEmail, string adminPassword)
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

        public bool VerifyUserInfo(string email, string password, string phoneBn)
        {
            try
            {
                return (VerifyUserEmail(user.Email) && VerifyUserPassword(user.Password) && VerifyUserPhoneNum(user.PhoneNumber));
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool VerifyUserEmail(string email)
        {
            var validEmail = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return validEmail.IsMatch(email);
        }

        public bool VerifyUserPassword(string password)
        {
            var hasMinimum8Char = new Regex(@".{8,}");
            var hasValidChars = new Regex(@"[A-Za-z0-9\s.,@!]");

            return (hasMinimum8Char.IsMatch(password) && hasValidChars.IsMatch(password)); 
        }

        public bool VerifyUserPhoneNum(string phoneNum)
        {
            var validPhoneNum = new Regex(@"^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$");
            return validPhoneNum.IsMatch(phoneNum);
        }
    }
}