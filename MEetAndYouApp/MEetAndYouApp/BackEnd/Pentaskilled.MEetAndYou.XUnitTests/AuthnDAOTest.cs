using Xunit;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.Managers;
using System;
using System.Diagnostics;
using System.IO;

namespace Pentaskilled.MEetAndYou.XUnitTests
{   
    public class AuthnDAOTest
    {
        [Fact]
        public void VerifyCredentialsInDBTest()
        {
            IAuthnDAO _AuthnDAO = new AuthnDAO();
            string email = "jdcramos@gmail.com";
            string password = "jimothy235!!";
            bool accountExists = true;

            Assert.Equal(accountExists, _AuthnDAO.ValidateCredentials(email, password).Result); 
        } 
    }
}
