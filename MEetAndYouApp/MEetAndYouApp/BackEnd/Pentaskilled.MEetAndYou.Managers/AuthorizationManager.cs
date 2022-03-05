using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Security.Permissions;
using System.Threading;

namespace Pentaskilled.MEetAndYou.Managers
{
    public class AuthorizationManager
    {
        //public static void main()
        //{
        //    // Create generic identity and generic principal objects
        //    System.Security.Principal.GenericIdentity gid = new GenericIdentity("yazan", "developer");
        //    string[] roles = { "developer", "manager" };
        //    System.Security.Principal.GenericPrincipal gpn = new GenericPrincipal(gid, roles);

        //    // Now make this new generic principal the current one
        //    Thread.CurrentPrincipal = gpn;

        //    // Now call a method that needs to make checks based on the current principal (which, in this
        //    // case is 'yazan' in the role of 'developer')
        //    PrintHello();
        //}

        //[PrincipalPermission(SecurityAction.Demand, Role = "SupremeCommander")]
        //public static void PrintHello()
        //{
        //    Console.WriteLine("Hello World");
        //}
    }
}
