using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Logging;
using Pentaskilled.MEetAndYou.Managers;
using Pentaskilled.MEetAndYou.Services;
using Pentaskilled.MEetAndYou.Services.Contracts;
using Pentaskilled.MEetAndYou.Services.Implementation;
using Pentaskilled.MEetAndYou.Managers.Implementation;
using Xunit;
using Xunit.Abstractions;
using Pentaskilled.MEetAndYou.DataAccess;

namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class SettingsTests
    {
        private ITestOutputHelper _outputHelper;
        private MEetAndYouDBContext _dbContext;

        public static DbContextOptions<MEetAndYouDBContext> dbContextOptions { get; }
        public static string connectionString = "Data Source=meetandyou-db.cyakceoi9n4j.us-west-1.rds.amazonaws.com;Initial Catalog=MEetAndYou-DB;User Id=admin;Password=TeostraLunastraAlatreon;Connect Timeout=30;TrustServerCertificate=True;";

        static SettingsTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<MEetAndYouDBContext>()
                .UseSqlServer(connectionString)
                .Options;

        }

        public SettingsTests(ITestOutputHelper helper)
        {
            _outputHelper = helper;
            _dbContext = new MEetAndYouDBContext(dbContextOptions);


        }

        /// <summary>
        /// Test to see if email is successfully updated for the user.
        /// </summary>
        [Fact]
        public void checkEmailUpdate()
        {
            bool isValid = false;
            BaseResponse response;
            try
            {
                SettingsDAO settingsDAO = new SettingsDAO(_dbContext);
                response = settingsDAO.updateUserEmail(5, "rayray@rayray.edu").Result;
                isValid = true;
                _outputHelper.WriteLine($"Message: {response.Message}");
            }
            catch (Exception ex)
            {
                
            }
            
            Assert.True(isValid);
            

        }

        /// <summary>
        /// Test to see if phone number for user is updated.
        /// </summary>
        [Fact]
        public void checkPhoneUpdate()
        {
            bool isValid = false;
            BaseResponse response;
            try
            {
                SettingsDAO settings = new SettingsDAO(_dbContext);
                response = settings.updateUserPhone(5, "number").Result;
                isValid = true;
                _outputHelper.WriteLine($"Message: {response.Message}");
            }
            catch (Exception ex) { }

            Assert.True(isValid);
        }









    }
}
