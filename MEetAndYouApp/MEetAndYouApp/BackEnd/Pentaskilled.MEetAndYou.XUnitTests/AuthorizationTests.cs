using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class AuthorizationTests
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
