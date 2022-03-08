using Xunit;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.Managers;
using System;
using System.Diagnostics;
using System.IO;

namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class ACCreationTest
    {
        [Fact]
        public void AccountRegistration()
        {
            Stopwatch stopWatch = new();
            int expectedTime = 15;
            AccountCreationManager _ACManager = new AccountCreationManager();
            string email = "DIGEON2465@gmail.com";
            string password = "joshiscoolIO!";
            string phoneNumber = "4084802175";

            stopWatch.Start();
            
            string createResult = _ACManager.BeginAccountCreation(email, password, phoneNumber);
            stopWatch.Stop();
            Console.WriteLine(createResult);

            Assert.Equal("Account Creation Successful", createResult);
            //Assert.True(stopWatch.Elapsed.TotalSeconds <= expectedTime);
        }

        [Fact]
        public void UnavailableUserName()
        {
 
                Stopwatch stopWatch = new();
                int expectedTime = 15;
                AccountCreationManager _ACManager = new AccountCreationManager();
                string email = "viviand2465@gmail.com";
                string password = "joshiscool!";
                string phoneNumber = "4084802185";

                stopWatch.Start();

                string createResult = _ACManager.CheckAccountAvailability(email, password, phoneNumber);
                stopWatch.Stop();

                Assert.Equal("Username is not available.", createResult);
                //Assert.True(stopWatch.Elapsed.TotalSeconds <= expectedTime);
            
        }
    }
}
