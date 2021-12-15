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
        bool CreateUserAccountRecord(UserAccountEntity user);

        bool UpdateUserAccountEmail(int id, string newEmail);

        bool UpdateUserAccountPassword(int id, string newPassword);

        bool UpdateUserAccountPhone(int id, string newPhoneNum);

        bool DeleteUserAccountRecord(int id);

        bool DisableUserAccountRecord(int id);

        bool EnableUserAccountRecord(int id);

        bool CreateAdminAccountRecord(AdminAccountEntity admin);

        bool UpdateAdminAccountEmail(int id, string newEmail);

        bool UpdateAdminAccountPassword(int id, string newPassword);

        bool DeleteAdminAccountRecord(int id);

        bool VerifyAdminRecordInDB(string email, string password);
    }
}
