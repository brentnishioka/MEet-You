using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.DataAccess.Implementation
{
    public class RatingDAO : IRatingDAO
    {
        private readonly MEetAndYouDBContext _dbcontext;

        public RatingDAO(MEetAndYouDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<BaseResponse> AddRatingInDBAsync(UserEventRating userRating)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> ModifyRatingInDBAsync(UserEventRating userRating)
        {
            throw new NotSupportedException();
        }

        public async Task<BaseResponse> AddNoteInDBAsync(ItineraryNote itineraryNote)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> ModifyNoteInDBAsync(ItineraryNote itineraryNote)
        {
            throw new NotImplementedException();
        }
    }
}
