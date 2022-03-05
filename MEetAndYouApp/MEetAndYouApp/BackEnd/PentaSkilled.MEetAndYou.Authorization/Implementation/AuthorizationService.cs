using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using PentaSkilled.MEetAndYou.Authorization.Contracts;
using PentaSkilled.MEetAndYou.Authorization.Model;

namespace PentaSkilled.MEetAndYou.Authorization.Implmentation
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
