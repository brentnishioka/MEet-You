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

        public async Task<ItineraryResponse> GetUserItineraryAsync(int userID, int itineraryID)
        {
            List<Itinerary> itinerary = null;
            try
            {
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

        public async Task<RatingResponse> GetUserEventRatingsAsync(int itineraryID)
        {
            Thread.Sleep(200);
            List<UserEventRating> userEventRatings = null;
            try
            {
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

        public async Task<NoteResponse> GetUserItineraryNoteAsync(int itineraryID)
        {
            List<ItineraryNote> itineraryNote = null;
            try
            {
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

        public async Task<BaseResponse> AddRatingInDBAsync(UserEventRating userRating)
        {
            try
            {
                var task = Task.Run(() => {
                    var local = _dbcontext.Set<UserEventRating>().Local
                        .FirstOrDefault(entry => entry.ItineraryId.Equals(userRating.ItineraryId) && entry.EventId.Equals(userRating.EventId));
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

        public async Task<BaseResponse> ModifyRatingInDBAsync(UserEventRating userRating)
        {
            try
            {
                var task = Task.Run(() => {
                    var local = _dbcontext.Set<UserEventRating>().Local
                        .FirstOrDefault(entry => entry.ItineraryId.Equals(userRating.ItineraryId) && entry.EventId.Equals(userRating.EventId));
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

        public async Task<BaseResponse> AddNoteInDBAsync(ItineraryNote itineraryNote)
        {
            try
            {
                var task = Task.Run(() => {
                    var local = _dbcontext.Set<ItineraryNote>().Local
                        .FirstOrDefault(entry => entry.ItineraryId.Equals(itineraryNote.ItineraryId));
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

        public async Task<BaseResponse> ModifyNoteInDBAsync(ItineraryNote itineraryNote)
        {
            try
            {
                var task = Task.Run(() => {
                    var local = _dbcontext.Set<ItineraryNote>().Local
                        .FirstOrDefault(entry => entry.ItineraryId.Equals(itineraryNote.ItineraryId));
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
    }
}
