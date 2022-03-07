using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities;

namespace Pentaskilled.MEetAndYou.DataAccess.Contracts
{
    public interface IAccountCreation
    {
        bool DoesEmailExist(UserAccountEntity user);
        bool UpdateAccountActivity(UserAccountEntity user);

        bool RemoveUnActivedAccount(UserAccountEntity user);
    }
}
