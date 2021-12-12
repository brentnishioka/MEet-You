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

        bool UpdateAccountEmail(UserAccountEntity ua, string newEmail);

        bool UpdateAccountPassword(UserAccountEntity ua);

        bool UpdateAccountPhone(UserAccountEntity ua);

        bool UpdateAccountRole(UserAccountEntity ua);

        bool DeleteAccountRecord(UserAccountEntity ua);

        bool DisableAccountRecord(UserAccountEntity ua);

        bool EnableAccountRecord(UserAccountEntity ua);

        bool VerifyUserInDB(UserAccountEntity ua);
    }
}
