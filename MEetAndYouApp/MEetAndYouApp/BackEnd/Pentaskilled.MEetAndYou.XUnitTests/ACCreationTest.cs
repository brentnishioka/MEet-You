﻿using System;
using System.Diagnostics;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers;
using Xunit;

namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class ACCreationTest
    {
        [Fact]
        public async void AccountRegistration()
        {
            Stopwatch stopWatch = new();
            int expectedTime = 15;
            AccountCreationManager _ACManager = new AccountCreationManager();
            string email = "DIGEO465@gmail.com";
            string password = "joshiscoolIO!";
            string phoneNumber = "4084802175";

            stopWatch.Start();

            BaseResponse createResult = await _ACManager.BeginAccountCreation(email, password, phoneNumber);
            stopWatch.Stop();
            Console.WriteLine(createResult);

            Assert.Equal("Account Creation Successful", createResult.Message);
            //Assert.True(stopWatch.Elapsed.TotalSeconds <= expectedTime);
        }

        [Fact]
        public void AvailableUserName()
        {

            Stopwatch stopWatch = new();
            int expectedTime = 15;
            AccountCreationManager _ACManager = new AccountCreationManager();
            string email = "viviand24651@gmail.com";


            stopWatch.Start();

            string createResult = _ACManager.CheckAccountAvailability(email);
            stopWatch.Stop();

            Assert.Equal("Username is available.", createResult);
            //Assert.True(stopWatch.Elapsed.TotalSeconds <= expectedTime);

        }

        [Fact]
        public void UnAvailableUserName()
        {

            Stopwatch stopWatch = new();
            int expectedTime = 15;
            AccountCreationManager _ACManager = new AccountCreationManager();
            string email = "jdcramos@gmail.com";


            stopWatch.Start();

            string createResult = _ACManager.CheckAccountAvailability(email);
            stopWatch.Stop();

            Assert.Equal("Username is not available.", createResult);
            //Assert.True(stopWatch.Elapsed.TotalSeconds <= expectedTime);

        }

        [Fact]

        public void AccountIsUnactivated()
        {
            AccountCreationDAO _accountCreation = new AccountCreationDAO();
            UserAccountEntity user = new UserAccountEntity();

            Stopwatch stopWatch = new();
            int expectedTime = 15;


            user.Active = Convert.ToInt32("0");

            stopWatch.Start();

            bool createResult = _accountCreation.RemoveUnActivatedAccount(user).Result;
            stopWatch.Stop();

            Assert.False(createResult);
        }
    }
}
