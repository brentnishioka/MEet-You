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
using Pentaskilled.MEetAndYou.Services.Contracts;
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
        public static string connectionString = "Data Source=meetandyou-db.cyakceoi9n4j.us-west-1.rds.amazonaws.com;Initial Catalog=MEetAndYou-DB;User Id=admin;Password=TeostraLunastraAlatreon;Connect Timeout=30;TrustServerCertificate=True;";
        private readonly string _eventsAPIkey = "";
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

            // Assert
            Assert.True(response.IsSuccessful);
        }


        [Theory]
        [InlineData(3, 5, 3)]
        public async void SaveEventsManagerTest(int numEvent, int itinID, int userID)
        {
            //Arrange
            IAPIService eventAPI = new EventAPIService(_eventsAPIkey);
            SuggestionDAO suggestionDAO = new SuggestionDAO(_dbContext);
            SuggestionManager suggestionManager = new SuggestionManager(suggestionDAO, _dbContext, eventAPI);
            List<Event> eventList = new List<Event>();

            for (int i = 0; i < numEvent; i++)
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
            BaseResponse response = await suggestionManager.SaveEventAsync(eventList, itinID, userID);
            _output.WriteLine(response.Message);

            // Assert
            Assert.True(response.IsSuccessful);
        }

        [Theory]
        [InlineData(3, 5, 20)]
        public async void SaveEventsManagerNotAuthzTest(int numEvent, int itinID, int userID)
        {
            //Arrange
            IAPIService eventAPI = new EventAPIService(_eventsAPIkey);
            SuggestionDAO suggestionDAO = new SuggestionDAO(_dbContext);
            SuggestionManager suggestionManager = new SuggestionManager(suggestionDAO, _dbContext, eventAPI);
            List<Event> eventList = new List<Event>();

            for (int i = 0; i < numEvent; i++)
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
            BaseResponse response = await suggestionManager.SaveEventAsync(eventList, itinID, userID);
            _output.WriteLine(response.Message);

            // Assert
            Assert.False(response.IsSuccessful);
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
        public async void GetRandomEventsManagerTest()
        {
            //Arrange
            IAPIService eventAPI = new EventAPIService(_eventsAPIkey);
            SuggestionDAO suggestionDAO = new SuggestionDAO(_dbContext);
            SuggestionManager suggestionManager = new SuggestionManager(suggestionDAO, _dbContext, eventAPI);
            bool actual = false;

            //Act
            SuggestionResponse results = await suggestionManager.GetRandomEventsAsync();
            if (results != null && results.IsSuccessful == true && results.Data != null)
            {
                actual = true;
            }
            //Assert
            _output.WriteLine(results.Message);
            Assert.True(actual);

            //Assert 
            Assert.True(actual);
        }

        [Theory]
        [InlineData ("art", "Long Beach", "May 4")]
        public void ParsingJSONTest(string category, string location, string date)
        {
            // Arrange
            SuggestionDAO suggestionDAO = new SuggestionDAO(_dbContext);

            Console.WriteLine("Parsing the date: ");
            DateTime dateTime = suggestionDAO.DateConversion(date);
            Console.WriteLine(date.ToString());
            int limit = 10;

            EventAPIService eventAPI = new EventAPIService(_eventsAPIkey);
            JObject results = eventAPI.GetEventByCategory(category, location, dateTime);

            bool actual = false;

            // Act
            _output.WriteLine("Parse JSON ...");
            if (results != null)
            {
                ICollection<Event> eventList = suggestionDAO.ParseJSON(results, limit);
                actual = true;
            }

            // Assert
            Assert.True(actual);
        }

        [Theory]
        [InlineData("art", "Long Beach", "May 4")]
        public void GetEventsAPITest(string category, string location, string date)
        {
            //Arrange
            EventAPIService eventAPI = new EventAPIService(_eventsAPIkey);
            SuggestionDAO suggestionDAO = new SuggestionDAO();

            Console.WriteLine("Parsing the date: ");
            DateTime dateTime = suggestionDAO.DateConversion(date);
            Console.WriteLine(date.ToString());

            bool actual = false;

            //Act
            _output.WriteLine("Loading Events using category ...");
            JObject results = eventAPI.GetEventByCategory(category, location, dateTime);
            if (results != null)
            {
                actual = true;
            }
            //Assert
            Assert.True(actual);
        }

        [Theory]
        [InlineData("Food AND drink", "Long Beach", "May 4")]
        public async void GetEventsAPIManagerTest(string category, string location, string date)
        {
            //Arrange
            IAPIService eventAPI = new EventAPIService(_eventsAPIkey);
            ISuggestionDAO suggestionDAO = new SuggestionDAO(_dbContext);
            SuggestionManager suggestionManager = new SuggestionManager(suggestionDAO, _dbContext, eventAPI);

            Console.WriteLine("Parsing the date: ");
            DateTime dateTime = suggestionDAO.DateConversion(date);
            Console.WriteLine(date.ToString());

            bool actual = false;

            //Act
            _output.WriteLine("Loading Events using category (Manager) ...");
            //JObject results = eventAPI.GetEventByCategory(category, location, dateTime);
            SuggestionResponse results = await suggestionManager.GetEvents(category, location, dateTime);
            if (results != null && results.IsSuccessful == true)
            {
                actual = true;
            }
            //Assert
            _output.WriteLine(results.Message);
            Assert.True(actual);
        }

        [Theory]
        [InlineData("Soccer", "Long Beach", "May 4")]
        public async void GetEventsManagerWrongCatTest(string category, string location, string date)
        {
            //Arrange
            IAPIService eventAPI = new EventAPIService(_eventsAPIkey);
            ISuggestionDAO suggestionDAO = new SuggestionDAO(_dbContext);
            SuggestionManager suggestionManager = new SuggestionManager(suggestionDAO, _dbContext, eventAPI);

            Console.WriteLine("Parsing the date: ");
            DateTime dateTime = suggestionDAO.DateConversion(date);
            Console.WriteLine(date.ToString());

            bool actual = false;

            //Act
            _output.WriteLine("Loading Events using category (Manager) ...");
            //JObject results = eventAPI.GetEventByCategory(category, location, dateTime);
            SuggestionResponse results = await suggestionManager.GetEvents(category, location, dateTime);
            if (results != null && results.IsSuccessful == true)
            {
                actual = true;
            }
            //Assert
            _output.WriteLine(results.Message);
            Assert.False(actual);
        }

        [Theory]
        [InlineData(5, 55)]
        public async void DeleteEventsDAOTest(int itinID, int eventID)
        {
            //Arrange
            SuggestionDAO suggestionDAO = new SuggestionDAO(_dbContext);

            //Act
            _output.WriteLine("Deleting Events....");
            BaseResponse response = await suggestionDAO.DeleteEventAsync(itinID, eventID);
            _output.WriteLine(response.Message);

            // Assert
            Assert.True(response.IsSuccessful);
        }

        [Theory]
        [InlineData(5, 57, 3)]
        public async void DeleteEventsManagerTest(int itinID, int eventID, int userID)
        {
            //Arrange
            SuggestionDAO suggestionDAO = new SuggestionDAO(_dbContext);
            IAPIService eventAPI = new EventAPIService(_eventsAPIkey);
            SuggestionManager suggestionManager = new SuggestionManager(suggestionDAO, _dbContext, eventAPI);

            //Act
            _output.WriteLine("Manager Deleting Events....");
            BaseResponse response = await suggestionManager.DeleteEventAsync(itinID, eventID, userID);
            _output.WriteLine(response.Message);

            // Assert
            Assert.True(response.IsSuccessful);
        }

        [Theory]
        [InlineData(3, 9)]
        public async void AddItineraiesDAOTest(int numItin, int itinOwver)
        {
            //Arrange
            SuggestionDAO suggestionDAO = new SuggestionDAO(_dbContext);
            List<Itinerary> itinList = new List<Itinerary>();

            for (int i = 0; i < numItin; i++)
            {
                Itinerary temp = new Itinerary {
                    ItineraryName = "Test Itinerary " + i,
                    Rating = 0,
                    ItineraryOwner = itinOwver
                };
                itinList.Add(temp);
            }

            //Act
            _output.WriteLine("Adding Itineraries ....");
            BaseResponse response = await suggestionDAO.AddItineraryAsync(itinList);
            _output.WriteLine(response.Message);

            // Assert
            Assert.True(response.IsSuccessful);
        }

        // Onwer ID is supposed to be included from the front end
        [Theory]
        [InlineData(3, 9)]
        public async void AddingItineraryManagerTest(int numItin, int itinOwver)
        {
            //Arrange
            IAPIService eventAPI = new EventAPIService(_eventsAPIkey);
            SuggestionDAO suggestionDAO = new SuggestionDAO(_dbContext);
            SuggestionManager suggestionManager = new SuggestionManager(suggestionDAO, _dbContext, eventAPI);
            List<Itinerary> itinList = new List<Itinerary>();

            for (int i = 0; i < numItin; i++)
            {
                Itinerary temp = new Itinerary {
                    ItineraryName = "Test Itinerary " + i,
                    Rating = 0,
                    ItineraryOwner = itinOwver
                };
                itinList.Add(temp);
            }

            //Act
            _output.WriteLine("Adding Itineraries ....");
            BaseResponse response = await suggestionManager.AddItineraryAsync(itinList);
            _output.WriteLine(response.Message);

            // Assert
            Assert.True(response.IsSuccessful);
        }

        [Theory]
        [InlineData("Concert", "Long Beach", "May 5")]
        public async void SaveEventsInBulk(string category, string location, string date)
        {
            //Arrange
            SuggestionDAO suggestionDAO = new SuggestionDAO(_dbContext);

            Console.WriteLine("Parsing the date: ");
            EventAPIService eventAPI = new EventAPIService(_eventsAPIkey);
            DateTime dateTime = suggestionDAO.DateConversion(date);
            Console.WriteLine(date.ToString());
            int limit = 10;

            JObject results = eventAPI.GetEventByCategory(category, location, dateTime);

            bool actual = false;



            //Act
            _output.WriteLine("Parse JSON ...");
            List<Event> eventList = new List<Event>();
            if (results != null)
            {
                eventList = (List<Event>)suggestionDAO.ParseJSON(results, limit);
                actual = true;
            }
            _output.WriteLine("Saving Events....");
            BaseResponse response = await suggestionDAO.SaveEventsListAsync(eventList);
            _output.WriteLine(response.Message);

            // Assert
            Assert.True(response.IsSuccessful);
        }
    }
}
