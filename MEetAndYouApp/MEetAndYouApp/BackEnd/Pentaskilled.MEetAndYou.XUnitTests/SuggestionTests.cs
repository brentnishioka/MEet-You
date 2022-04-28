using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
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

        [Fact]
        public async void SaveEventsTest()
        {
            //Arrange
            SuggestionDAO suggestionDAO = new SuggestionDAO();
            List<Event> eventList = new List<Event>();
            int numEvent = 3;

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
            BaseResponse response = await suggestionDAO.SaveEventAsync(eventList);
            _output.WriteLine(response.Message);

            //
            Assert.True(response.IsSuccessful);
        }
    }
}
