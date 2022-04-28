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
        [InlineData(1, 2, 4)]
        public void AddRatingSuccessTest(int eventID, int itineraryID, int userRating)
        {
            IRatingDAO _ratingDAO = new RatingDAO(new MEetAndYouDBContext());
            UserEventRating rating = new UserEventRating(eventID, itineraryID, userRating);
            BaseResponse result = _ratingDAO.AddRatingInDBAsync(rating).Result;
            Assert.True(result.IsSuccessful);
        }
    }
}
