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
        private readonly MEetAndYouDBContext _dbcontext;

        public RatingManager(IRatingService _ratingService, MEetAndYouDBContext dbcontext)
        {
            this._ratingService = _ratingService;
            _dbcontext = dbcontext;
        }

        public async Task<ItineraryResponse> RetrieveUserItinerary(int userID, int itineraryID)
        {
            if (userID > 0 && itineraryID > 0)
            {
                ItineraryResponse getUserItineraryResult = await _ratingService.GetItineraryService(userID, itineraryID);
                return getUserItineraryResult;
            }
            return new ItineraryResponse("The itineraries could not be fetched successfully because the given user ID or itinerary ID were invalid.", false, null);
        }

        public async Task<RatingResponse> RetrieveUserRatings(int itineraryID)
        {
            if (itineraryID > 0)
            {
                RatingResponse getUserRatingsResult = await _ratingService.GetUserRatingsService(itineraryID);
                return getUserRatingsResult;
            }
            return new RatingResponse("The ratings could not be fetched successfully because the given itinerary ID is invalid.", false, null);
        }

        public async Task<NoteResponse> RetrieveUserNote(int itineraryID)
        {
            if (itineraryID > 0)
            {
                NoteResponse getUserNoteResult = await _ratingService.GetNoteService(itineraryID);
                return getUserNoteResult;
            }
            return new NoteResponse("The notes could not be fetched successfully because the given itinerary ID is invalid.", false, null);
        }

        public async Task<BaseResponse> CreateItineraryNote(int itineraryID, string noteContent)
        {
            if (itineraryID > 0 && noteContent != null)
            {
                ItineraryNote itineraryNote = new ItineraryNote(itineraryID, noteContent);
                BaseResponse addNoteResult = await _ratingService.CreateNoteService(itineraryNote);
                return addNoteResult;
            }
            return new BaseResponse("The note could not be created successfully because either the given itinerary ID or note contents were not valid.", false);
        }

        public async Task<BaseResponse> CreateRating(int eventID, int itineraryID, int userRating)
        {
            if (eventID > 0 && itineraryID > 0 && (userRating >= 1 && userRating <= 5))
            {
                UserEventRating userEventRating = new UserEventRating(eventID, itineraryID, userRating);
                BaseResponse addRatingResult = await _ratingService.CreateRatingService(userEventRating);
                return addRatingResult;
            }
            return new BaseResponse("The rating could not be created successfully because either the given event ID, itinerary ID, or user rating were invalid.", false);
        }

        public async Task<BaseResponse> ModifyItineraryNote(int itineraryID, string noteContent)
        {
            if (itineraryID > 0 && noteContent != null)
            {
                ItineraryNote itineraryNote = new ItineraryNote(itineraryID, noteContent);
                BaseResponse modifyNoteResult = await _ratingService.ModifyNoteService(itineraryNote);
                return modifyNoteResult;
            }
            return new BaseResponse("The note could not be modified successfully because either the given itinerary ID or note contents were not valid.", false);
        }

        public async Task<BaseResponse> ModifyRating(int eventID, int itineraryID, int userRating)
        {
            if (eventID > 0 && itineraryID > 0 && (userRating >= 1 && userRating <= 5))
            {
                UserEventRating userEventRating = new UserEventRating(eventID, itineraryID, userRating);
                BaseResponse modifyRatingResult = await _ratingService.ModifyRatingService(userEventRating);
                return modifyRatingResult;
            }
            return new BaseResponse("The rating could not be modified successfully because either the given event ID, itinerary ID, or user rating were invalid.", false);
        }
    }
}
