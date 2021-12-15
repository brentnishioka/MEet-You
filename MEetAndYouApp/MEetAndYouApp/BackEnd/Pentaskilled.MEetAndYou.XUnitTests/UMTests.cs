using Xunit;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.Managers;
using System;
using System.Data.SqlClient;
using System.Data;

namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class UMTests
    {
        [Fact]
        public void BeginCreateUserProcessTest()
        {
            UserAccountEntity user = new UserAccountEntity();

            string email = "viviand2465@gmail.com";
            string password = "joshiscool!";
            string phoneNumber = "4084802185";
            string registerDate = DateTime.UtcNow.ToString();
            int active = 1;

            UMManager _UMManager = new UMManager();

            _UMManager.VerifyAdmin("rupak@gmail.com", "198@2f.aw!fj");

            string isSuccessfullyCreated = "User Account successfully created";

            Assert.Equal(isSuccessfullyCreated, _UMManager.BeginCreateUserProcess(email, password, phoneNumber, registerDate, active));
        }

        [Fact]
        public void UpdateUserAccountEmailTest()
        {
            int id = 1;
            string newEmail = "gidjoshviv@gmail.com";

            IUMDAO _UMDAO = new UMDAO();
            bool isEmailSuccessfullyUpdated = true;

            Assert.Equal(isEmailSuccessfullyUpdated, _UMDAO.isUserAccountEmailUpdated(id, newEmail));
        }

        [Fact]
        public void UpdateUserAccountPasswordTest()
        {
            int id = 1;
            string newPassword = "PasSwOrD10";

            IUMDAO _UMDAO = new UMDAO();
            bool isPasswordSuccessfullyUpdated = true;

            Assert.Equal(isPasswordSuccessfullyUpdated, _UMDAO.isUserAccountPasswordUpdated(id, newPassword));
        }

        [Fact]
        public void UpdateUserccountPhoneTest()
        {
            int id = 1;
            string newPhone = "(516) 598-2915";

            IUMDAO _UMDAO = new UMDAO();
            bool isPhoneSuccessfullyUpdated = true;

            Assert.Equal(isPhoneSuccessfullyUpdated, _UMDAO.isUserAccountPhoneUpdated(id, newPhone));
        }

        [Fact]
        public void DeleteUserAccountRecordTest()
        {
            int id = 6;

            IUMDAO _UMDAO = new UMDAO();
            bool isSuccessfullyDeleted = true;

            Assert.Equal(isSuccessfullyDeleted.ToString(), _UMDAO.isUserAccountDeleted(id).ToString());
        }

        [Fact]
        public void DisableUserAccountRecordTest()
        {
            int id = 1;

            IUMDAO _UMDAO = new UMDAO();
            bool isSuccessfullyDisabled = true;

            Assert.Equal(isSuccessfullyDisabled, _UMDAO.isUserAccountDisabled(id));
        }

        [Fact]
        public void EnableUserAccountRecordTest()
        {
            int id = 3;

            IUMDAO _UMDAO = new UMDAO();
            bool isSuccessfullyEnabled = true;

            Assert.Equal(isSuccessfullyEnabled, _UMDAO.isUserAccountEnabled(id));
        }

        [Fact]
        public void CreateAdminAccountRecordTest()
        {
            AdminAccountEntity admin = new AdminAccountEntity();

            admin.Email = "sysadmin@gmail.com";
            admin.Password = "password";

            IUMDAO _UMDAO = new UMDAO();
            bool isSuccessfullyCreated = true;

            Assert.Equal(isSuccessfullyCreated, _UMDAO.isAdminAccountCreated(admin));
        }

        [Fact]
        public void VerifyUserRecordInDBTest()
        {
            UserAccountEntity user = new UserAccountEntity();

            user.UserID = 1;
            user.Email = "viviand2465@gmail.com";
            user.Password = "joshiscool!";
            user.PhoneNumber = "4084802185";
            user.RegisterDate = "12/15/2021 2:23:44 AM";
            user.Active = 1; 

            IUMDAO _UMDAO = new UMDAO();
            bool doesRecordExist = true;

            Assert.Equal(doesRecordExist.ToString(), _UMDAO.isUserInDBVerified(user).ToString());
        }

        [Fact]
        public void UpdateAdminAccountEmailTest()
        {
            int id = 1;
            string newEmail = "Best.Admin@gmail.com";

            IUMDAO _UMDAO = new UMDAO();
            bool isEmailSuccessfullyUpdated = true;

            Assert.Equal(isEmailSuccessfullyUpdated, _UMDAO.isAdminEmailUpdated(id, newEmail));
        }

        [Fact]
        public void UpdateAdminAccountPasswordTest()
        {
            int id = 1;
            string newPassword = "AdMiN10";

            IUMDAO _UMDAO = new UMDAO();
            bool isPasswordSuccessfullyUpdated = true;

            Assert.Equal(isPasswordSuccessfullyUpdated, _UMDAO.isAdminPasswordUpdated(id, newPassword));
        }

        [Fact]
        public void DeleteAdminAccountRecordTest()
        {
            int id = 4;

            IUMDAO _UMDAO = new UMDAO();
            bool isSuccessfullyDeleted = true;

            Assert.Equal(isSuccessfullyDeleted.ToString(), _UMDAO.isAdminDeleted(id).ToString());
        }

        [Fact]
        public void VerifyUserInfoTest()
        {
            UserAccountEntity user = new UserAccountEntity();

            user.UserID = 2;
            user.Email = "jdcramos@gmail.com";
            user.Password = "jimothy235!!";
            user.PhoneNumber = "(800)813-5420";
            user.RegisterDate = "12/12/2021 1:11:11 AM";
            user.Active = 1;

            UMManager _UMManager= new UMManager();
            bool isValidEmail = true;

            Assert.Equal(isValidEmail.ToString(), _UMManager.VerifyUserInfo(user.Email, user.Password, user.PhoneNumber).ToString());
        }

        [Fact]
        public void VerifyAdminRecordInDBTest()
        {
            string email = "rupak@gmail.com";
            string password = "198@2f.aw!fj";

            UMManager _UMManager = new UMManager();
            bool doesRecordExist = true;

            Assert.Equal(doesRecordExist.ToString(), _UMManager.VerifyAdmin(email, password).ToString());
        }
    }
}
