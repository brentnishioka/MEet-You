using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Pulls a UserAccountRecord object from databse by email
        /// </summary>
        /// <param name="email"> the email of the UserAccountRecord to be pulled </param>
        /// <returns>  
        ///     A UserAccountRecord object
        /// </returns>
        public async Task<UserAccountRecordResponse> GetUserAccountRecordAsync(string email)
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
                return new UserAccountRecordResponse("Unable to find user by email", false, userAccountRecord);
            }

            // Successfully pulled UserAccountRecord from context using user email
            else
            {
                return new UserAccountRecordResponse("Successfully found user by email", true, userAccountRecord);
            }
        }

        /// <summary>
        /// Adds a user to an associated itinerary
        /// </summary>
        /// <param name="userAccountRecord"> the UserAccountRecord to add to an itinerary </param>
        /// <param name="itineraryID"> the ID of the itinerary to add a user </param>
        /// <param name="permission"> the permission of the add user </param>
        /// <returns>  
        ///     A HyperlinkResponse object containting a message, operation status, and list of UserItinerary & Emails
        /// </returns>
        public async Task<HyperlinkResponse> AddUserToItineraryAsync(UserAccountRecord userAccountRecord, int itineraryID, string permission)
        {
            Itinerary itin;

            try
            {
                // Find associated itinerary
                itin = await _dbContext.Itineraries.Include(i => i.UserItineraries).FirstOrDefaultAsync(i => i.ItineraryId == itineraryID);

                // Create userItinerary object to be added
                UserItinerary userItinerary = new UserItinerary(userAccountRecord.UserId, itineraryID, permission);

                // Find object from context
                var user = await _dbContext.UserItineraries.FirstOrDefaultAsync(u => u == userItinerary);

                // LINQ to find count of unique users of an itinerary
                var uniqueUsers = await
                    (from u in _dbContext.UserItineraries
                     where u.ItineraryId == itineraryID
                     group u by new { u.ItineraryId, u.UserId } into grp
                     select new 
                     {
                         grp.Key.ItineraryId,
                         grp.Key.UserId,
                     }).CountAsync();
                
                // Checks if existing users in itinerary is less than 5
                if (uniqueUsers > 5)
                { 
                    return new HyperlinkResponse("Max users reached, please remove a user", true, itin.UserItineraries.ToList(), GetAllEmailsAsync(itin.UserItineraries.ToList()).Result);
                }

                // Add user if it does not exist in DB
                if (!itin.UserItineraries.Contains(user))
                {
                    // Add object to context
                    itin.UserItineraries.Add(userItinerary);
                    _dbContext.Entry(itin).State = EntityState.Modified;

                    // Save changes to context
                    await _dbContext.SaveChangesAsync();
                }

                else
                {
                    return new HyperlinkResponse("User already added", false, itin.UserItineraries.ToList(), GetAllEmailsAsync(itin.UserItineraries.ToList()).Result);
                }
            }
            catch (DbUpdateException)
            {
                return new HyperlinkResponse("Database failed to add user", false, new List<UserItinerary>(), new List<string>());
            }
            catch (NullReferenceException)
            {
                return new HyperlinkResponse("Database could not find user", false, new List<UserItinerary>(), new List<string>());
            }

            return new HyperlinkResponse("User successfully added", true, itin.UserItineraries.ToList(), GetAllEmailsAsync(itin.UserItineraries.ToList()).Result);
        }

        /// <summary>
        /// Removes a user to an associated itinerary
        /// </summary>
        /// <param name="userAccountRecord"> the UserAccountRecord to remove from an itinerary </param>
        /// <param name="itineraryID"> the ID of the itinerary to remove a user </param>
        /// <param name="permission"> the permission of the removed user </param>
        /// <returns>  
        ///     A HyperlinkResponse object containting a message, operation status, and list of UserItinerary & Emails
        /// </returns>
        public async Task<HyperlinkResponse> RemoveUserFromItineraryAsync(UserAccountRecord userAccountRecord, int itineraryID, string permission)
        {
            Itinerary itin;

            try
            {
                // Find associated itinerary
                itin = await _dbContext.Itineraries.Include(i => i.UserItineraries).FirstOrDefaultAsync(i => i.ItineraryId == itineraryID);

                // Do not modify if userAccountRecord ID matches Itinerary owner's ID
                if (userAccountRecord.UserId == itin.ItineraryOwner)
                {
                    return new HyperlinkResponse("Unable to remove owner permissions", false, itin.UserItineraries.ToList(), GetAllEmailsAsync(itin.UserItineraries.ToList()).Result);
                }

                // Create userItinerary object to be removed
                UserItinerary userItinerary = new UserItinerary(userAccountRecord.UserId, itineraryID, permission);

                // Find object from context
                var user = await _dbContext.UserItineraries.FirstOrDefaultAsync(u => u == userItinerary); 

                // Remove if user exists in userItinerary
                if (itin.UserItineraries.Contains(user))
                {
                    // Remove object from context
                    itin.UserItineraries.Remove(user);
                    _dbContext.Entry(itin).State = EntityState.Modified;

                    // Save changes to context
                    await _dbContext.SaveChangesAsync();
                }

                else
                {
                    return new HyperlinkResponse("User is not in itinerary", false, itin.UserItineraries.ToList(), GetAllEmailsAsync(itin.UserItineraries.ToList()).Result);
                }
                
            }
            catch (DbUpdateException)
            {
                return new HyperlinkResponse("Database failed to remove user", false, new List<UserItinerary>(), new List<string>());
            }
            catch (NullReferenceException)
            {
                return new HyperlinkResponse("Database could not find user", false, new List<UserItinerary>(), new List<string>());
            }

            return new HyperlinkResponse("User successfully removed", true, itin.UserItineraries.ToList(), GetAllEmailsAsync(itin.UserItineraries.ToList()).Result);
        }

        public async Task<HyperlinkResponse> isUserOwnerAsync(int userID, int itineraryID)
        {
            HyperlinkResponse response;

            try
            {
                // Find associated itinerary
                Itinerary itin = await _dbContext.Itineraries.FindAsync(itineraryID);

                // Compares user ID with itinerary owner's ID
                if (userID == itin.ItineraryOwner)
                { 
                    response = new HyperlinkResponse("Authorized to modify user in itinerary", true, itin.UserItineraries.ToList(), GetAllEmailsAsync(itin.UserItineraries.ToList()).Result);
                }
                else
                {
                    response = new HyperlinkResponse("Not Authorized to modify user in itinerary", false, new List<UserItinerary>(), new List<string>());
                }

            }
            catch (NullReferenceException)
            {
                return new HyperlinkResponse("Could not find itinerary", false, new List<UserItinerary>(), new List<string>());
            }

            return response;
        }

        /// <summary>
        /// Retrieves all the user's emails given a list of UserItinterary
        /// </summary>
        /// <param name="userItineraries"> the list of UserItinerary objects </param>
        /// <returns>  
        ///     A list of email strings
        /// </returns>
        public async Task<List<String>> GetAllEmailsAsync(List<UserItinerary> userItineraries)
        { 
            List<string> emails = new List<string>();
            List<int> userIDs;

            try
            {
                // Map list of UserItinerary to list of user IDs
                userIDs = userItineraries.Select(a => a.UserId).ToList();

                foreach (var id in userIDs)
                {
                    // Find UserAccountRecord by id
                    UserAccountRecord user = await _dbContext.UserAccountRecords.FindAsync(id);
                    emails.Add(user.UserEmail);
                }

                return emails;
            }
            catch (SqlException)
            {
                return new List<string>();
            }
            catch (ArgumentNullException)
            {
                return new List<string>();
            }
        }
    }
}
