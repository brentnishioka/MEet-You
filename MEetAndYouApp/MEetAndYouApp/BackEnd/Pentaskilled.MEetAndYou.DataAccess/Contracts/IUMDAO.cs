using Pentaskilled.MEetAndYou.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.DataAccess
{
    public interface IUMDAO
    {
        bool IsUserCreated(UserAccountEntity user);

        bool IsUserEmailUpdated(int id, string newEmail);

        bool IsUserPasswordUpdated(int id, string newPassword);

        bool IsUserPhoneUpdated(int id, string newPhoneNum);

        bool IsUserDeleted(int id);

        bool IsUserDisabled(int id);

        bool IsUserEnabled(int id);

        bool IsAdminCreated(AdminAccountEntity admin);

        bool IsAdminEmailUpdated(int id, string newEmail);

        bool IsAdminPasswordUpdated(int id, string newPassword);

        bool IsAdminDeleted(int id);

        bool IsUserInDBVerified(UserAccountEntity user);

        bool IsAdminInDBVerified(string email, string password);
    }
}
