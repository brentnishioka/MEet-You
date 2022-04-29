using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Services.Implementation;
using Xunit;
using Xunit.Abstractions;

namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class HyperlinkTests
    {
        private readonly ITestOutputHelper _output;
        private MEetAndYouDBContext _dbContext;
        public static DbContextOptions<MEetAndYouDBContext> dbContextOptions { get; }
        public static string connectionString = "Data Source=meetandyou-db.cyakceoi9n4j.us-west-1.rds.amazonaws.com;Initial Catalog=MEetAndYou-DB;User Id=admin;Password=!491MEetAndYou5;Connect Timeout=30;TrustServerCertificate=True;";

        static HyperlinkTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<MEetAndYouDBContext>()
                .UseSqlServer(connectionString)
                .Options;

        }
        public HyperlinkTests(ITestOutputHelper output)
        {
            this._output = output;
            _dbContext = new MEetAndYouDBContext(dbContextOptions);
        }

        [Theory]
        [InlineData(3, 5)]
        public async void IsUserOwnerTest(int userID, int itineraryID)
        {
            //Arrange
            HyperlinkDAO HyperlinkDAO = new HyperlinkDAO(_dbContext);
            List<Event> eventList = new List<Event>();

            //Act
            _output.WriteLine("Checking owner....");
            HyperlinkResponse response = await HyperlinkDAO.isUserOwner(userID, itineraryID);
            _output.WriteLine(response.Message);

            //Assert
            Assert.True(response.IsSuccessful);
        }
    }
}
