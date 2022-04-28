using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities;

namespace Pentaskilled.MEetAndYou.DataAccess.Contracts
{
    public interface IAccountCreation
    {
        Task<bool> DoesEmailExist(UserAccountEntity user);
        Task<bool> UpdateAccountActivity(UserAccountEntity user);

        Task<bool> RemoveUnActivatedAccount(UserAccountEntity user);
    }
}
