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
        bool isUserAccountCreated(UserAccountEntity user);

        bool isUserAccountEmailUpdated(int id, string newEmail);

        bool isUserAccountPasswordUpdated(int id, string newPassword);

        bool isUserAccountPhoneUpdated(int id, string newPhoneNum);

        bool isUserAccountDeleted(int id);

        bool isUserAccountDisabled(int id);

        bool isUserAccountEnabled(int id);

        bool isAdminAccountCreated(AdminAccountEntity admin);

        bool isAdminEmailUpdated(int id, string newEmail);

        bool isAdminPasswordUpdated(int id, string newPassword);

        bool isAdminDeleted(int id);

        bool isUserInDBVerified(UserAccountEntity user);

        bool isAdminInDBVerified(string email, string password);
    }
}
