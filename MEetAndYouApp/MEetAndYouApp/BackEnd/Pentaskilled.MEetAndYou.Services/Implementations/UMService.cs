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

        public bool isUserCreated(UserAccountEntity user)
        {
            try
            {
                return _UMDAO.isUserCreated(user);
            }
            catch (Exception)
            {
                throw new Exception(); 
            }
        }

        public bool isUserEmailUpdated(int id, string newEmail)
        {
            try
            {
                return _UMDAO.isUserEmailUpdated(id, newEmail);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool isUserPasswordUpdated(int id, string newPassword)
        {
            try
            {
                return _UMDAO.isUserPasswordUpdated(id, newPassword);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool isUserPhoneUpdated(int id, string newPhoneNum)
        {
            try
            {
                return _UMDAO.isUserPhoneUpdated(id, newPhoneNum);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool isUserDeleted(int id)
        {
            try
            {
                return _UMDAO.isUserDeleted(id);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public bool isUserDisabled(int id)
        {
            try
            {
                return _UMDAO.isUserDisabled(id);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool isUserEnabled(int id)
        {
            try
            {
                return _UMDAO.isUserEnabled(id);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool isAdminCreated(AdminAccountEntity admin)
        {
            try
            {
                return _UMDAO.isAdminCreated(admin);
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
