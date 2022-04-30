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
        public static string connectionString = "";

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

        // Test to add existing user to itinerary with a given permission
        [Theory]
        [InlineData("jdcramos@gmail.com")]      // Email exists in DB
        public async void AddExistingUserTest(string email)
        {
            //Arrange
            HyperlinkDAO hyperlinkDAO = new HyperlinkDAO(_dbContext);
            UserAccountRecordResponse user;
            HyperlinkResponse hyperResponse;
            Random rand = new Random();

            //Act
            _output.WriteLine("Adding user to itinerary....");
            user = await hyperlinkDAO.GetUserAccountRecordAsync(email);
            hyperResponse = await hyperlinkDAO.AddUserToItineraryAsync(user.Data, 9, "View");
            _output.WriteLine(hyperResponse.Message);

            //Assert 
            Assert.True(hyperResponse.IsSuccessful);
        }

        // Test to add non-existing user to itinerary with a given permission
        [Theory]
        [InlineData("DoesNotExist@email.com")]  // Email does NOT exist in DB
        public async void AddNonExistingUserTest(string email)
        {
            //Arrange
            HyperlinkDAO hyperlinkDAO = new HyperlinkDAO(_dbContext);
            UserAccountRecordResponse user;
            HyperlinkResponse hyperResponse;
            Random rand = new Random();

            //Act
            _output.WriteLine("Adding user to itinerary....");
            user = await hyperlinkDAO.GetUserAccountRecordAsync(email);
            hyperResponse = await hyperlinkDAO.AddUserToItineraryAsync(user.Data, 9, "View");
            _output.WriteLine(hyperResponse.Message);

            //Assert 
            Assert.False(hyperResponse.IsSuccessful);
        }

        // Pulls UserAccountRecord by email from database context
        [Theory]
        [InlineData("jdcramos@gmail.com")]      // Email exists in DB
        public async void GetExistingUserTest(string email)
        {
            //Arrange
            HyperlinkDAO hyperlinkDAO = new HyperlinkDAO(_dbContext);
            UserAccountRecord userAccountRecord = new UserAccountRecord();

            //Act
            _output.WriteLine("Pulling User Account Record....");
            UserAccountRecordResponse response = await hyperlinkDAO.GetUserAccountRecordAsync(email);
            _output.WriteLine(response.Message);

            // Assert
            Assert.True(response.IsSuccessful);
        }

        // Pulls UserAccountRecord by email from database context
        [Theory]
        [InlineData("DoesNotExist@email.com")]  // Email does NOT exist in DB
        public async void GetNonExistingUserTest(string email)
        {
            //Arrange
            HyperlinkDAO hyperlinkDAO = new HyperlinkDAO(_dbContext);
            UserAccountRecord userAccountRecord = new UserAccountRecord();

            //Act
            _output.WriteLine("Pulling User Account Record....");
            UserAccountRecordResponse response = await hyperlinkDAO.GetUserAccountRecordAsync(email);
            _output.WriteLine(response.Message);

            // Assert
            Assert.False(response.IsSuccessful);
        }

        // Test to determine if a user if the owner of an itinerary
        [Theory]
        [InlineData(3, 5)]  // User owns the itinerary
        public async void UserDoesOwnTest(int userID, int itineraryID)
        {
            //Arrange
            HyperlinkDAO HyperlinkDAO = new HyperlinkDAO(_dbContext);

            //Act
            _output.WriteLine("Checking owner....");
            HyperlinkResponse response = await HyperlinkDAO.isUserOwnerAsync(userID, itineraryID);
            _output.WriteLine(response.Message);

            //Assert
            Assert.True(response.IsSuccessful);
        }

        // Test to determine if a user if the owner of an itinerary
        [Theory]
        [InlineData(2, 5)]  // User does NOT own the itinerary
        public async void UserDoesNotOwnTest(int userID, int itineraryID)
        {
            //Arrange
            HyperlinkDAO HyperlinkDAO = new HyperlinkDAO(_dbContext);

            //Act
            _output.WriteLine("Checking owner....");
            HyperlinkResponse response = await HyperlinkDAO.isUserOwnerAsync(userID, itineraryID);
            _output.WriteLine(response.Message);

            //Assert
            Assert.False(response.IsSuccessful);
        }

        // Test to remove existing user from itinerary with a given permission
        [Theory]
        [InlineData("jdcramos@gmail.com")]      // Email exists in DB
        public async void RemoveExistingUserTest(string email)
        {
            //Arrange
            HyperlinkDAO hyperlinkDAO = new HyperlinkDAO(_dbContext);
            UserAccountRecordResponse user;
            HyperlinkResponse hyperResponse;
            Random rand = new Random();

            //Act
            _output.WriteLine("Removing user from itinerary....");
            user = await hyperlinkDAO.GetUserAccountRecordAsync(email);
            hyperResponse = await hyperlinkDAO.RemoveUserFromItineraryAsync(user.Data, 9, "View");
            _output.WriteLine(hyperResponse.Message);

            //Assert 
            Assert.True(hyperResponse.IsSuccessful);
        }

        // Test to remove non-existent user from itinerary with a given permission
        [Theory]
        [InlineData("DoesNotExist@email.com")]  // Email does NOT exist in DB
        public async void RemoveNonExistingUserTest(string email)
        {
            //Arrange
            HyperlinkDAO hyperlinkDAO = new HyperlinkDAO(_dbContext);
            UserAccountRecordResponse user;
            HyperlinkResponse hyperResponse;
            Random rand = new Random();

            //Act
            _output.WriteLine("Removing user from itinerary....");
            user = await hyperlinkDAO.GetUserAccountRecordAsync(email);
            hyperResponse = await hyperlinkDAO.RemoveUserFromItineraryAsync(user.Data, 9, "View");
            _output.WriteLine(hyperResponse.Message);

            //Assert 
            Assert.False(hyperResponse.IsSuccessful);
        }
    }
}
