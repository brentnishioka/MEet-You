using System.Security.Principal;

namespace PentaSkilled.MEetAndYou.Authorization.Model
{
    public class UserIdentity : IIdentity
    {

        public string? AuthenticationType {
            get;
        }

        public bool IsAuthenticated { get; }

        public string? Name { get; }

        public UserIdentity()
        {
            Name = "Guest";
            IsAuthenticated = false;
        }

        public UserIdentity(string name)
        {
            Name = name;
            IsAuthenticated = false;
            AuthenticationType = "None";
        }

        public UserIdentity(string name, string authType)
        {
            Name = name;
            AuthenticationType = authType;
            IsAuthenticated = false;
        }

        public UserIdentity(string name, bool isAuthenticated)
        {
            Name = name;
            IsAuthenticated = isAuthenticated;
        }

    }
}
