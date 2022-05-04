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

        public async Task<ItineraryResponse> RetrieveUserItinerary(int userID, int itineraryID)
        {
            ItineraryResponse getUserItineraryResult = await _ratingDAO.GetUserItineraryAsync(userID, itineraryID);
            return getUserItineraryResult;
        }

        public async Task<RatingResponse> RetrieveUserRatings(int itineraryID)
        {
            RatingResponse getUserRatingsResult = await _ratingDAO.GetUserEventRatingsAsync(itineraryID);
            return getUserRatingsResult;
        }

        public async Task<BaseResponse> CreateItineraryNote(int itineraryID, string noteContent)
        {
            ItineraryNote itineraryNote = new ItineraryNote(itineraryID, noteContent);
            BaseResponse addNoteResult = await _ratingDAO.AddNoteInDBAsync(itineraryNote);
            return addNoteResult;
        }

        public async Task<BaseResponse> CreateRating(int eventID, int itineraryID, int userRating)
        {
            UserEventRating userEventRating = new UserEventRating(eventID, itineraryID, userRating);
            BaseResponse addRatingResult = await _ratingDAO.AddRatingInDBAsync(userEventRating);
            //BaseResponse logAddRatingResult = _ratingService.LogCreatedNoteAsync(userEventRating);
            return addRatingResult;
            //throw new NotImplementedException();
        }

        public async Task<BaseResponse> ModifyItineraryNote(int itineraryID, string noteContent)
        {
            ItineraryNote itineraryNote = new ItineraryNote(itineraryID, noteContent);
            BaseResponse modifyNoteResult = await _ratingDAO.ModifyNoteInDBAsync(itineraryNote);
            return modifyNoteResult;
        }

        public async Task<BaseResponse> ModifyRating(int eventID, int itineraryID, int userRating)
        {
            // First, check to see if the event already has an existing rating.
            UserEventRating existingEventRating = await _dbcontext.UserEventRatings.FindAsync(eventID, itineraryID);
            if (existingEventRating != null)
            {
                existingEventRating.UserRating = userRating;
            }
            BaseResponse modifyRatingResult = await _ratingDAO.ModifyRatingInDBAsync(existingEventRating);
            return modifyRatingResult;
        }
    }
}
