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
        private readonly LoggingManager _loggingManager;

        public RatingService(IRatingDAO _ratingDAO, LoggingManager _loggingManager)
        {
            this._ratingDAO = _ratingDAO;
            this._loggingManager = _loggingManager;
        }

        public async Task<BaseResponse> LogCreatedNoteAsync(ItineraryNote itineraryNote, int userID)
        {
            try
            {
                string logMessage = "User " + userID + " added an itinerary note for itinerary with ID "
                    + itineraryNote.ItineraryId;
                await _loggingManager.BeginLogProcess("Data", Entities.LogLevel.Info, userID, logMessage);
            }
            catch (SqlException ex)
            {
                return new BaseResponse("The log for adding an itinerary note encountered a database error and could not be added.", false);
            }
            catch (Exception ex)
            {
                return new BaseResponse("The log for adding an itinerary note was not successfully created.", false);
            }
            return new BaseResponse("The log for adding an itinerary note was successfully created.", true);
        }

        public async Task<BaseResponse> LogCreatedRatingAsync(UserEventRating userEventRating, int userID)
        {
            try
            {
                string logMessage = "User " + userID + " added an event rating to " + userEventRating.UserRating 
                    + " for event " + userEventRating.EventId + " for itinerary with ID " + userEventRating.ItineraryId;
                await _loggingManager.BeginLogProcess("Data", Entities.LogLevel.Info, userID, logMessage);
            }
            catch (SqlException ex)
            {
                return new BaseResponse("The log for adding an event rating encountered a database error and could not be added.", false);
            }
            catch (Exception ex)
            {
                return new BaseResponse("The log for adding an event rating was not successfully created.", false);
            }
            return new BaseResponse("The log for adding an event rating not was successfully created.", true);
        }

        public async Task<BaseResponse> LogModifiedNoteAsync(ItineraryNote itineraryNote, int userID)
        {
            try
            {
                string logMessage = "User " + userID + " modified an itinerary note for itinerary with ID "
                    + itineraryNote.ItineraryId;
                await _loggingManager.BeginLogProcess("Data", Entities.LogLevel.Info, userID, logMessage);
            }
            catch (SqlException ex)
            {
                return new BaseResponse("The log for modifying an itinerary note encountered a database error and could not be added.", false);
            }
            catch (Exception ex)
            {
                return new BaseResponse("The log for modifying an itinerary note was not successfully created.", false);
            }
            return new BaseResponse("The log for modifying an itinerary note was successfully created.", true);
        }

        public async Task<BaseResponse> LogModifiedRatingAsync(UserEventRating userEventRating, int userID)
        {
            try
            {
                string logMessage = "User " + userID + " modified an event rating to " + userEventRating.UserRating
                    + " for event " + userEventRating.EventId + " for itinerary with ID " + userEventRating.ItineraryId;
                await _loggingManager.BeginLogProcess("Data", Entities.LogLevel.Info, userID, logMessage);
            }
            catch (SqlException ex)
            {
                return new BaseResponse("The log for modifying an event rating encountered a database error and could not be added.", false);
            }
            catch (Exception ex)
            {
                return new BaseResponse("The log for modifying an event rating was not successfully created.", false);
            }
            return new BaseResponse("The log for modifying an event rating not was successfully created.", true);
        }
    }
}
