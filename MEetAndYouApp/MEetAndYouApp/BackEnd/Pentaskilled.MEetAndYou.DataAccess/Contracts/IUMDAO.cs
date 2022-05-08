using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities;

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

        Task<bool> DeleteAcc(UserAccountEntity uAcc);

        bool IsUserEnabled(int id);

        bool IsAdminCreated(AdminAccountEntity admin);

        bool IsAdminEmailUpdated(int id, string newEmail);

        bool IsAdminPasswordUpdated(int id, string newPassword);

        bool IsAdminDeleted(int id);

        bool IsUserInDBVerified(UserAccountEntity user);

        bool IsAdminInDBVerified(string email, string password);

        Task<int> GetUserIDByEmail(string userEmail);
    }
}
