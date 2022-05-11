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
    public class RatingDAO : IRatingDAO
    {
        private readonly MEetAndYouDBContext _dbcontext;

        public RatingDAO(MEetAndYouDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // DAO method to retrieve the user's itinerary.
        public async Task<ItineraryResponse> GetUserItineraryAsync(int userID, int itineraryID)
        {
            // Input validation for the ID's
            if (userID > 0 && itineraryID > 0)
            {
                List<Itinerary> itinerary = null;
                try
                {
                    // LINQ query to get the list of itineraries which match the given user ID & itinerary ID
                    itinerary = await
                        (from itin in _dbcontext.Itineraries.Include("Events")
                         where itin.ItineraryOwner == userID && itin.ItineraryId == itineraryID
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

        // DAO method to retrieve the user's event ratings.
        public async Task<RatingResponse> GetUserEventRatingsAsync(int itineraryID)
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

        // DAO method to retrieve the user's itinerary note.
        public async Task<NoteResponse> GetUserItineraryNoteAsync(int itineraryID)
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

        // DAO method to add a user event rating.
        public async Task<BaseResponse> AddRatingInDBAsync(UserEventRating userRating)
        {
            // Input validation for the ID's and user rating.
            if (userRating.EventId > 0 && userRating.ItineraryId > 0 && (userRating.UserRating >= 1 && userRating.UserRating <= 5))
            {
                try
                {
                    var task = Task.Run(() => {
                        var local = _dbcontext.Set<UserEventRating>().Local
                            .FirstOrDefault(entry => entry.ItineraryId.Equals(userRating.ItineraryId) && entry.EventId.Equals(userRating.EventId));
                        // Prevents conflicts with DBContext object configuration.
                        if (local != null)
                        {
                            _dbcontext.Entry(local).State = EntityState.Detached;
                        }
                        _dbcontext.Entry(userRating).State = EntityState.Added;
                    });
                    await task;
                    int addRatingResult = await _dbcontext.SaveChangesAsync();
                }
                catch (SqlException ex)
                {
                    return new BaseResponse("An error occurred when adding the rating to the database.", false);
                }
                catch (Exception ex)
                {
                    return new BaseResponse("The rating could not be added.", false);
                }
                return new BaseResponse("The rating was successfully added.", true);
            }
            return new BaseResponse("The rating could not be created successfully because either the given event ID, itinerary ID, or user rating were invalid.", false);
        }

        // DAO method to modify a user event rating.
        public async Task<BaseResponse> ModifyRatingInDBAsync(UserEventRating userRating)
        {
            // Input validation for the ID's and user rating.
            if (userRating.EventId > 0 && userRating.ItineraryId > 0 && (userRating.UserRating >= 1 && userRating.UserRating <= 5))
            {
                try
                {
                    var task = Task.Run(() => {
                        var local = _dbcontext.Set<UserEventRating>().Local
                            .FirstOrDefault(entry => entry.ItineraryId.Equals(userRating.ItineraryId) && entry.EventId.Equals(userRating.EventId));
                        // Prevents conflicts with DBContext object configuration.
                        if (local != null)
                        {
                            _dbcontext.Entry(local).State = EntityState.Detached;
                        }
                        _dbcontext.Entry(userRating).State = EntityState.Modified;
                    });
                    await task;
                    int modifyRatingResult = await _dbcontext.SaveChangesAsync();
                }
                catch (SqlException ex)
                {
                    return new BaseResponse("An error occurred when modifying the rating in the database." + ex.Message, false);
                }
                catch (Exception ex)
                {
                    return new BaseResponse("The rating could not be modified. " + ex.Message, false);
                }
                return new BaseResponse("The rating was successfully modified.", true);
            }
            return new BaseResponse("The rating could not be modified successfully because either the given event ID, itinerary ID, or user rating were invalid.", false);
        }

        // Manager method to add an itinerary note.
        public async Task<BaseResponse> AddNoteInDBAsync(ItineraryNote itineraryNote)
        {
            // Input validation for the ID and note content.
            if (itineraryNote.ItineraryId > 0 && itineraryNote.NoteContent != null)
            {
                try
                {
                    var task = Task.Run(() => {
                        var local = _dbcontext.Set<ItineraryNote>().Local
                            .FirstOrDefault(entry => entry.ItineraryId.Equals(itineraryNote.ItineraryId));
                        // Prevents conflicts with DBContext object configuration.
                        if (local != null)
                        {
                            _dbcontext.Entry(local).State = EntityState.Detached;
                        }
                        _dbcontext.Entry(itineraryNote).State = EntityState.Added;
                    });
                    await task;
                    int addRatingResult = await _dbcontext.SaveChangesAsync();
                }
                catch (SqlException ex)
                {
                    return new BaseResponse("An error occurred when adding the note to the database.", false);
                }
                catch (Exception ex)
                {
                    return new BaseResponse("The note could not be added.", false);
                }
                return new BaseResponse("The note was successfully added.", true);
            }
            return new BaseResponse("The note could not be created successfully because either the given itinerary ID or note contents were not valid.", false);
        }

        // DAO method to modify an itinerary note.
        public async Task<BaseResponse> ModifyNoteInDBAsync(ItineraryNote itineraryNote)
        {
            // Input validation for the ID and note content.
            if (itineraryNote.ItineraryId > 0 && itineraryNote.NoteContent != null)
            {
                try
                {
                    var task = Task.Run(() => {
                        var local = _dbcontext.Set<ItineraryNote>().Local
                            .FirstOrDefault(entry => entry.ItineraryId.Equals(itineraryNote.ItineraryId));
                        // Prevents conflicts with DBContext object configuration.
                        if (local != null)
                        {
                            _dbcontext.Entry(local).State = EntityState.Detached;
                        }
                        _dbcontext.Entry(itineraryNote).State = EntityState.Modified;
                    });
                    await task;
                    int modifyRatingResult = await _dbcontext.SaveChangesAsync();
                }
                catch (SqlException ex)
                {
                    return new BaseResponse("An error occurred when modifying the note in the database.", false);
                }
                catch (Exception ex)
                {
                    return new BaseResponse("The note could not be modified.", false);
                }
                return new BaseResponse("The note was successfully modified.", true);
            }
            return new BaseResponse("The note could not be modified successfully because either the given itinerary ID or note contents were not valid.", false);
        }
    }
}
