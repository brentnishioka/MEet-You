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
using Pentaskilled.MEetAndYou.Managers.Implementation;
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
        [InlineData("spartucus@gmail.com")]      // Email exists in DB
        public async void AddExistingUserTest(string email)
        {
            // Arrange
            HyperlinkDAO hyperlinkDAO = new HyperlinkDAO(_dbContext);
            UserAccountRecordResponse user;
            HyperlinkResponse response;
            Random rand = new Random();

            // Act
            _output.WriteLine("Adding user to itinerary....");
            user = await hyperlinkDAO.GetUserAccountRecordAsync(email);
            response = await hyperlinkDAO.AddUserToItineraryAsync(user.Data, 9, "View");
            _output.WriteLine(response.Message);

            // Assert 
            Assert.True(response.IsSuccessful);
        }

        // Test to add non-existing user to itinerary with a given permission
        [Theory]
        [InlineData("DoesNotExist@email.com")]  // Email does NOT exist in DB
        public async void AddNonExistingUserTest(string email)
        {
            // Arrange
            HyperlinkDAO hyperlinkDAO = new HyperlinkDAO(_dbContext);
            UserAccountRecordResponse user;
            HyperlinkResponse response;
            Random rand = new Random();

            // Act
            _output.WriteLine("Adding user to itinerary....");
            user = await hyperlinkDAO.GetUserAccountRecordAsync(email);
            response = await hyperlinkDAO.AddUserToItineraryAsync(user.Data, 9, "View");
            _output.WriteLine(response.Message);

            // Assert 
            Assert.False(response.IsSuccessful);
        }

        // Pulls UserAccountRecord by email from database context
        [Theory]
        [InlineData("jdcramos@gmail.com")]      // Email exists in DB
        public async void GetExistingUserTest(string email)
        {
            // Arrange
            HyperlinkDAO hyperlinkDAO = new HyperlinkDAO(_dbContext);
            UserAccountRecordResponse response;

            // Act
            _output.WriteLine("Pulling User Account Record....");
            response = await hyperlinkDAO.GetUserAccountRecordAsync(email);
            _output.WriteLine(response.Message);

            // Assert
            Assert.True(response.IsSuccessful);
        }

        // Pulls UserAccountRecord by email from database context
        [Theory]
        [InlineData("DoesNotExist@email.com")]  // Email does NOT exist in DB
        public async void GetNonExistingUserTest(string email)
        {
            // Arrange
            HyperlinkDAO hyperlinkDAO = new HyperlinkDAO(_dbContext);
            UserAccountRecordResponse response;

            // Act
            _output.WriteLine("Pulling User Account Record....");
            response = await hyperlinkDAO.GetUserAccountRecordAsync(email);
            _output.WriteLine(response.Message);

            // Assert
            Assert.False(response.IsSuccessful);
        }

        // Test to determine if a user if the owner of an itinerary
        [Theory]
        [InlineData(3, 5)]  // User owns the itinerary
        public async void UserDoesOwnTest(int userID, int itineraryID)
        {
            // Arrange
            HyperlinkDAO HyperlinkDAO = new HyperlinkDAO(_dbContext);
            HyperlinkResponse response;

            // Act
            _output.WriteLine("Checking owner....");
            response = await HyperlinkDAO.isUserOwnerAsync(userID, itineraryID);
            _output.WriteLine(response.Message);

            // Assert
            Assert.True(response.IsSuccessful);
        }

        // Test to determine if a user if the owner of an itinerary
        [Theory]
        [InlineData(2, 5)]  // User does NOT own the itinerary
        public async void UserDoesNotOwnTest(int userID, int itineraryID)
        {
            // Arrange
            HyperlinkDAO HyperlinkDAO = new HyperlinkDAO(_dbContext);

            // Act
            _output.WriteLine("Checking owner....");
            HyperlinkResponse response = await HyperlinkDAO.isUserOwnerAsync(userID, itineraryID);
            _output.WriteLine(response.Message);

            // Assert
            Assert.False(response.IsSuccessful);
        }

        // Test to remove existing user from itinerary with a given permission
        [Theory]
        [InlineData("spartucus@gmail.com")]      // Email exists in DB
        public async void RemoveExistingUserTest(string email)
        {
            // Arrange
            HyperlinkDAO hyperlinkDAO = new HyperlinkDAO(_dbContext);
            UserAccountRecordResponse user;
            HyperlinkResponse response;
            Random rand = new Random();

            // Act
            _output.WriteLine("Removing user from itinerary....");
            user = await hyperlinkDAO.GetUserAccountRecordAsync(email);
            response = await hyperlinkDAO.RemoveUserFromItineraryAsync(user.Data, 9, "View");
            _output.WriteLine(response.Message);

            // Assert 
            Assert.True(response.IsSuccessful);
        }

        // Test to remove non-existent user from itinerary with a given permission
        [Theory]
        [InlineData("DoesNotExist@email.com")]  // Email does NOT exist in DB
        public async void RemoveNonExistingUserTest(string email)
        {
            // Arrange
            HyperlinkDAO hyperlinkDAO = new HyperlinkDAO(_dbContext);
            UserAccountRecordResponse user;
            HyperlinkResponse response;
            Random rand = new Random();

            // Act
            _output.WriteLine("Removing user from itinerary....");
            user = await hyperlinkDAO.GetUserAccountRecordAsync(email);
            response = await hyperlinkDAO.RemoveUserFromItineraryAsync(user.Data, 9, "View");
            _output.WriteLine(response.Message);

            // Assert 
            Assert.False(response.IsSuccessful);
        }

        // Test to add user to itinerary in HyperlinkManager
        [Theory]
        [InlineData(8, 9, "jdcramos@gmail.com", "View")]
        public async void AddUserManagerTest(int userID, int itineraryID, string email, string permission)
        {
            // Arrange
            HyperlinkDAO hyperlinkDAO = new HyperlinkDAO(_dbContext);
            HyperlinkManager hyperlinkManager = new HyperlinkManager(hyperlinkDAO, _dbContext);
            HyperlinkResponse response;

            // Act
            _output.WriteLine("Adding User from HyperlinkManager....");
            response = await hyperlinkManager.AddUserToItineraryAsync(userID, itineraryID, email, permission);
            _output.WriteLine(response.Message);

            // Assert
            Assert.True(response.IsSuccessful);
        }

        // Test to remove user to itinerary in HyperlinkManager
        [Theory]
        [InlineData(8, 9, "jdcramos@gmail.com", "View")]
        public async void RemoveUserManagerTest(int userID, int itineraryID, string email, string permission)
        {
            // Arrange
            HyperlinkDAO hyperlinkDAO = new HyperlinkDAO(_dbContext);
            HyperlinkManager hyperlinkManager = new HyperlinkManager(hyperlinkDAO, _dbContext);
            HyperlinkResponse response;

            // Act
            _output.WriteLine("Removing User from HyperlinkManager....");
            response = await hyperlinkManager.RemoveUserFromItineraryAsync(userID, itineraryID, email, permission);
            _output.WriteLine(response.Message);

            // Assert
            Assert.True(response.IsSuccessful);
        }

        // Test for checking valid numericality
        [Theory]
        [InlineData(8)]
        public void IsValidNumericalityTest(int number)
        {
            // Arrange
            bool isValidNumber; 

            // Act
            _output.WriteLine("Validating number....");
            isValidNumber = Validator.IsValidNumber(number);

            // Assert
            Assert.True(isValidNumber);
        }

        // Test for checking invalid numericality
        [Theory]
        [InlineData(0)]
        [InlineData(-123)]
        public void IsInvalidNumericalityTest(int number)
        {
            // Arrange
            bool isValidNumber;

            // Act
            _output.WriteLine("Validating number....");
            isValidNumber = Validator.IsValidNumber(number);

            // Assert
            Assert.False(isValidNumber);
        }

        // Test for checking valid email
        [Theory]
        [InlineData("jdcramos@gmail.com")]
        public void IsValidEmailTest(string email)
        {
            // Arrange
            bool isValidEmail;

            // Act
            _output.WriteLine("Validating email....");
            isValidEmail = Validator.IsValidEmail(email);

            // Assert
            Assert.True(isValidEmail);
        }

        // Test for checking invalid email
        [Theory]
        [InlineData("abcdef")]
        [InlineData("abcdef@gmailcom")]
        public void IsInvalidEmailTest(string email)
        {
            // Arrange
            bool isValidEmail;

            // Act
            _output.WriteLine("Validating email....");
            isValidEmail = Validator.IsValidEmail(email);

            // Assert
            Assert.False(isValidEmail);
        }
    }
}
