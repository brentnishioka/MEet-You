using System.Security.Principal;

namespace PentaSkilled.MEetAndYou.Authorization.Contracts
{
    public interface IAuthorizeService
    {
        public bool IsAuthorized(IPrincipal principal, string role);
        public bool IsAuthenticated();
    }
}
