using Xunit;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.Logging;
using System;

namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class UMTests
    {
        [Fact]
        public void CreateAccountRecordTest()
        {
            UserAccountEntity ua = new UserAccountEntity();

            ua.Email = "viviand2465@gmail.com";
            ua.Password = "joshishandsome1";
            ua.PhoneNumber = "4084802185";
            ua.Role = "Regular User";
            ua.RegisterDate = DateTime.UtcNow;
            ua.Active = 1;

            IUMDAO _UMDAO = new UMDAO();
            bool isSuccessfullyCreated = true;

            Assert.Equal(isSuccessfullyCreated, _UMDAO.CreateAccountRecord(ua));
        }
    }
}
