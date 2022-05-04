using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers.Implementation;
using Pentaskilled.MEetAndYou.Managers;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Pentaskilled.MEetAndYou.Services.Contracts;
using Pentaskilled.MEetAndYou.Services.Implementation;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Xunit;
using Xunit.Abstractions;


namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class CalendarTest
    {
        private readonly ITestOutputHelper _output;
        private MEetAndYouDBContext _dbContext;
        public static DbContextOptions<MEetAndYouDBContext> dbContextOptions { get; }
        public static string connectionString = "Data Source=meetandyou-db.cyakceoi9n4j.us-west-1.rds.amazonaws.com;Initial Catalog=MEetAndYou-DB;User Id=admin;Password=TeostraLunastraAlatreon;Connect Timeout=30;TrustServerCertificate=True;";
        
        static CalendarTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<MEetAndYouDBContext>()
                .UseSqlServer(connectionString)
                .Options;

        }
        public CalendarTest(ITestOutputHelper output)
        {
            this._output = output;
            _dbContext = new MEetAndYouDBContext(dbContextOptions);
        }

        [Theory]
        [InlineData(9, "2022-05-06")]
        public async void GetItinerayDAOTest(int userID, string date)
        {
            //Arrange
            ICalendarDAO calendardDAO = new CalendarDAO(_dbContext);
            DateTime dateTime = calendardDAO.DateConversion(date);

            ItineraryResponse actual = null;

            //Act
            actual = await calendardDAO.GetItineraries(userID, dateTime);
            _output.WriteLine("The size of the array: " + actual.Data.Count);
            foreach (Itinerary itin in actual.Data)
            {
                _output.WriteLine("Itinerary ID: " + itin.ItineraryId);
                _output.WriteLine("Itinerary Name: " + itin.ItineraryName);
            }

            //Assert
            Assert.True(actual != null);
        }

        [Theory]
        [InlineData(9, "2022-05-06")]
        public async void GetItineraryManagerTest(int userID, string date)
        {

            //Arrange 
            ICalendarDAO calendarDAO = new CalendarDAO(_dbContext);
            ICalendarManager calendarManager = new CalendarManager(calendarDAO, _dbContext);

            //Act 
            _output.WriteLine("Getting itineraries from CalendarManager...");
            ItineraryResponse itinResponse = await calendarManager.LoadUserItineraries(userID, date);
            _output.WriteLine(itinResponse.Message);

            //Assert
            Assert.True(itinResponse.IsSuccessful);           
        }


    }
}
