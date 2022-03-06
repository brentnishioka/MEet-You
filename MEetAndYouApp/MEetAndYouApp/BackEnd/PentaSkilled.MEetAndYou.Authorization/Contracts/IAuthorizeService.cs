using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PentaSkilled.MEetAndYou.Authorization.Contracts
{
    public interface IAuthorizeService
    {
        public bool IsAuthorized(IPrincipal principal, string role);
        public bool IsAuthenticated();
    }
}
