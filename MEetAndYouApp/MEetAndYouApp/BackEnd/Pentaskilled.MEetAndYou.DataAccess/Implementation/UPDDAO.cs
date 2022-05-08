using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
namespace Pentaskilled.MEetAndYou.DataAccess.Implementation
{
    public class UPDDAO : IUPDDAO
    {

        private readonly MEetAndYouDBContext _dbcontext;

        public UPDDAO(MEetAndYouDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<ItineraryResponse> GetItineraryAsync(int userID)
        {
            if (userID > 0)
            {
                List<Itinerary> itinerary = null;
                try
                {
                    // LINQ query to get the list of itineraries which match the given user ID & itinerary ID
                    itinerary = await
                        (from itin in _dbcontext.Itineraries.Include("Events")
                         where itin.ItineraryOwner == userID 
                         select itin).ToListAsync<Itinerary>();
                }
                catch (SqlException ex)
                {
                    return new ItineraryResponse("An error occurred when retrieving the itinerary from the database." + ex.Message, false, itinerary);
                }
                catch (Exception ex)
                {
                    return new ItineraryResponse("An error occurred when retrieving the itineraries." + ex.Message, false, itinerary);
                }
                return new ItineraryResponse("The itinerary was retrieved successfully.", true, itinerary);
            }
            return new ItineraryResponse("The itineraries could not be fetched successfully because the given user ID or itinerary ID were invalid.", false, null);
        }

        public async Task<RatingResponse> GetRatingsAsync(int itineraryID)
        {
            // Need to put the thread to sleep to prevent conflicts when two functions access
            // the DBContext object simultaneously.
            Thread.Sleep(200);

            // Input validation for the ID
            if (itineraryID > 0)
            {
                List<UserEventRating> userEventRatings = null;
                try
                {
                    // LINQ query to get the list of user event ratings which matches the given itinerary ID
                    userEventRatings = await
                        (from ratings in _dbcontext.UserEventRatings
                         where ratings.ItineraryId == itineraryID
                         select ratings).ToListAsync<UserEventRating>();
                }
                catch (SqlException ex)
                {
                    return new RatingResponse("An error occurred when retrieving the user's event ratings from the database." + ex.Message, false, userEventRatings);
                }
                catch (Exception ex)
                {
                    return new RatingResponse("An error occurred when retrieving the user's event ratings." + ex.Message, false, userEventRatings);
                }
                return new RatingResponse("The user's event ratings were retrieved successfully.", true, userEventRatings);
            }
            return new RatingResponse("The ratings could not be fetched successfully because the given itinerary ID is invalid.", false, null);
        }

        public async Task<NoteResponse> GetNoteAsync(int itineraryID)
        {
            // Input validation for the ID
            if (itineraryID > 0)
            {
                List<ItineraryNote> itineraryNote = null;
                try
                {
                    // LINQ query to get the note which matches the given itinerary ID
                    itineraryNote = await
                        (from notes in _dbcontext.ItineraryNotes
                         where notes.ItineraryId == itineraryID
                         select notes).ToListAsync<ItineraryNote>();
                }
                catch (SqlException ex)
                {
                    return new NoteResponse("An error occurred when retrieving the user's notes from the database." + ex.Message, false, itineraryNote);
                }
                catch (Exception ex)
                {
                    return new NoteResponse("An error occurred when retrieving the user's event ratings." + ex.Message, false, itineraryNote);
                }
                return new NoteResponse("The user's itinerary note was retrieved successfully.", true, itineraryNote);
            }
            return new NoteResponse("The notes could not be fetched successfully because the given itinerary ID is invalid.", false, null);
        }

        /// <summary>
        /// Method to get user account info from the database
        /// </summary>
        /// <param name="userID"> id of the user</param>
        /// <returns> User account record </returns>
        public async Task<UserAccountRecordResponse> getUserAccount(int userID)
        {
            UserAccountRecord userAccountRecord;

            // Execute a LINQ-to-Entity query
            userAccountRecord = await
                (from user in _dbcontext.UserAccountRecords
                 where user.UserId == userID
                 select user).FirstOrDefaultAsync<UserAccountRecord>();

            // If userAccountRecord is null, set error message and isSuccessful to false 
            if (userAccountRecord == null)
            {
                return new UserAccountRecordResponse("Unable to find user by ID", false, userAccountRecord);
            }

            // Successfully pulled UserAccountRecord from context using user email
            else
            {
                return new UserAccountRecordResponse("Successfully found user by ID", true, userAccountRecord);
            }
        }
    }
}
