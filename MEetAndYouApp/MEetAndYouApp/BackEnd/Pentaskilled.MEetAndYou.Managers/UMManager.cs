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

        public bool verifyAdmin(string adminEmail, string adminPassword)
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
                return false;
            }
            return true;
        }

        public bool verifyUserInfo(UserAccountEntity user)
        {
            
        }

        public bool verifyUserEmail(string email)
        {
           
        }

        public bool verifyUserPassword(string password)
        {
            
        }
    }
}