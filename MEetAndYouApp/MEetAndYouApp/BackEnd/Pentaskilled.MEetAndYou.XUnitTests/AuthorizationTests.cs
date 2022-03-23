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
            List<string> expectedList = new List<string>{ "Admin", "User" };

            //Act
            _output.WriteLine("result " + AuthzDAO.ConnectionString);
            List<string> actualList = AuthzDAO.GetRoles(2).Result;
            _output.WriteLine("Resulting roles: ");
            _output.WriteLine("count: " + actualList.Count);
            foreach (string r in actualList) {
                _output.WriteLine(r);
            }

            //Assert
            Assert.Equal(expectedList, actualList);
        }

        //[Fact]
        //public void GetUserRolesAsyncTest()
        //{
        //    //Arrange
        //    AuthorizationDAO AuthzDAO = new AuthorizationDAO();
        //    AuthzDAO.ConnectionString = new ConnectionString().ToString();
        //    List<string> expectedList = new List<string> { "Admin", "User" };
        //    int userID = 2;

        //    //Act
        //    _output.WriteLine("result " + AuthzDAO.ConnectionString);
        //    List<string> actualList = AuthzDAO.GetRolesAsync(userID).Result;
        //    _output.WriteLine("Resulting roles: ");
        //    _output.WriteLine("count: " + actualList.Count);
        //    foreach (string r in actualList)
        //    {
        //        _output.WriteLine(r);
        //    }

        //    //Assert
        //    Assert.Equal(expectedList, actualList);
        //}
    }
}
