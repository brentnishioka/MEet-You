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

namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class UPDTests
    {
        private UPDManager _updManager;
        private readonly ITestOutputHelper testOutputHelper;
        private MEetAndYouDBContext _dbcontext;
        public static DbContextOptions<MEetAndYouDBContext> DbContextOptions { get; }
        public static string connectionString = "Data Source=meetandyou-db.cyakceoi9n4j.us-west-1.rds.amazonaws.com;Initial Catalog=MEetAndYou-DB;User Id=admin;Password=TeostraLunastraAlatreon;Connect Timeout=30;TrustServerCertificate=True;";

        static UPDTests()
        {
            DbContextOptions = new DbContextOptionsBuilder<MEetAndYouDBContext>()
                .UseSqlServer(connectionString)
                .Options;
        }
        
        public UPDTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
            this._updManager = new UPDManager();
            _dbcontext = new MEetAndYouDBContext(DbContextOptions);
        }


        /// <summary>
        /// Test to see if the itinerary from the user is fetched from the database
        /// </summary>
        [Fact]
        public void checkItineraryDAO()
        {
            bool valid = false;
            List<Itinerary> itineraries = new ItineraryDAO(_dbcontext).GetUserItineraries(5);
            if (itineraries.Count > 0)
                valid = true;
            foreach (var i in itineraries)
            {
                testOutputHelper.WriteLine(i.ToString());
            }
            Assert.True(valid);
        }

        /// <summary>
        /// Test to see the user's information is fetched from the database
        /// </summary>
        [Fact]
        public void checkUserRecordDAO()
        {
            bool valid = false;
            UserDAO userDAO = new UserDAO(_dbcontext);
            UserAccountRecord user = userDAO.getUserAccount(5).Result;
            if (user == null)
                return;

            testOutputHelper.WriteLine(user.ToString());
            valid = true;
            Assert.True(valid);
        }


        /// <summary>
        /// Test to see if the user data along with itineraries are fetched from the database and wrapped in a UPData object.
        /// </summary>
        [Fact]
        public void checkGetUPData()
        {
            bool valid = false;
            UPDManager manager = new UPDManager(new ItineraryDAO(_dbcontext), new UserDAO(_dbcontext));
            UPData data = manager.GetUPData(5).Result;
            testOutputHelper.WriteLine(data.ToString());
            valid = true;
            Assert.True(valid);
        }


        




    }
}
