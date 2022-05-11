using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.API.Controllers;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Pentaskilled.MEetAndYou.Managers.Implementation;
using Pentaskilled.MEetAndYou.Services.Contracts;
using Pentaskilled.MEetAndYou.Services.Implementation;
using Xunit;
using Xunit.Abstractions;

namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class RatingTests
    {
        private readonly ITestOutputHelper _output;
        private MEetAndYouDBContext _dbContext;
        public static DbContextOptions<MEetAndYouDBContext> dbContextOptions { get; }
        public static string connectionString = "Data Source=meetandyou-db.cyakceoi9n4j.us-west-1.rds.amazonaws.com;Initial Catalog=MEetAndYou-DB;User Id=admin;Password=TeostraLunastraAlatreon;Connect Timeout=30;TrustServerCertificate=True;";

        static RatingTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<MEetAndYouDBContext>()
                .UseSqlServer(connectionString)
                .Options;

        }

        public RatingTests(ITestOutputHelper output)
        {
            _output = output;
            _dbContext = new MEetAndYouDBContext(dbContextOptions);
        }

        [Theory]
        [InlineData(4, 7, 4)]
        public async void AddRatingSuccessTest(int eventID, int itineraryID, int userRating)
        {
            IRatingDAO _ratingDAO = new RatingDAO(_dbContext);
            UserEventRating rating = new UserEventRating(eventID, itineraryID, userRating);
            BaseResponse result = await _ratingDAO.AddRatingInDBAsync(rating);
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData(4, 7, 3)]
        public async void ModifyRatingSuccessTest(int eventID, int itineraryID, int userRating)
        {
            IRatingDAO _ratingDAO = new RatingDAO(_dbContext);
            UserEventRating rating = new UserEventRating(eventID, itineraryID, userRating);
            BaseResponse result = await _ratingDAO.ModifyRatingInDBAsync(rating);
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData(6, "Enjoyed the first world wonder, the other six were very underwhelming.")]
        public async void AddItineraryNoteSuccessTest(int itineraryID, string noteContent)
        {
            IRatingDAO _ratingDAO = new RatingDAO(_dbContext);
            ItineraryNote note = new ItineraryNote(itineraryID, noteContent);
            BaseResponse result = await _ratingDAO.AddNoteInDBAsync(note);
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData(6, "This is the modified message.")]
        public async void ModifyItineraryNoteSuccessTest(int itineraryID, string noteContent)
        {
            IRatingDAO _ratingDAO = new RatingDAO(_dbContext);
            ItineraryNote note = new ItineraryNote(itineraryID, noteContent);
            BaseResponse result = await _ratingDAO.ModifyNoteInDBAsync(note);
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData(5, 7)]
        public async void GetUserItinerarySuccessTest(int userID, int itineraryID)
        {
            IRatingDAO _ratingDAO = new RatingDAO(_dbContext);
            ItineraryResponse result = await _ratingDAO.GetUserItineraryAsync(userID, itineraryID);
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData(5)]
        public async  void GetUserEventRatingsSuccessTest(int itineraryID)
        {
            IRatingDAO _ratingDAO = new RatingDAO(_dbContext);
            RatingResponse result = await _ratingDAO.GetUserEventRatingsAsync(itineraryID);
            Assert.True(result.IsSuccessful);
        }        

        [Theory]
        [InlineData(5)]
        public async void GetUserItineraryNoteSuccessTest(int itineraryID)
        {
            IRatingDAO _ratingDAO = new RatingDAO(_dbContext);
            NoteResponse result = await _ratingDAO.GetUserItineraryNoteAsync(itineraryID);
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData(4, 7, 4)]
        public async void CreateRatingServiceSuccessTest(int eventID, int itineraryID, int userRating)
        {
            IRatingDAO _ratingDAO = new RatingDAO(_dbContext);
            IRatingService _ratingService = new RatingService(_ratingDAO, _dbContext);
            UserEventRating rating = new UserEventRating(eventID, itineraryID, userRating);
            
            BaseResponse result = await _ratingService.CreateRatingService(rating);
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData(6, "Enjoyed the first world wonder, the other six were very underwhelming.")]
        public async void CreateNoteServiceSuccessTest(int itineraryID, string noteContent)
        {
            IRatingDAO _ratingDAO = new RatingDAO(_dbContext);
            IRatingService _ratingService = new RatingService(_ratingDAO, _dbContext);
            ItineraryNote note = new ItineraryNote(itineraryID, noteContent);

            BaseResponse result = await _ratingService.CreateNoteService(note);
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData(4, 7, 2)]
        public async void ModifyRatingServiceSuccessTest(int eventID, int itineraryID, int userRating)
        {
            IRatingDAO _ratingDAO = new RatingDAO(_dbContext);
            IRatingService _ratingService = new RatingService(_ratingDAO, _dbContext);
            UserEventRating rating = new UserEventRating(eventID, itineraryID, userRating);

            BaseResponse result = await _ratingService.ModifyRatingService(rating);
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData(6, "This is the modified message.")]
        public async void ModifyNoteServiceSuccessTest(int itineraryID, string noteContent)
        {
            IRatingDAO _ratingDAO = new RatingDAO(_dbContext);
            IRatingService _ratingService = new RatingService(_ratingDAO, _dbContext);
            ItineraryNote note = new ItineraryNote(itineraryID, noteContent);

            BaseResponse result = await _ratingService.ModifyNoteService(note);
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData(3, 11)]
        public async void GetItineraryServiceSuccessTest(int userID, int itineraryID)
        {
            IRatingDAO _ratingDAO = new RatingDAO(_dbContext);
            IRatingService _ratingService = new RatingService(_ratingDAO, _dbContext);

            ItineraryResponse result = await _ratingService.GetItineraryService(userID, itineraryID);
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData(7)]
        public async void GetNoteServiceSuccessTest(int itineraryID)
        {
            IRatingDAO _ratingDAO = new RatingDAO(_dbContext);
            IRatingService _ratingService = new RatingService(_ratingDAO, _dbContext);

            NoteResponse result = await _ratingService.GetNoteService(itineraryID);
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData(7)]
        public async void GetUserRatingsServiceSuccessTest(int itineraryID)
        {
            IRatingDAO _ratingDAO = new RatingDAO(_dbContext);
            IRatingService _ratingService = new RatingService(_ratingDAO, _dbContext);

            RatingResponse result = await _ratingService.GetUserRatingsService(itineraryID);
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData(3, 11)]
        public async void RetrieveItineraryManagerSuccessTest(int userID, int itineraryID)
        {
            IRatingDAO _ratingDAO = new RatingDAO(_dbContext);
            IRatingService _ratingService = new RatingService(_ratingDAO, _dbContext);
            IRatingManager _ratingManager = new RatingManager(_ratingService, _dbContext);

            ItineraryResponse result = await _ratingManager.RetrieveUserItinerary(userID, itineraryID);
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData(5)]
        public async void RetrieveRatingsManagerSuccessTest(int itineraryID)
        {
            IRatingDAO _ratingDAO = new RatingDAO(_dbContext);
            IRatingService _ratingService = new RatingService(_ratingDAO, _dbContext);
            IRatingManager _ratingManager = new RatingManager(_ratingService, _dbContext);

            RatingResponse result = await _ratingManager.RetrieveUserRatings(itineraryID);
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData(5)]
        public async void RetrieveNoteManagerSuccessTest(int itineraryID)
        {
            IRatingDAO _ratingDAO = new RatingDAO(_dbContext);
            IRatingService _ratingService = new RatingService(_ratingDAO, _dbContext);
            IRatingManager _ratingManager = new RatingManager(_ratingService, _dbContext);

            NoteResponse result = await _ratingManager.RetrieveUserNote(itineraryID);
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData(5, "Test Note on Itinerary 5.")]
        public async void CreateNoteManagerSuccessTest(int itineraryID, string noteContent)
        {
            IRatingDAO _ratingDAO = new RatingDAO(_dbContext);
            IRatingService _ratingService = new RatingService(_ratingDAO, _dbContext);
            IRatingManager _ratingManager = new RatingManager(_ratingService, _dbContext);

            BaseResponse result = await _ratingManager.CreateItineraryNote(itineraryID, noteContent);
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData(5, "Modified Test Note on Itinerary 5.")]
        public async void ModifyNoteManagerSuccessTest(int itineraryID, string noteContent)
        {
            IRatingDAO _ratingDAO = new RatingDAO(_dbContext);
            IRatingService _ratingService = new RatingService(_ratingDAO, _dbContext);
            IRatingManager _ratingManager = new RatingManager(_ratingService, _dbContext);

            BaseResponse result = await _ratingManager.ModifyItineraryNote(itineraryID, noteContent);
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData(16, 5, 1)]
        public async void CreateRatingManagerSuccessTest(int eventID, int itineraryID, int userRating)
        {
            IRatingDAO _ratingDAO = new RatingDAO(_dbContext);
            IRatingService _ratingService = new RatingService(_ratingDAO, _dbContext);
            IRatingManager _ratingManager = new RatingManager(_ratingService, _dbContext);

            BaseResponse result = await _ratingManager.CreateRating(eventID, itineraryID, userRating);
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData(16, 5, 5)]
        public async void ModifyRatingManagerSuccessTest(int eventID, int itineraryID, int userRating)
        {
            IRatingDAO _ratingDAO = new RatingDAO(_dbContext);
            IRatingService _ratingService = new RatingService(_ratingDAO, _dbContext);
            IRatingManager _ratingManager = new RatingManager(_ratingService, _dbContext);

            BaseResponse result = await _ratingManager.ModifyRating(eventID, itineraryID, userRating);
            Assert.True(result.IsSuccessful);
        }

        //[Theory]
        //[InlineData(9, 19)]
        //public async void GetItineraryControllerSuccessTest(int userID, int itineraryID)
        //{
        //    IRatingDAO _ratingDAO = new RatingDAO(_dbContext);
        //    IRatingService _ratingService = new RatingService(_ratingDAO, _dbContext);
        //    IRatingManager _ratingManager = new RatingManager(_ratingService, _dbContext);
        //    RatingController _ratingController = new RatingController(_ratingManager, _dbContext);

        //    ActionResult<ItineraryResponse> result = await _ratingController.GetUserItinerary(userID, itineraryID);
        //    ItineraryResponse respObj = (ItineraryResponse)result.Result;
        //    Assert.True(respObj.IsSuccessful);
        //}
    }
}
