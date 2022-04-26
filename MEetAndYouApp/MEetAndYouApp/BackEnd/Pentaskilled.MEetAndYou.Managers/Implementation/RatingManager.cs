using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Pentaskilled.MEetAndYou.Services.Contracts;
using Pentaskilled.MEetAndYou.Services.Implementation;

namespace Pentaskilled.MEetAndYou.Managers.Implementation
{
    public class RatingManager : IRatingManager
    {
        private readonly IRatingService _ratingService;
        private readonly IRatingDAO _ratingDAO;
        private readonly MEetAndYouDBContext _dbcontext;

        public RatingManager(IRatingService _ratingService, IRatingDAO _ratingDAO, MEetAndYouDBContext dbcontext)
        {
            this._ratingService = _ratingService;
            this._ratingDAO = _ratingDAO;
            _dbcontext = dbcontext;
        }

        public BaseResponse CreateItineraryNote(int itineraryID, string noteContent)
        {
            throw new NotImplementedException();
        }

        public BaseResponse CreateRating(int eventID, int itineraryID, int userRating)
        {
            throw new NotImplementedException();
        }

        public BaseResponse ModifyItineraryNote(int itineraryID, string noteContent)
        {
            throw new NotImplementedException();
        }

        public BaseResponse ModifyRating(int eventID, int itineraryID, int userRating)
        {
            throw new NotImplementedException();
        }
    }
}
