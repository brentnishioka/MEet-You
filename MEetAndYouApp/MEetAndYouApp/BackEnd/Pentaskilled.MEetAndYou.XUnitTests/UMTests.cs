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
        public void CreateUserAccountRecordTest()
        {
            UserAccountEntity user = new UserAccountEntity();

            user.Email = "viviand2465@gmail.com";
            user.Password = "joshiscool!";
            user.PhoneNumber = "4084802185";
            user.RegisterDate = DateTime.UtcNow;
            user.Active = 1;

            IUMDAO _UMDAO = new UMDAO();
            bool isSuccessfullyCreated = true;

            Assert.Equal(isSuccessfullyCreated, _UMDAO.CreateUserAccountRecord(user));
        }

        [Fact]
        public void UpdateUserAccountEmailTest()
        {
            int id = 2;
            string newEmail = "gidjoshviv@gmail.com";

            IUMDAO _UMDAO = new UMDAO();
            bool isEmailSuccessfullyUpdated = true;

            Assert.Equal(isEmailSuccessfullyUpdated, _UMDAO.UpdateUserAccountEmail(id, newEmail));
        }

        [Fact]
        public void UpdateUserAccountPasswordTest()
        {
            int id = 2;
            string newPassword = "PasSwOrD10";

            IUMDAO _UMDAO = new UMDAO();
            bool isPasswordSuccessfullyUpdated = true;

            Assert.Equal(isPasswordSuccessfullyUpdated, _UMDAO.UpdateUserAccountPassword(id, newPassword));
        }

        [Fact]
        public void UpdateUserccountPhoneTest()
        {
            int id = 2;
            string newPhone = "(516) 598-2915";

            IUMDAO _UMDAO = new UMDAO();
            bool isPhoneSuccessfullyUpdated = true;

            Assert.Equal(isPhoneSuccessfullyUpdated, _UMDAO.UpdateUserAccountPhone(id, newPhone));
        }

        [Fact]
        public void DeleteUserAccountRecordTest()
        {
            int id = 1;

            IUMDAO _UMDAO = new UMDAO();
            bool isSuccessfullyDeleted = true;

            Assert.Equal(isSuccessfullyDeleted.ToString(), _UMDAO.DeleteUserAccountRecord(id).ToString());
        }

        [Fact]
        public void DisableUserAccountRecordTest()
        {
            int id = 2;

            IUMDAO _UMDAO = new UMDAO();
            bool isSuccessfullyDisabled = true;

            Assert.Equal(isSuccessfullyDisabled, _UMDAO.DisableUserAccountRecord(id));
        }

        [Fact]
        public void EnableUserAccountRecordTest()
        {
            int id = 2;

            IUMDAO _UMDAO = new UMDAO();
            bool isSuccessfullyEnabled = true;

            Assert.Equal(isSuccessfullyEnabled, _UMDAO.EnableUserAccountRecord(id));
        }

        [Fact]
        public void CreateAdminAccountRecordTest()
        {
            AdminAccountEntity admin = new AdminAccountEntity();

            admin.Email = "sysadmin@gmail.com";
            admin.Password = "password";

            IUMDAO _UMDAO = new UMDAO();
            bool isSuccessfullyCreated = true;

            Assert.Equal(isSuccessfullyCreated, _UMDAO.CreateAdminAccountRecord(admin));
        }

        [Fact]
        public void VerifyUserRecordInDBTest()
        {
            UserAccountEntity user = new UserAccountEntity();

            user.Email = "viviand2465@gmail.com";
            user.Password = "joshiscool!";
            user.PhoneNumber = "4084802185";
            user.RegisterDate = DateTime.UtcNow;
            user.Active = 1;

            IUMDAO _UMDAO = new UMDAO();
            bool doesRecordExist = true;

            Assert.Equal(doesRecordExist.ToString(), _UMDAO.VerifyUserRecordInDB(user).ToString());
        }

        [Fact]
        public void VerifyAdminRecordInDBTest()
        {
            string email = "sysadmin@gmail.com";
            string password = "password";

            IUMDAO _UMDAO = new UMDAO();
            bool doesRecordExist = true;

            Assert.Equal(doesRecordExist.ToString(), _UMDAO.VerifyAdminRecordInDB(email, password).ToString());
        }

        [Fact]
        public void UpdateAdminAccountEmailTest()
        {
            int id = 1;
            string newEmail = "Best.Admin@gmail.com";

            IUMDAO _UMDAO = new UMDAO();
            bool isEmailSuccessfullyUpdated = true;

            Assert.Equal(isEmailSuccessfullyUpdated, _UMDAO.UpdateAdminAccountEmail(id, newEmail));
        }

        [Fact]
        public void UpdateAdminAccountPasswordTest()
        {
            int id = 1;
            string newPassword = "AdMiN10";

            IUMDAO _UMDAO = new UMDAO();
            bool isPasswordSuccessfullyUpdated = true;

            Assert.Equal(isPasswordSuccessfullyUpdated, _UMDAO.UpdateAdminAccountPassword(id, newPassword));
        }

        [Fact]
        public void DeleteAdminAccountRecordTest()
        {
            int id = 1;

            IUMDAO _UMDAO = new UMDAO();
            bool isSuccessfullyDeleted = true;

            Assert.Equal(isSuccessfullyDeleted.ToString(), _UMDAO.DeleteAdminAccountRecord(id).ToString());
        }
    }
}
