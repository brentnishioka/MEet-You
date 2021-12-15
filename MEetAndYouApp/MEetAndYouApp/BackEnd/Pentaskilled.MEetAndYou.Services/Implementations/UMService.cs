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

        public bool isUserAccountCreated(UserAccountEntity user)
        {
            try
            {
                return _UMDAO.isUserAccountCreated(user);
            }
            catch (Exception)
            {
                throw new Exception(); 
            }
        }

        public bool isUserAccountEmailUpdated(int id, string newEmail)
        {
            try
            {
                return _UMDAO.isUserAccountEmailUpdated(id, newEmail);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool isUserAccountPasswordUpdated(int id, string newPassword)
        {
            try
            {
                return _UMDAO.isUserAccountPasswordUpdated(id, newPassword);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool isUserAccountPhoneUpdated(int id, string newPhoneNum)
        {
            try
            {
                return _UMDAO.isUserAccountPhoneUpdated(id, newPhoneNum);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool isUserAccountDeleted(int id)
        {
            try
            {
                return _UMDAO.isUserAccountDeleted(id);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public bool isUserAccountDisabled(int id)
        {
            try
            {
                return _UMDAO.isUserAccountDisabled(id);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool isUserAccountEnabled(int id)
        {
            try
            {
                return _UMDAO.isUserAccountEnabled(id);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool isAdminAccountCreated(AdminAccountEntity admin)
        {
            try
            {
                return _UMDAO.isAdminAccountCreated(admin);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool isAdminEmailUpdated(int id, string newEmail)
        {
            try
            {
                return _UMDAO.isAdminEmailUpdated(id, newEmail);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool isAdminPasswordUpdated(int id, string newPassword)
        {
            try
            {
                return _UMDAO.isAdminPasswordUpdated(id, newPassword);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool isAdminDeleted(int id)
        {
            try
            {
                return _UMDAO.isAdminDeleted(id);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool isAdminInDBVerified(string email, string password)
        {
            try
            {
                return _UMDAO.isAdminInDBVerified(email, password);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
