using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Services.Implementation;
using Xunit;
using Xunit.Abstractions;

namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class SuggestionTests
    {
        private readonly ITestOutputHelper _output;
        private MEetAndYouDBContext _dbContext;
        public static DbContextOptions<MEetAndYouDBContext> dbContextOptions { get; }
        public static string connectionString = "Data Source=DESKTOP-0QA4EN0\\SQLEXPRESS;Initial Catalog=MEetAndYou-DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;";
        static SuggestionTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<MEetAndYouDBContext>()
                .UseSqlServer(connectionString)
                .Options;
            
        }
        public SuggestionTests(ITestOutputHelper output)
        {
            this._output = output;
            _dbContext = new MEetAndYouDBContext(dbContextOptions);
        }

        [Theory]
        [InlineData (3, 5)]
        public async void SaveEventsTest(int numEvent, int itinID)
        {
            //Arrange
            SuggestionDAO suggestionDAO = new SuggestionDAO(_dbContext);
            List<Event> eventList = new List<Event>();

            for (int i = 0; i<numEvent; i++)
            {
                Event temp = new Event {
                    EventName = "Test event " + i,
                    Address = i + "Main street, Long Beach CA 99284",
                    Description = "Test events use for saving events unit test",
                    EventDate = DateTime.Now
                };
                eventList.Add(temp);
            }

            //Act
            _output.WriteLine("Saving Events....");
            BaseResponse response = await suggestionDAO.SaveEventAsync(eventList, itinID);
            _output.WriteLine(response.Message);

            //
            Assert.True(response.IsSuccessful);
        }

        [Fact]
        public async void GetRandomCategoryTest()
        {
            //Arrange
            SuggestionDAO suggestionDAO = new SuggestionDAO(_dbContext);
            bool actual = false;

            //Act
            Category randCat = await suggestionDAO.GetRandomCategory();
            if (randCat != null)
            {
                actual = true;
            }
            _output.WriteLine("Category: " + randCat.CategoryName);

            //Assert 
            Assert.True(actual);
        }

        [Fact]
        public void ParsingJSONTest()
        {
            // Arrange
            SuggestionDAO suggestionDAO = new SuggestionDAO(_dbContext);

            string location = "Long Beach";
            string category = "coffee";
            Console.WriteLine("Parsing the date: ");
            string date = "May 1";
            DateTime dateTime = suggestionDAO.DateConversion(date);
            Console.WriteLine(date.ToString());
            int limit = 10;

            EventAPIService eventAPI = new EventAPIService();
            JObject results = eventAPI.GetEventByCategory(category, location, dateTime);

            bool actual = false;

            // Act
            _output.WriteLine("Parse JSON ...");
            ICollection<Event> eventList = suggestionDAO.ParseJSON(results, limit);
            if(eventList != null)
            {
                actual=true;
            }

            // Assert
            Assert.True(actual);
        }


    }
}
