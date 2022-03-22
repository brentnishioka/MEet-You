using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Xunit;
using Xunit.Abstractions;

namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class AuthorizationTests
    {
        private readonly ITestOutputHelper _output;
        public AuthorizationTests(ITestOutputHelper output)
        {
            this._output = output;
        }
        [Fact]
        public void GetUserRolesTest()
        {
            //Arrange
            AuthorizationDAO AuthzDAO = new AuthorizationDAO();
            AuthzDAO.ConnectionString = new ConnectionString().ToString();
            int expectedRoleCount = 2;

            //Act
            _output.WriteLine("result " + AuthzDAO.ConnectionString);
            List<string> result = AuthzDAO.GetRoles(2).Result;
            _output.WriteLine("Resulting roles: ");
            _output.WriteLine("count: " + result.Count);
            foreach (string r in result) {
                _output.WriteLine(r);
            }

            //Assert
            Assert.Equal(expectedRoleCount, result.Count);
        }
    }
}
