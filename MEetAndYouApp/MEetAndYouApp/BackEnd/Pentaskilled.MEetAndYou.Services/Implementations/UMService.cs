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

        public bool IsUserCreated(UserAccountEntity user)
        {
            try
            {
                return _UMDAO.IsUserCreated(user);
            }
            catch (Exception)
            {
                throw new Exception(); 
            }
        }

        public bool IsUserEmailUpdated(int id, string newEmail)
        {
            try
            {
                return _UMDAO.IsUserEmailUpdated(id, newEmail);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool IsUserPasswordUpdated(int id, string newPassword)
        {
            try
            {
                return _UMDAO.IsUserPasswordUpdated(id, newPassword);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool IsUserPhoneUpdated(int id, string newPhoneNum)
        {
            try
            {
                return _UMDAO.IsUserPhoneUpdated(id, newPhoneNum);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool IsUserDeleted(int id)
        {
            try
            {
                return _UMDAO.IsUserDeleted(id);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public bool IsUserDisabled(int id)
        {
            try
            {
                return _UMDAO.IsUserDisabled(id);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool IsUserEnabled(int id)
        {
            try
            {
                return _UMDAO.IsUserEnabled(id);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool IsAdminCreated(AdminAccountEntity admin)
        {
            try
            {
                return _UMDAO.IsAdminCreated(admin);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool IsAdminEmailUpdated(int id, string newEmail)
        {
            try
            {
                return _UMDAO.IsAdminEmailUpdated(id, newEmail);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool IsAdminPasswordUpdated(int id, string newPassword)
        {
            try
            {
                return _UMDAO.IsAdminPasswordUpdated(id, newPassword);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool IsAdminDeleted(int id)
        {
            try
            {
                return _UMDAO.IsAdminDeleted(id);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public bool IsAdminInDBVerified(string email, string password)
        {
            try
            {
                return _UMDAO.IsAdminInDBVerified(email, password);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
