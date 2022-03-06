using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess;
using Xunit;
using Xunit.Abstractions;

namespace Pentaskilled.MEetAndYou.XUnitTests
{
     public class AuthorizationTests2
    {
        private readonly ITestOutputHelper _output;
        public AuthorizationTests2(ITestOutputHelper output)
        {
            this._output = output;
        }

        [Fact]
        public int VerifyCredentialsInDBTest()
        {
            // Create generic identity.
            GenericIdentity myIdentity = new GenericIdentity("JoshuaRamos");

            // Create generic principal.
            String[] myStringArray = { "Manager", "Teller" };
            String[] myStringArray2 = {"Teller" };
            GenericPrincipal myPrincipal =
                new GenericPrincipal(myIdentity, myStringArray);

            // Attach the principal to the current thread.
            // This is not required unless repeated validation must occur,
            // other code in your application must validate, or the
            // PrincipalPermission object is used.
            Thread.CurrentPrincipal = myPrincipal;

            // Print values to the console.
            String name = myPrincipal.Identity.Name;
            bool auth = myPrincipal.Identity.IsAuthenticated;
            bool isInRole = myPrincipal.IsInRole("Manager");

            Console.WriteLine("The name is: {0}", name);
            Console.WriteLine("The isAuthenticated is: {0}", auth);
            Console.WriteLine("Is this a Manager? {0}", isInRole);

            _output.WriteLine("The name is: {0}", name);
            _output.WriteLine("The isAuthenticated is: {0}", auth);
            _output.WriteLine("Is this a Manager? {0}", isInRole);
            _output.WriteLine("Type:" + myIdentity.AuthenticationType);

            // Verify that the generic principal has been assigned the
            // NetworkUser role.
            if (myPrincipal.IsInRole("Manager"))
            {
                _output.WriteLine("User belongs to the Manager role.");
            }

            _output.WriteLine("Hello World");

            WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
            WindowsBuiltInRole myBuiltInRole = WindowsBuiltInRole.Guest; 

            string[] roles = new string[10];
            if (windowsIdentity.IsAuthenticated)
            {
                // Add custom NetworkUser role.
                roles[0] = "NetworkUser";
            }

            if (windowsIdentity.IsGuest)
            {
                // Add custom GuestUser role.
                roles[1] = "GuestUser";
            }

            if (windowsIdentity.IsSystem)
            {
                // Add custom SystemUser role.
                roles[2] = "SystemUser";
            }

            // Construct a GenericIdentity object based on the current Windows
            // identity name and authentication type.
            string authenticationType = windowsIdentity.AuthenticationType;
            string userName = windowsIdentity.Name;
            GenericIdentity genericIdentity =
                new GenericIdentity(userName, authenticationType);

            // Construct a GenericPrincipal object based on the generic identity
            // and custom roles for the user.
            GenericPrincipal genericPrincipal =
                new GenericPrincipal(genericIdentity, roles);

            _output.WriteLine("This is the list of roles of genericPrincipal:");
            foreach (string i in roles)
            {
                _output.WriteLine("Role: " + i);
            }
            _output.WriteLine("This is Window Identity." + genericPrincipal.IsInRole("SystemUser"));


            return 0;
        }
    }
}
