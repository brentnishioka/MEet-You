using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.DataAccess.Implementation
{
    public class HyperlinkDAO : IHyperlinkDAO
    {
        private readonly MEetAndYouDBContext _dbContext;

        public HyperlinkDAO()
        {
            _dbContext = new MEetAndYouDBContext();
        }
        public HyperlinkDAO(MEetAndYouDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserAccountRecordResponse> GetUserAccountRecordByEmail(string email)
        {
            UserAccountRecord userAccountRecord;

            // Execute a LINQ-to-Entity query
            userAccountRecord = await
                (from user in _dbContext.UserAccountRecords
                 where user.UserEmail == email
                 select user).FirstOrDefaultAsync<UserAccountRecord>();

            // If userAccountRecord is null, set error message and isSuccessful to false 
            if (userAccountRecord == null)
            {
                return new UserAccountRecordResponse("User account not found", false, userAccountRecord);
            }

            // Successfully pulled UserAccountRecord from context using user email
            else
            {
                return new UserAccountRecordResponse("User successfully found", true, userAccountRecord);
            }
        }

        public async Task<HyperlinkResponse> AddUserToItinerary(UserAccountRecord userAccountRecord, int itineraryID, string permission)
        {
            Itinerary itin;

            try
            {
                // Find associated itinerary
                itin = await _dbContext.Itineraries.FindAsync(itineraryID);

                // Create userItinerary object to be added
                UserItinerary userItinerary = new UserItinerary(userAccountRecord.UserId, itineraryID, permission);

                // Add object to context
                itin.UserItineraries.Add(userItinerary);
                _dbContext.Entry(itin).State = EntityState.Modified;

                // Save changes to context
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return new HyperlinkResponse("User already added", false, null);
            }

            return new HyperlinkResponse("User successfully added", true, itin);
        }

        public async Task<HyperlinkResponse> RemoveUserFromItinerary(UserAccountRecord userAccountRecord, int itineraryID, string permission)
        {
            Itinerary itin;

            try
            {
                // Find associated itinerary
                itin = await _dbContext.Itineraries.FindAsync(itineraryID);

                // Create userItinerary object to be removed
                UserItinerary userItinerary = new UserItinerary(userAccountRecord.UserId, itineraryID, permission);

                // Find object from context
                var user = await _dbContext.UserItineraries.FirstOrDefaultAsync(u => u == userItinerary);

                // Remove object from context
                itin.UserItineraries.Remove(user);
                _dbContext.Entry(itin).State = EntityState.Modified;

                // Save changes to context
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return new HyperlinkResponse("Database failed to remove user", false, null);
            }

            return new HyperlinkResponse("User successfully removed", true, itin);
        }

        public async Task<HyperlinkResponse> isUserOwner(int userID, int itineraryID)
        {
            HyperlinkResponse response;

            try
            {
                // Find associated itinerary
                Itinerary itin = await _dbContext.Itineraries.FindAsync(itineraryID);

                // Compares user ID with itinerary owner's ID
                if (userID == itin.ItineraryOwner)
                {
                    response = new HyperlinkResponse("User owns the itinerary", true, itin);
                }
                else
                {
                    response = new HyperlinkResponse("User does not own the itinerary", false, null);
                }

            }
            catch (SqlException ex)
            {
                return new HyperlinkResponse("Could not find itinerary", false, null);
            }

            return response;
        }
    }
}
