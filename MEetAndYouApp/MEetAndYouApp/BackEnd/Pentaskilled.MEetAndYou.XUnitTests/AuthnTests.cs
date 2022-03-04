using Xunit;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.Managers;
using System;
using System.Diagnostics;
using System.IO;


namespace Pentaskilled.MEetAndYou.XUnitTests
{   
    public class AuthnTests
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

        [Fact]
        public void verifyAuthnManager()
        {
            AuthnManager authnManager = new AuthnManager();
            string returnVal = authnManager.AuthenticateUser("jdcramos@gmail.com", "jimothy235!!");
            Assert.NotNull(returnVal);
            
        }

        [Fact]
        public void GetPhoneNumberTest()
        {
            IAuthnDAO _AuthnDAO = new AuthnDAO();
            string email = "jdcramos@gmail.com";
            string password = "jimothy235!!";
            string phoneNum = "(801)813-5420";

            Assert.Equal(phoneNum, _AuthnDAO.GetPhoneNum(email, password).Result);
        }
    }
}
