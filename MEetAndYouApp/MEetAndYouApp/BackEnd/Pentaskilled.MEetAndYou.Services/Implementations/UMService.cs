using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities;

namespace Pentaskilled.MEetAndYou.Services
{
    public class UMService : IUMService
    {
        private IUMDAO _UMDAO;

        public UMService()
        {
            _UMDAO = new UMDAO();
        }

        public string CreateUserAccountRecord(UserAccountEntity user)
        {
            try
            {
                _UMDAO.CreateUserAccountRecord(user);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "User Account successfully created";
        }

        public string UpdateUserAccountEmail(int id, string newEmail)
        {
            try
            {
                _UMDAO.UpdateUserAccountEmail(id, newEmail);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "User Account successfully updated";
        }

        public string UpdateUserAccountPassword(int id, string newPassword)
        {
            try
            {
                _UMDAO.UpdateUserAccountPassword(id, newPassword);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "User Account successfully updated";
        }

        public string UpdateUserAccountPhone(int id, string newPhoneNum)
        {
            try
            {
                _UMDAO.UpdateUserAccountPhone(id, newPhoneNum);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "User Account successfully updated";
        }

        public string DeleteUserAccountRecord(int id)
        {
            try
            {
                _UMDAO.DeleteUserAccountRecord(id);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "User Account successfully deleted";
        }

        public string DisableUserAccountRecord(int id)
        {
            try
            {
                _UMDAO.DisableUserAccountRecord(id);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "User Account successfully disabled";
        }

        public string EnableUserAccountRecord(int id)
        {
            try
            {
                _UMDAO.EnableUserAccountRecord(id);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "User Account successfully enabled";
        }

        public string CreateAdminAccountRecord(AdminAccountEntity admin)
        {
            try
            {
                _UMDAO.CreateAdminAccountRecord(admin);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Admin Account successfully created";
        }

        public string UpdateAdminAccountEmail(int id, string newEmail)
        {
            try
            {
                _UMDAO.UpdateAdminAccountEmail(id, newEmail);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Admin Account successfully updated";
        }

        public string UpdateAdminAccountPassword(int id, string newPassword)
        {
            try
            {
                _UMDAO.UpdateAdminAccountPassword(id, newPassword);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Admin Account successfully updated";
        }

        public string DeleteAdminAccountRecord(int id)
        {
            try
            {
                _UMDAO.DeleteAdminAccountRecord(id);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Admin Account successfully deleted";
        }
    }
}
