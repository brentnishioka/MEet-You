using System.Collections.Generic;

namespace Pentaskilled.MEetAndYou.DataAccess.Contracts
{
    public interface IAuthorizationDAO
    {
        List<string> GetRoles(int userID);

        bool VerifyToken(int userID, string token);
    }
}
