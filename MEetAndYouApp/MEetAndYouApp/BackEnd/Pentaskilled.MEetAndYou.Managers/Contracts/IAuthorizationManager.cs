

namespace Pentaskilled.MEetAndYou.Managers
{
    public interface IAuthorizationManager
    {
        bool IsAuthorized(int userID, string userToken, string role);
    }
}
