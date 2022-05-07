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
        //private readonly LoggingManager _loggingManager;

        public RatingService(IRatingDAO _ratingDAO, MEetAndYouDBContext dbcontext)
        {
            this._ratingDAO = _ratingDAO;
            _dbcontext = dbcontext;
            //this._loggingManager = _loggingManager;
        }

        public async Task<BaseResponse> CreateNoteService(ItineraryNote itineraryNote)
        {
            if (itineraryNote.ItineraryId > 0 && itineraryNote.NoteContent != null)
                {
                    BaseResponse addNoteResult = await _ratingDAO.AddNoteInDBAsync(itineraryNote);
                    return addNoteResult;
                }
            return new BaseResponse("The note could not be created successfully because either the given itinerary ID or note contents were not valid.", false);
            {
                BaseResponse addNoteResult = await _ratingDAO.AddNoteInDBAsync(itineraryNote);
                return addNoteResult;
            }
            return new BaseResponse("The note could not be created successfully because either the given itinerary ID or note contents were not valid.", false);
        }

        public async Task<BaseResponse> CreateRatingService(UserEventRating userRating)
        {
            if (userRating.EventId > 0 && userRating.ItineraryId > 0 && (userRating.UserRating >= 1 && userRating.UserRating <= 5))
            {
                BaseResponse addRatingResult = await _ratingDAO.AddRatingInDBAsync(userRating);
                return addRatingResult;
            }
            return new BaseResponse("The rating could not be created successfully because either the given event ID, itinerary ID, or user rating were invalid.", false);
        }

        public async Task<NoteResponse> GetNoteService(int itineraryID)
        {
            if (itineraryID > 0)
            {
                NoteResponse getUserNoteResult = await _ratingDAO.GetUserItineraryNoteAsync(itineraryID);
                return getUserNoteResult;
            }
            return new NoteResponse("The notes could not be fetched successfully because the given itinerary ID is invalid.", false, null);
        }

        public async Task<ItineraryResponse> GetItineraryService(int userID, int itineraryID)
        {
            if (userID > 0 && itineraryID > 0)
            {
                ItineraryResponse getRatingFromDbResult = await _ratingDAO.GetUserItineraryAsync(userID, itineraryID);
                return getRatingFromDbResult;
            }
            return new ItineraryResponse("The itineraries could not be fetched successfully because the given user ID or itinerary ID were invalid.", false, null);
        }

        public async Task<RatingResponse> GetUserRatingsService(int itineraryID)
        {
            if (itineraryID > 0)
            {
                RatingResponse getUserRatingsResult = await _ratingDAO.GetUserEventRatingsAsync(itineraryID);
                return getUserRatingsResult;
            }
            return new RatingResponse("The ratings could not be fetched successfully because the given itinerary ID is invalid.", false, null);
        }

        public async Task<BaseResponse> ModifyNoteService(ItineraryNote itineraryNote)
        {
            if (itineraryNote.ItineraryId > 0 && itineraryNote.NoteContent != null)
            {
                BaseResponse modifyNoteResult = await _ratingDAO.ModifyNoteInDBAsync(itineraryNote);
                return modifyNoteResult;
            }
            return new BaseResponse("The note could not be modified successfully because either the given itinerary ID or note contents were not valid.", false);
        }

        public async Task<BaseResponse> ModifyRatingService(UserEventRating userRating)
        {
            if (userRating.EventId > 0 && userRating.ItineraryId > 0 && (userRating.UserRating >= 1 && userRating.UserRating <= 5))
            {
                BaseResponse modifyRatingResult = await _ratingDAO.ModifyRatingInDBAsync(userRating);
                return modifyRatingResult;
            }
            return new BaseResponse("The rating could not be modified successfully because either the given event ID, itinerary ID, or user rating were invalid.", false);
        }

        //public async Task<BaseResponse> LogCreatedNoteAsync(ItineraryNote itineraryNote, int userID)
        //{
        //    try
        //    {
        //        string logMessage = "User " + userID + " added an itinerary note for itinerary with ID "
        //            + itineraryNote.ItineraryId;
        //        await _loggingManager.BeginLogProcess("Data", Entities.LogLevel.Info, userID, logMessage);
        //    }
        //    catch (SqlException ex)
        //    {
        //        return new BaseResponse("The log for adding an itinerary note encountered a database error and could not be added.", false);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse("The log for adding an itinerary note was not successfully created.", false);
        //    }
        //    return new BaseResponse("The log for adding an itinerary note was successfully created.", true);
        //}

        //public async Task<BaseResponse> LogCreatedRatingAsync(UserEventRating userEventRating, int userID)
        //{
        //    try
        //    {
        //        string logMessage = "User " + userID + " added an event rating to " + userEventRating.UserRating
        //            + " for event " + userEventRating.EventId + " for itinerary with ID " + userEventRating.ItineraryId;
        //        await _loggingManager.BeginLogProcess("Data", Entities.LogLevel.Info, userID, logMessage);
        //    }
        //    catch (SqlException ex)
        //    {
        //        return new BaseResponse("The log for adding an event rating encountered a database error and could not be added.", false);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse("The log for adding an event rating was not successfully created.", false);
        //    }
        //    return new BaseResponse("The log for adding an event rating not was successfully created.", true);
        //}

        //public async Task<BaseResponse> LogModifiedNoteAsync(ItineraryNote itineraryNote, int userID)
        //{
        //    try
        //    {
        //        string logMessage = "User " + userID + " modified an itinerary note for itinerary with ID "
        //            + itineraryNote.ItineraryId;
        //        await _loggingManager.BeginLogProcess("Data", Entities.LogLevel.Info, userID, logMessage);
        //    }
        //    catch (SqlException ex)
        //    {
        //        return new BaseResponse("The log for modifying an itinerary note encountered a database error and could not be added.", false);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse("The log for modifying an itinerary note was not successfully created.", false);
        //    }
        //    return new BaseResponse("The log for modifying an itinerary note was successfully created.", true);
        //}

        //public async Task<BaseResponse> LogModifiedRatingAsync(UserEventRating userEventRating, int userID)
        //{
        //    try
        //    {
        //        string logMessage = "User " + userID + " modified an event rating to " + userEventRating.UserRating
        //            + " for event " + userEventRating.EventId + " for itinerary with ID " + userEventRating.ItineraryId;
        //        await _loggingManager.BeginLogProcess("Data", Entities.LogLevel.Info, userID, logMessage);
        //    }
        //    catch (SqlException ex)
        //    {
        //        return new BaseResponse("The log for modifying an event rating encountered a database error and could not be added.", false);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse("The log for modifying an event rating was not successfully created.", false);
        //    }
        //    return new BaseResponse("The log for modifying an event rating not was successfully created.", true);
        //}
    }
}
