using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities;


namespace Pentaskilled.MEetAndYou.Services
{
    public interface IUMService
    {
        bool isUserCreated(UserAccountEntity user);

        bool isUserEmailUpdated(int id, string newEmail);

        bool isUserPasswordUpdated(int id, string newPassword);

        bool isUserPhoneUpdated(int id, string newPhoneNum);

        bool isUserDeleted(int id);

        bool isUserDisabled(int id);

        bool isUserEnabled(int id);

        bool isAdminCreated(AdminAccountEntity admin);

        bool isAdminEmailUpdated(int id, string newEmail);

        bool isAdminPasswordUpdated(int id, string newPassword);

        bool isAdminDeleted(int id);

        bool isAdminInDBVerified(string email, string password);
    }
}
