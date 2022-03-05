using System.Security.Principal;

namespace PentaSkilled.MEetAndYou.Authorization.Model
{
    public class UserPrincipal : IPrincipal
    {
        private IIdentity? _identity;
        private string[]? _roles;
        public IIdentity? Identity { get; set; }
        public string[]? Roles { get; set; }

        //Constructor
        public UserPrincipal(UserIdentity userIdentity)
        {
            Identity = userIdentity;
        }

        public UserPrincipal(UserIdentity userIdentity, string[] roleList)
        {
            Identity = userIdentity;
            Roles = roleList;
        }


        public bool IsInRole(string role)
        {
            if (role == null || Roles == null)
            {
                return false;
            }
            foreach (string r in Roles)
            {
                if (r == role)
                {
                    return true;
                }
            }
            return false;

        }
    }
}
