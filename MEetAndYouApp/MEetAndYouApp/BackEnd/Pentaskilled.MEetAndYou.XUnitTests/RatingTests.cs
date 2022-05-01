using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Xunit;

namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class RatingTests
    {
        [Theory]
        [InlineData(4, 7, 4)]
        public void AddRatingSuccessTest(int eventID, int itineraryID, int userRating)
        {
            IRatingDAO _ratingDAO = new RatingDAO(new MEetAndYouDBContext());
            UserEventRating rating = new UserEventRating(eventID, itineraryID, userRating);
            BaseResponse result = _ratingDAO.AddRatingInDBAsync(rating).Result;
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData(4, 7, 3)]
        public void ModifyRatingSuccessTest(int eventID, int itineraryID, int userRating)
        {
            IRatingDAO _ratingDAO = new RatingDAO(new MEetAndYouDBContext());
            UserEventRating rating = new UserEventRating(eventID, itineraryID, userRating);
            BaseResponse result = _ratingDAO.ModifyRatingInDBAsync(rating).Result;
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData(6, "Enjoyed the first world wonder, the other six were very underwhelming.")]
        public void AddItineraryNoteSuccessTest(int itineraryID, string noteContent)
        {
            IRatingDAO _ratingDAO = new RatingDAO(new MEetAndYouDBContext());
            ItineraryNote note = new ItineraryNote(itineraryID, noteContent);
            BaseResponse result = _ratingDAO.AddNoteInDBAsync(note).Result;
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData(6, "")]
        public void ModifyItineraryNoteSuccessTest(int itineraryID, string noteContent)
        {
            IRatingDAO _ratingDAO = new RatingDAO(new MEetAndYouDBContext());
            ItineraryNote note = new ItineraryNote(itineraryID, noteContent);
            BaseResponse result = _ratingDAO.ModifyNoteInDBAsync(note).Result;
            Assert.True(result.IsSuccessful);
        }
    }
}
