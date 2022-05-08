using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers.Contracts;

namespace Pentaskilled.MEetAndYou.Managers
{
    public class AccountDeletionManager : IAccountDeletionManager
    {
        private readonly IUMDAO _umDAO;
        private UserAccountEntity uAcc;

        public AccountDeletionManager(IUMDAO umDAO)
        {
            _umDAO = umDAO;
            uAcc = new UserAccountEntity();
        }

        public async Task<BaseResponse> DeleteUser(int userID)
        {
            // Validation check to ensure the user ID is a positive integer value.
            if (userID > 0)
            {
                try
                {
                    uAcc.UserID = userID;
                    BaseResponse result = await _umDAO.DeleteAccAsync(uAcc);
                    return result;
                }
                catch (Exception ex)
                {
                    return new BaseResponse("The user's account could not be deleted.", false);
                }
            }
            else
            {
                return new BaseResponse("The user ID provided was invalid.", false);
            }
        }
    }
}
