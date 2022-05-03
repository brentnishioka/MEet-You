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

        public ItineraryResponse RetrieveUserItinerary(int userID, int itineraryID)
        {
            ItineraryResponse getUserItineraryResult = _ratingDAO.GetUserItinerary(userID, itineraryID).Result;
            return getUserItineraryResult;
        }

        public BaseResponse CreateItineraryNote(int itineraryID, string noteContent)
        {
            ItineraryNote itineraryNote = new ItineraryNote(itineraryID, noteContent);
            BaseResponse addNoteResult = _ratingDAO.AddNoteInDBAsync(itineraryNote).Result;
            return addNoteResult;
        }

        public BaseResponse CreateRating(int eventID, int itineraryID, int userRating)
        {
            UserEventRating userEventRating = new UserEventRating(eventID, itineraryID, userRating);
            BaseResponse addRatingResult = _ratingDAO.AddRatingInDBAsync(userEventRating).Result;
            //BaseResponse logAddRatingResult = _ratingService.LogCreatedNoteAsync(userEventRating);
            return addRatingResult;
            //throw new NotImplementedException();
        }

        public BaseResponse ModifyItineraryNote(int itineraryID, string noteContent)
        {
            ItineraryNote itineraryNote = new ItineraryNote(itineraryID, noteContent);
            BaseResponse modifyNoteResult = _ratingDAO.ModifyNoteInDBAsync(itineraryNote).Result;
            return modifyNoteResult;
        }

        public BaseResponse ModifyRating(int eventID, int itineraryID, int userRating)
        {
            // First, check to see if the event already has an existing rating.
            UserEventRating existingEventRating = _dbcontext.UserEventRatings.Where(x => x.EventId == eventID && x.ItineraryId == itineraryID).FirstOrDefault();
            if (existingEventRating != null)
            {
                existingEventRating.UserRating = userRating;
            }
            BaseResponse modifyRatingResult = _ratingDAO.ModifyRatingInDBAsync(existingEventRating).Result;
            return modifyRatingResult;
        }
    }
}
