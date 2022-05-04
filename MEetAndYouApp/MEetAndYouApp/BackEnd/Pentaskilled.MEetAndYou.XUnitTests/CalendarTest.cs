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
        public static string connectionString = "Data Source=DESKTOP-0QA4EN0\\SQLEXPRESS;Initial Catalog=MEetAndYou-DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;";
        private readonly string _eventsAPIkey = "";
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
        [InlineData(9, "2022-04-28")]
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
        [InlineData(9, "2022-04-28")]
        public async void GetItineraryManagerTest(int userID, string date)
        {
            ICalendarManager calendarManager = new CalendarManager(_dbContext);
        }

    }
}
