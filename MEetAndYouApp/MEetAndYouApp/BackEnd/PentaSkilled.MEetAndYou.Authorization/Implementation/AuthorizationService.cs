using System.Security.Principal;
using PentaSkilled.MEetAndYou.Authorization.Contracts;

namespace PentaSkilled.MEetAndYou.Authorization.Implementation
{
    public class AuthorizationService : IAuthorizeService
    {
        // Might not need this
        public bool IsAuthenticated()
        {
            throw new NotImplementedException();
        }

        // Verify the user
        public bool IsAuthorized(IPrincipal principal, string role)
        {
            return principal.IsInRole(role);

        }

    }
}
