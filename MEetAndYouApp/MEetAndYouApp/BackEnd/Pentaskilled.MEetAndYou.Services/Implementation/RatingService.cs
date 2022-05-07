using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Logging;
using Pentaskilled.MEetAndYou.Services.Contracts;

namespace Pentaskilled.MEetAndYou.Services.Implementation
{
    public class RatingService : IRatingService
    {
        private readonly IRatingDAO _ratingDAO;
        private readonly MEetAndYouDBContext _dbcontext;

        public RatingService(IRatingDAO _ratingDAO, MEetAndYouDBContext dbcontext)
        {
            this._ratingDAO = _ratingDAO;
            _dbcontext = dbcontext;
        }

        // Service method to create an itinerary note.
        public async Task<BaseResponse> CreateNoteService(ItineraryNote itineraryNote)
        {
            // Input validation for the ID and note content.
            if (itineraryNote.ItineraryId > 0 && itineraryNote.NoteContent != null)
                {
                    BaseResponse addNoteResult = await _ratingDAO.AddNoteInDBAsync(itineraryNote);
                    return addNoteResult;
                }
            return new BaseResponse("The note could not be created successfully because either the given itinerary ID or note contents were not valid.", false);
        }

        // Service method to create a user event rating.
        public async Task<BaseResponse> CreateRatingService(UserEventRating userRating)
        {
            // Input validation for the ID's and user rating.
            if (userRating.EventId > 0 && userRating.ItineraryId > 0 && (userRating.UserRating >= 1 && userRating.UserRating <= 5))
            {
                BaseResponse addRatingResult = await _ratingDAO.AddRatingInDBAsync(userRating);
                return addRatingResult;
            }
            return new BaseResponse("The rating could not be created successfully because either the given event ID, itinerary ID, or user rating were invalid.", false);
        }

        // Service method to retrieve the user's itinerary note.
        public async Task<NoteResponse> GetNoteService(int itineraryID)
        {
            // Input validation for the ID
            if (itineraryID > 0)
            {
                NoteResponse getUserNoteResult = await _ratingDAO.GetUserItineraryNoteAsync(itineraryID);
                return getUserNoteResult;
            }
            return new NoteResponse("The notes could not be fetched successfully because the given itinerary ID is invalid.", false, null);
        }

        // Service method to retrieve the user's itinerary.
        public async Task<ItineraryResponse> GetItineraryService(int userID, int itineraryID)
        {
            // Input validation for the ID's
            if (userID > 0 && itineraryID > 0)
            {
                ItineraryResponse getRatingFromDbResult = await _ratingDAO.GetUserItineraryAsync(userID, itineraryID);
                return getRatingFromDbResult;
            }
            return new ItineraryResponse("The itineraries could not be fetched successfully because the given user ID or itinerary ID were invalid.", false, null);
        }

        // Service method to retrieve the user's event ratings.
        public async Task<RatingResponse> GetUserRatingsService(int itineraryID)
        {
            // Input validation for the ID
            if (itineraryID > 0)
            {
                RatingResponse getUserRatingsResult = await _ratingDAO.GetUserEventRatingsAsync(itineraryID);
                return getUserRatingsResult;
            }
            return new RatingResponse("The ratings could not be fetched successfully because the given itinerary ID is invalid.", false, null);
        }

        // Service method to modify an itinerary note.
        public async Task<BaseResponse> ModifyNoteService(ItineraryNote itineraryNote)
        {
            // Input validation for the ID and note content.
            if (itineraryNote.ItineraryId > 0 && itineraryNote.NoteContent != null)
            {
                BaseResponse modifyNoteResult = await _ratingDAO.ModifyNoteInDBAsync(itineraryNote);
                return modifyNoteResult;
            }
            return new BaseResponse("The note could not be modified successfully because either the given itinerary ID or note contents were not valid.", false);
        }

        // Service method to modify a user event rating.
        public async Task<BaseResponse> ModifyRatingService(UserEventRating userRating)
        {
            // Input validation for the ID's and user rating.
            if (userRating.EventId > 0 && userRating.ItineraryId > 0 && (userRating.UserRating >= 1 && userRating.UserRating <= 5))
            {
                BaseResponse modifyRatingResult = await _ratingDAO.ModifyRatingInDBAsync(userRating);
                return modifyRatingResult;
            }
            return new BaseResponse("The rating could not be modified successfully because either the given event ID, itinerary ID, or user rating were invalid.", false);
        }
    }
}
