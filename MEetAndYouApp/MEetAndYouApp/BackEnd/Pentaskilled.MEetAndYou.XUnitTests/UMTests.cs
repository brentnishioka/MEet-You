using Xunit;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.Managers;
using System;
using System.Diagnostics;
using System.IO;
namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class UMTests
    {
        [Fact]
        public void CreateUserTest()
        {
            Stopwatch stopWatch = new();
            int expectedTime = 5;
            UMManager _UMManager = new UMManager();
            string email = "viviand2465@gmail.com";
            string password = "joshiscool!";
            string phoneNumber = "4084802185";
            string registerDate = DateTime.UtcNow.ToString();
            string active = "1";

            stopWatch.Start();
            bool isAdmin = _UMManager.IsAdminVerified("rupak@gmail.com", "198@2f.aw!fj");
            string createResult = UMManager.BeginCreateUser(email, password, phoneNumber, registerDate, active);
            stopWatch.Stop();

            Assert.True(isAdmin);
            Assert.Equal("User account was successfully created", createResult);
            Assert.True(stopWatch.Elapsed.TotalSeconds <= expectedTime);
        }

        [Fact]
        public void UpdateUserEmailTest()
        {
            Stopwatch stopWatch = new();
            int expectedTime = 5;
            UMManager _UMManager = new UMManager();
            string id = "1";
            string newEmail = "gidjoshviv@gmail.com";

            stopWatch.Start();
            bool isAdmin = _UMManager.IsAdminVerified("rupak@gmail.com", "198@2f.aw!fj");
            string updateResult = _UMManager.BeginUpdateUserEmail(id, newEmail);
            stopWatch.Stop();

            Assert.True(isAdmin);
            Assert.Equal("User email was successfully updated", updateResult);
            Assert.True(stopWatch.Elapsed.TotalSeconds <= expectedTime);
        }

        [Fact]
        public void UpdateUserPasswordTest()
        {
            Stopwatch stopWatch = new();
            int expectedTime = 5;
            UMManager _UMManager = new UMManager();
            string id = "1";
            string newPassword = "PasSwOrD10";

            stopWatch.Start();
            bool isAdmin = _UMManager.IsAdminVerified("rupak@gmail.com", "198@2f.aw!fj");
            string updateResult = _UMManager.BeginUpdateUserPassword(id, newPassword);
            stopWatch.Stop();

            Assert.True(isAdmin);
            Assert.Equal("User password was successfully updated", updateResult);
            Assert.True(stopWatch.Elapsed.TotalSeconds <= expectedTime);
        }

        [Fact]
        public void UpdateUserPhoneTest()
        {
            Stopwatch stopWatch = new();
            int expectedTime = 5;
            UMManager _UMManager = new UMManager();
            string id = "1";
            string newPhone = "(516)598-2915";

            stopWatch.Start();
            bool isAdmin = _UMManager.IsAdminVerified("rupak@gmail.com", "198@2f.aw!fj");
            string updateResult = _UMManager.BeginUpdateUserPhone(id, newPhone);
            stopWatch.Stop();

            Assert.True(isAdmin);
            Assert.Equal("User phone number was successfully updated", updateResult);
            Assert.True(stopWatch.Elapsed.TotalSeconds <= expectedTime);
        }

        [Fact]
        public void DeleteUserAccountTest()
        {
            Stopwatch stopWatch = new();
            int expectedTime = 5;
            UMManager _UMManager = new UMManager();
            string id = "6";

            stopWatch.Start();
            bool isAdmin = _UMManager.IsAdminVerified("rupak@gmail.com", "198@2f.aw!fj");
            string deleteResult = _UMManager.BeginDeleteUserAccount(id);
            stopWatch.Stop();

            Assert.True(isAdmin);
            Assert.Equal("User account was successfully deleted", deleteResult);
            Assert.True(stopWatch.Elapsed.TotalSeconds <= expectedTime);
        }

        [Fact]
        public void DisableUserAccountTest()
        {
            Stopwatch stopWatch = new();
            int expectedTime = 5;
            UMManager _UMManager = new UMManager();
            string id = "1";

            stopWatch.Start();
            bool isAdmin = _UMManager.IsAdminVerified("rupak@gmail.com", "198@2f.aw!fj");
            string disableResult = _UMManager.BeginDisableUserAccount(id);
            stopWatch.Stop();

            Assert.True(isAdmin);
            Assert.Equal("User account was successfully disabled", disableResult);
            Assert.True(stopWatch.Elapsed.TotalSeconds <= expectedTime);
        }

        [Fact]
        public void EnableUserAccountRecordTest()
        {
            Stopwatch stopWatch = new();
            int expectedTime = 5;
            UMManager _UMManager = new UMManager();
            string id = "3";

            stopWatch.Start();
            bool isAdmin = _UMManager.IsAdminVerified("rupak@gmail.com", "198@2f.aw!fj");
            string enableResult = _UMManager.BeginEnableUserAccount(id);
            stopWatch.Stop();

            Assert.True(isAdmin);
            Assert.Equal("User account was successfully enabled", enableResult);
            Assert.True(stopWatch.Elapsed.TotalSeconds <= expectedTime);
        }

        [Fact]
        public void CreateAdminTest()
        {
            Stopwatch stopWatch = new();
            int expectedTime = 5;
            UMManager _UMManager = new UMManager();
            string email = "sysadmin@gmail.com";
            string password = "password";

            stopWatch.Start();
            bool isAdmin = _UMManager.IsAdminVerified("rupak@gmail.com", "198@2f.aw!fj");
            string createResult = _UMManager.BeginCreateAdmin(email, password);
            stopWatch.Stop();

            Assert.True(isAdmin);
            Assert.Equal("Admin account was successfully created", createResult);
            Assert.True(stopWatch.Elapsed.TotalSeconds <= expectedTime);
        }

        [Fact]
        public void UpdateAdminEmailTest()
        {
            Stopwatch stopWatch = new();
            int expectedTime = 5;
            UMManager _UMManager = new UMManager();
            string id = "1";
            string newEmail = "Best.Admin@gmail.com";

            stopWatch.Start();
            bool isAdmin = _UMManager.IsAdminVerified("rupak@gmail.com", "198@2f.aw!fj");
            string updateResult = _UMManager.BeginUpdateAdminEmail(id, newEmail);
            stopWatch.Stop();

            Assert.True(isAdmin);
            Assert.Equal("Admin email was successfully updated", updateResult);
            Assert.True(stopWatch.Elapsed.TotalSeconds <= expectedTime);
        }

        [Fact]
        public void UpdateAdminAccountPasswordTest()
        {
            Stopwatch stopWatch = new();
            int expectedTime = 5;
            UMManager _UMManager = new UMManager();
            string id = "1";
            string newPassword = "AdMiN100";

            stopWatch.Start();
            bool isAdmin = _UMManager.IsAdminVerified("rupak@gmail.com", "198@2f.aw!fj");
            string updateResult = _UMManager.BeginUpdateAdminPassword(id, newPassword);
            stopWatch.Stop();

            Assert.True(isAdmin);
            Assert.Equal("Admin password was successfully updated", updateResult);
            Assert.True(stopWatch.Elapsed.TotalSeconds <= expectedTime);
        }

        [Fact]
        public void DeleteAdminAccountRecordTest()
        {
            Stopwatch stopWatch = new();
            int expectedTime = 5;
            UMManager _UMManager = new UMManager();
            string id = "4";

            stopWatch.Start();
            bool isAdmin = _UMManager.IsAdminVerified("rupak@gmail.com", "198@2f.aw!fj");
            string deleteResult = _UMManager.BeginDeleteAdminAccount(id);
            stopWatch.Stop();

            Assert.True(isAdmin);
            Assert.Equal("Admin account was successfully deleted", deleteResult);
            Assert.True(stopWatch.Elapsed.TotalSeconds <= expectedTime);
        }

        [Fact]
        public void VerifyUserInDBTest()
        {
            IUMDAO _UMDAO = new UMDAO();
            UserAccountEntity user = new UserAccountEntity();
            user.UserID = 2;
            user.Email = "jdcramos@gmail.com";
            user.Password = "jimothy235!!";
            user.PhoneNumber = "(800)813-5420";
            user.RegisterDate = "12/12/2021 1:11:11 AM";
            user.Active = 1;

            bool doesRecordExist = _UMDAO.IsUserInDBVerified(user);

            Assert.True(doesRecordExist);
        }

        [Fact]
        public void VerifyAdminInDBTest()
        {
            UMManager _UMManager = new UMManager();
            string email = "rupak@gmail.com";
            string password = "198@2f.aw!fj";

            bool doesRecordExist = _UMManager.IsAdminVerified(email, password);

            Assert.True(doesRecordExist);
        }

        [Fact]
        public void VerifyUserInfoTest()
        {
            UMManager _UMManager = new UMManager();
            string email = "jdcramos@gmail.com";
            string password = "jimothy235!!";
            string phoneNumber = "(800)813-5420";

            string verifyResult = _UMManager.VerifyUserInfo(email, password, phoneNumber);

            Assert.Equal("User info is successfully verified.", verifyResult);
        }

        [Fact]
        public void BulkOperationTest()
        {
            UMManager _UMManager = new UMManager();
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "request.zip");
            string extractedFilePath = Directory.GetCurrentDirectory();

            string bulkResult = _UMManager.BulkOperation(filePath, extractedFilePath);

            Assert.Equal("Request successful", bulkResult);
        }
    }
}
