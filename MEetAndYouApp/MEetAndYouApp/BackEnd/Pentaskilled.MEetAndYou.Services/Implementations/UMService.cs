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

        public bool CreateUserAccountRecord(UserAccountEntity user)
        {
            try
            {
                return _UMDAO.CreateUserAccountRecord(user);
            }
            catch (Exception)
            {
                throw new Exception(); 
            }
        }

        public bool UpdateUserAccountEmail(int id, string newEmail)
        {
            try
            {
                return _UMDAO.UpdateUserAccountEmail(id, newEmail);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool UpdateUserAccountPassword(int id, string newPassword)
        {
            try
            {
                return _UMDAO.UpdateUserAccountPassword(id, newPassword);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool UpdateUserAccountPhone(int id, string newPhoneNum)
        {
            try
            {
                return _UMDAO.UpdateUserAccountPhone(id, newPhoneNum);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool DeleteUserAccountRecord(int id)
        {
            try
            {
                return _UMDAO.DeleteUserAccountRecord(id);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public bool DisableUserAccountRecord(int id)
        {
            try
            {
                return _UMDAO.DisableUserAccountRecord(id);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool EnableUserAccountRecord(int id)
        {
            try
            {
                return _UMDAO.EnableUserAccountRecord(id);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool CreateAdminAccountRecord(AdminAccountEntity admin)
        {
            try
            {
                return _UMDAO.CreateAdminAccountRecord(admin);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool UpdateAdminAccountEmail(int id, string newEmail)
        {
            try
            {
                return _UMDAO.UpdateAdminAccountEmail(id, newEmail);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool UpdateAdminAccountPassword(int id, string newPassword)
        {
            try
            {
                return _UMDAO.UpdateAdminAccountPassword(id, newPassword);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool DeleteAdminAccountRecord(int id)
        {
            try
            {
                return _UMDAO.DeleteAdminAccountRecord(id);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool VerifyAdminRecordInDB(string email, string password)
        {
            try
            {
                return _UMDAO.VerifyAdminRecordInDB(email, password);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
