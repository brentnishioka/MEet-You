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
        string CreateUserAccountRecord(UserAccountEntity user);

        string UpdateUserAccountEmail(int id, string newEmail);

        string UpdateUserAccountPassword(int id, string newPassword);

        string UpdateUserAccountPhone(int id, string newPhoneNum);

        string DeleteUserAccountRecord(int id);

        string DisableUserAccountRecord(int id);

        string EnableUserAccountRecord(int id);

        string CreateAdminAccountRecord(AdminAccountEntity admin);

        string UpdateAdminAccountEmail(int id, string newEmail);

        string UpdateAdminAccountPassword(int id, string newPassword);

        string DeleteAdminAccountRecord(int id);
    }
}
