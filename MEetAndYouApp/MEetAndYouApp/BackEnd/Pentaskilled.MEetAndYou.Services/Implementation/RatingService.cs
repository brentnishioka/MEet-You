using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Services.Contracts;

namespace Pentaskilled.MEetAndYou.Services.Implementation
{
    public class RatingService : IRatingService
    {
        private readonly IRatingDAO _ratingDAO;

        public RatingService(IRatingDAO _ratingDAO)
        {
            this._ratingDAO = _ratingDAO;
        }

        public async Task<BaseResponse> LogCreatedNoteAsync(ItineraryNote itineraryNote)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> LogCreatedRatingAsync(UserEventRating userRating)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> LogModifiedNoteAsync(ItineraryNote itineraryNote)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> LogModifiedRatingAsync(UserEventRating userRating)
        {
            throw new NotImplementedException();
        }
    }
}
