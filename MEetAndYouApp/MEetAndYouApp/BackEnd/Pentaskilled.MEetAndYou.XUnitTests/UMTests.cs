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
        public void CreateUserTest()
        {
            UserAccountEntity user = new UserAccountEntity();

            string email = "viviand2465@gmail.com";
            string password = "joshiscool!";
            string phoneNumber = "4084802185";
            string registerDate = DateTime.UtcNow.ToString();
            int active = 1;

            UMManager _UMManager = new UMManager();

            _UMManager.IsAdminVerified("rupak@gmail.com", "198@2f.aw!fj");

            string isSuccessfullyCreated = "User account was successfully created";

            Assert.Equal(isSuccessfullyCreated, _UMManager.BeginCreateUser(email, password, phoneNumber, registerDate, active));
        }

        [Fact]
        public void UpdateUserEmailTest()
        {
            int id = 1;
            string newEmail = "gidjoshviv@gmail.com";

            UMManager _UMManager = new UMManager();

            _UMManager.IsAdminVerified("rupak@gmail.com", "198@2f.aw!fj");

            string isSuccessfullyUpdated = "User email was successfully updated";

            Assert.Equal(isSuccessfullyUpdated, _UMManager.BeginUpdateUserEmail(id, newEmail));
        }

        [Fact]
        public void UpdateUserPasswordTest()
        {
            int id = 1;
            string newPassword = "PasSwOrD10";

            UMManager _UMManager = new UMManager();

            _UMManager.IsAdminVerified("rupak@gmail.com", "198@2f.aw!fj");

            string isSuccessfullyUpdated = "User password was successfully updated";

            Assert.Equal(isSuccessfullyUpdated, _UMManager.BeginUpdateUserPassword(id, newPassword));
        }

        [Fact]
        public void UpdateUserPhoneTest()
        {
            int id = 1;
            string newPhone = "(516)598-2915";

            UMManager _UMManager = new UMManager();

            _UMManager.IsAdminVerified("rupak@gmail.com", "198@2f.aw!fj");

            string isSuccessfullyUpdated = "User phone number was successfully updated";

            Assert.Equal(isSuccessfullyUpdated, _UMManager.BeginUpdateUserPhone(id, newPhone));
        }

        [Fact]
        public void DeleteUserAccountTest()
        {
            int id = 6;

            UMManager _UMManager = new UMManager();

            _UMManager.IsAdminVerified("rupak@gmail.com", "198@2f.aw!fj");

            string isSuccessfullyDeleted = "User account was successfully deleted";

            Assert.Equal(isSuccessfullyDeleted, _UMManager.BeginDeleteUserAccount(id));
        }

        [Fact]
        public void DisableUserAccountTest()
        {
            int id = 1;

            UMManager _UMManager = new UMManager();

            _UMManager.IsAdminVerified("rupak@gmail.com", "198@2f.aw!fj");

            string isSuccessfullyDisabled = "User account was successfully disabled";

            Assert.Equal(isSuccessfullyDisabled, _UMManager.BeginDisableUserAccount(id));
        }

        [Fact]
        public void EnableUserAccountRecordTest()
        {
            int id = 3;

            UMManager _UMManager = new UMManager();

            _UMManager.IsAdminVerified("rupak@gmail.com", "198@2f.aw!fj");

            string isSuccessfullyEnabled = "User account was successfully enabled";

            Assert.Equal(isSuccessfullyEnabled, _UMManager.BeginEnableUserAccount(id));
        }

        [Fact]
        public void CreateAdminTest()
        {
            AdminAccountEntity admin = new AdminAccountEntity();

            admin.Email = "sysadmin@gmail.com";
            admin.Password = "password";

            UMManager _UMManager = new UMManager();

            _UMManager.IsAdminVerified("rupak@gmail.com", "198@2f.aw!fj");

            string isSuccessfullyCreated = "Admin account was successfully created";

            Assert.Equal(isSuccessfullyCreated, _UMManager.BeginCreateAdmin(admin.Email, admin.Password));
        }

        [Fact]
        public void UpdateAdminEmailTest()
        {
            int id = 1;
            string newEmail = "Best.Admin@gmail.com";

            UMManager _UMManager = new UMManager();

            _UMManager.IsAdminVerified("rupak@gmail.com", "198@2f.aw!fj");

            string isSuccessfullyUpdated = "Admin email was successfully updated";

            Assert.Equal(isSuccessfullyUpdated, _UMManager.BeginUpdateAdminEmail(id, newEmail));
        }

        [Fact]
        public void UpdateAdminAccountPasswordTest()
        {
            int id = 1;
            string newPassword = "AdMiN100";

            UMManager _UMManager = new UMManager();

            _UMManager.IsAdminVerified("rupak@gmail.com", "198@2f.aw!fj");

            string isSuccessfullyUpdated = "Admin password was successfully updated";

            Assert.Equal(isSuccessfullyUpdated, _UMManager.BeginUpdateAdminPassword(id, newPassword));
        }

        [Fact]
        public void DeleteAdminAccountRecordTest()
        {
            int id = 4;

            UMManager _UMManager = new UMManager();

            _UMManager.IsAdminVerified("rupak@gmail.com", "198@2f.aw!fj");

            string isSuccessfullyDeleted = "Admin account was successfully deleted";

            Assert.Equal(isSuccessfullyDeleted, _UMManager.BeginDeleteAdminAccount(id));
        }

        [Fact]
        public void VerifyUserInDBTest()
        {
            UserAccountEntity user = new UserAccountEntity();

            user.UserID = 2;
            user.Email = "jdcramos@gmail.com";
            user.Password = "jimothy235!!";
            user.PhoneNumber = "(800)813-5420";
            user.RegisterDate = "12/12/2021 1:11:11 AM";
            user.Active = 1;

            IUMDAO _UMDAO = new UMDAO();
            bool doesRecordExist = true;

            Assert.Equal(doesRecordExist.ToString(), _UMDAO.isUserInDBVerified(user).ToString());
        }

        [Fact]
        public void VerifyAdminInDBTest()
        {
            string email = "rupak@gmail.com";
            string password = "198@2f.aw!fj";

            UMManager _UMManager = new UMManager();
            bool doesRecordExist = true;

            Assert.Equal(doesRecordExist.ToString(), _UMManager.IsAdminVerified(email, password).ToString());
        }

        [Fact]
        public void VerifyUserInfoTest()
        {
            string email = "jdcramos@gmail.com";
            string password = "jimothy235!!";
            string phoneNumber = "(800)813-5420";

            UMManager _UMManager= new UMManager();

            Assert.Equal("User info is successfully verified.", _UMManager.VerifyUserInfo(email, password, phoneNumber));
        }
    }
}
