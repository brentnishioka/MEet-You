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
        bool CreateAccountRecord(UserAccountEntity ua);

        bool UpdateAccountEmail(int id, string newEmail);

        bool UpdateAccountPassword(int id, string newPassword);

        bool UpdateAccountPhone(int id, string newPhoneNum);

        bool UpdateAccountRole(int id, string newRole);

        bool DeleteAccountRecord(int id);

        bool DisableAccountRecord(int id);

        bool EnableAccountRecord(int id);

        bool VerifyUserInDB(int id);
    }
}
