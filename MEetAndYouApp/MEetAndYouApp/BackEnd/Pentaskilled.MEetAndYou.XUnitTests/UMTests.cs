using Xunit;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities;
using System;
using System.Data.SqlClient;
using System.Data;

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

        [Fact]
        public void UpdateAccountEmailTest()
        {
            int id = 2;
            string newEmail = "gidjoshviv@gmail.com";

            IUMDAO _UMDAO = new UMDAO();
            bool isEmailSuccessfullyUpdated = true;

            Assert.Equal(isEmailSuccessfullyUpdated, _UMDAO.UpdateAccountEmail(id, newEmail));
        }

        [Fact]
        public void UpdateAccountPasswordTest()
        {
            int id = 2;
            string newPassword = "PasSwOrD10";

            IUMDAO _UMDAO = new UMDAO();
            bool isPasswordSuccessfullyUpdated = true;

            Assert.Equal(isPasswordSuccessfullyUpdated, _UMDAO.UpdateAccountPassword(id, newPassword));
        }

        [Fact]
        public void UpdateAccountPhoneTest()
        {
            int id = 2;
            string newPhone = "(516) 598-2915";

            IUMDAO _UMDAO = new UMDAO();
            bool isPhoneSuccessfullyUpdated = true;

            Assert.Equal(isPhoneSuccessfullyUpdated, _UMDAO.UpdateAccountPhone(id, newPhone));
        }

        [Fact]
        public void UpdateAccountRoleTest()
        {
            int id = 2;
            string newRole = "System Administrator";

            IUMDAO _UMDAO = new UMDAO();
            bool isRoleSuccessfullyUpdated = true;

            Assert.Equal(isRoleSuccessfullyUpdated, _UMDAO.UpdateAccountRole(id, newRole));
        }

        [Fact]
        public void DeleteAccountRecordTest()
        {
            int id = 1;

            IUMDAO _UMDAO = new UMDAO();
            bool isSuccessfullyDeleted = true;

            Assert.Equal(isSuccessfullyDeleted.ToString(), _UMDAO.DeleteAccountRecord(id).ToString());
        }

        [Fact]
        public void DisableAccountRecordTest()
        {
            int id = 4;

            IUMDAO _UMDAO = new UMDAO();
            bool isSuccessfullyDisabled = true;

            Assert.Equal(isSuccessfullyDisabled, _UMDAO.DisableAccountRecord(id));
        }

        [Fact]
        public void EnableAccountRecordTest()
        {
            int id = 2;

            IUMDAO _UMDAO = new UMDAO();
            bool isSuccessfullyEnabled = true;

            Assert.Equal(isSuccessfullyEnabled, _UMDAO.EnableAccountRecord(id));
        }

        [Fact]
        public void VerifyUserInDBTest()
        {
            int id = 2;

            IUMDAO _UMDAO = new UMDAO();
            bool doesUserExist = true;

            Assert.Equal(doesUserExist.ToString(), _UMDAO.VerifyUserInDB(id).ToString());
        }
    }
}
