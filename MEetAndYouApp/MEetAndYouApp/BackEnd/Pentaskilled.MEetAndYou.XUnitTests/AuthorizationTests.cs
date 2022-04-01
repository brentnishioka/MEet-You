using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Services.Implementation;
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
            List<string> actualList = AuthzDAO.GetRoles(2);
            _output.WriteLine("Resulting roles: ");
            _output.WriteLine("count: " + actualList.Count);
            foreach (string r in actualList) {
                _output.WriteLine(r);
            }

            //Assert
            Assert.Equal(expectedList, actualList);
        }

        // This test required the UserToken with ID: 4 already exist in the database. 
        [Fact]
        public void VerifyValidTokenTest()
        {
            // Arrange
            AuthnDAO authnDAO = new AuthnDAO();
            AuthorizationDAO authzDAO = new AuthorizationDAO();
            AuthnService authnService = new AuthnService();

            string token = "blueberrystrawberryy";
            int userID = 4;

            _output.WriteLine("");
            _output.WriteLine("Start adding new token: ");
            bool isSaved = authnDAO.SaveToken(userID, token).Result;
            _output.WriteLine("Result: " + isSaved);

            //Act 
            _output.WriteLine("");
            _output.WriteLine("Start verifying token: ");
            bool isVerified = authzDAO.VerifyToken(userID, token);
            _output.WriteLine("Result: " + isVerified);

            //Clean-up
            _output.WriteLine("");
            _output.WriteLine("Start removing token: ");
            bool isDeleted = authnDAO.DeleteToken(userID).Result;
            _output.WriteLine("Result: " + isDeleted);

            //Assert
            Assert.True(isVerified, String.Format("Expected True but got {0}", isVerified));
        }

        [Fact]
        public void VerifyWrongTokenTest()
        {
            // Arrange
            AuthnDAO authnDAO = new AuthnDAO();
            AuthorizationDAO authzDAO = new AuthorizationDAO();
            AuthnService authnService = new AuthnService();

            string token = "blueberrystrawberryy";
            string wrongToken = "blue123rystrawberryy";
            int userID = 4;

            _output.WriteLine("");
            _output.WriteLine("Start adding new token: ");
            bool isSaved = authnDAO.SaveToken(userID, token).Result;
            _output.WriteLine("Result: " + isSaved);

            //Act 
            _output.WriteLine("");
            _output.WriteLine("Start verifying token: ");
            bool isVerified = authzDAO.VerifyToken(userID, wrongToken);
            _output.WriteLine("Result: " + isVerified);

            //Clean-up
            _output.WriteLine("");
            _output.WriteLine("Start removing token: ");
            bool isDeleted = authnDAO.DeleteToken(userID).Result;
            _output.WriteLine("Result: " + isDeleted);

            //Assert
            Assert.False(isVerified, String.Format("Expected False but got {0}", isVerified));
        }

        [Fact]
        public void VerifyWrongIDTest()
        {
            // Arrange
            AuthnDAO authnDAO = new AuthnDAO();
            AuthorizationDAO authzDAO = new AuthorizationDAO();
            AuthnService authnService = new AuthnService();

            string token = "blueberrystrawberryy";
            int userID = 4;
            int wrongUserID = 123;

            _output.WriteLine("");
            _output.WriteLine("Start adding new token: ");
            bool isSaved = authnDAO.SaveToken(userID, token).Result;
            _output.WriteLine("Result: " + isSaved);

            //Act 
            _output.WriteLine("");
            _output.WriteLine("Start verifying token: ");
            bool isVerified = authzDAO.VerifyToken(wrongUserID, token);
            _output.WriteLine("Result: " + isVerified);

            //Clean-up
            _output.WriteLine("");
            _output.WriteLine("Start removing token: ");
            bool isDeleted = authnDAO.DeleteToken(userID).Result;
            _output.WriteLine("Result: " + isDeleted);

            //Assert
            Assert.False(isVerified, String.Format("Expected False but got {0}", isVerified));
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
