using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Services.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Pentaskilled.MEetAndYou.Services.Contracts;
using SerpApi;

namespace Pentaskilled.MEetAndYou.Managers.Implementation
{
    public class HyperlinkManager : IHyperlinkManager
    {
        private readonly MEetAndYouDBContext _dbContext;
        private readonly IHyperlinkDAO _hyperlinkDAO;

        public HyperlinkManager(IHyperlinkDAO hyperlinkDAO, MEetAndYouDBContext dbContext)
        {
            _hyperlinkDAO = hyperlinkDAO;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Adds a user to an itinerary with the following procedure:
        ///    1. Validate input of the arguments
        ///    2. Check to see if the User is the owner of the itinerary
        ///    3. Pull UserAccountRecord using an email
        ///    4. Add the user to the associated itinerary
        /// </summary>
        /// <param name="userID"> the ID of the itinerary's owner </param>
        /// <param name="itineraryID"> the ID of the itinerary to add a user </param>
        /// <param name="email"> the email of the added user </param>
        /// <param name="permission"> the permission of the added user </param>
        /// <returns>  
        ///     A HyperlinkResponse object containting a message, operation status, and list of UserItinerary & Emails
        /// </returns>
        public async Task<HyperlinkResponse> AddUserToItineraryAsync(int userID, int itineraryID, string email, string permission)
        {
            HyperlinkResponse hyperResponse;

            try
            {
                // Validate inputs
                bool isValidUserID = Validator.IsValidNumber(userID);
                if (!isValidUserID) { return new HyperlinkResponse("Invalid user id: must be greater than 0", false, new List<UserItinerary>(), new List<string>()); }

                bool isValidItineraryID = Validator.IsValidNumber(itineraryID);
                if (!isValidItineraryID) { return new HyperlinkResponse("Invalid itinerary id: must be greater than 0", false, new List<UserItinerary>(), new List<string>()); }

                bool isValidEmail = Validator.IsValidEmail(email);
                if (!isValidEmail) { return new HyperlinkResponse("Invalid email: use proper format", false, new List<UserItinerary>(), new List<string>()); }

                bool isValidPermission = Validator.IsValidString(permission);
                if (!isValidPermission) { return new HyperlinkResponse("Invalid permission", false, new List<UserItinerary>(), new List<string>()); }

                // Check to see if the user own the itinerary
                hyperResponse = await _hyperlinkDAO.isUserOwnerAsync(userID, itineraryID);
                if (hyperResponse.IsSuccessful == false)
                {
                    return hyperResponse;
                }

                // Pull UserAccountRecord using an email
                UserAccountRecordResponse userResponse = await _hyperlinkDAO.GetUserAccountRecordAsync(email);
                if (userResponse.IsSuccessful == false)
                {
                    return new HyperlinkResponse(userResponse.Message, false, new List<UserItinerary>(), new List<string>());
                }

                // Add the user to the associated itinerary
                hyperResponse = await _hyperlinkDAO.AddUserToItineraryAsync(userResponse.Data, itineraryID, permission);
            }
            catch (Exception ex)
            {
                return new HyperlinkResponse("Add user in Manager failed: \n" + ex.Message, false, new List<UserItinerary>(), new List<string>());
            }

            return hyperResponse;
        }

        /// <summary>
        /// Removes a user from an itinerary with the following procedure:
        ///    1. Validate input of the arguments
        ///    2. Check to see if the User is the owner of the itinerary
        ///    3. Pull UserAccountRecord using an email
        ///    4. Add the user to the associated itinerary
        /// </summary>
        /// <param name="userID"> the ID of the itinerary's owner </param>
        /// <param name="itineraryID"> the ID of the itinerary to remove a user </param>
        /// <param name="email"> the email of the removed user </param>
        /// <param name="permission"> the permission of the removed user </param>
        /// <returns>  
        ///     A HyperlinkResponse object containting a message, operation status, and list of UserItinerary & Emails
        /// </returns>
        public async Task<HyperlinkResponse> RemoveUserFromItineraryAsync(int userID, int itineraryID, string email, string permission)
        {
            HyperlinkResponse hyperResponse;

            try
            {
                // Validate inputs
                bool isValidUserID = Validator.IsValidNumber(userID);
                if (!isValidUserID) { return new HyperlinkResponse("Invalid user id: must be greater than 0", false, new List<UserItinerary>(), new List<string>()); }

                bool isValidItineraryID = Validator.IsValidNumber(itineraryID);
                if (!isValidItineraryID) { return new HyperlinkResponse("Invalid itinerary id: must be greater than 0", false, new List<UserItinerary>(), new List<string>()); }

                bool isValidEmail = Validator.IsValidEmail(email);
                if (!isValidEmail) { return new HyperlinkResponse("Invalid email: use proper format", false, new List<UserItinerary>(), new List<string>()); }

                bool isValidPermission = Validator.IsValidString(permission);
                if (!isValidPermission) { return new HyperlinkResponse("Invalid permission", false, new List<UserItinerary>(), new List<string>()); }

                // Check to see if the user own the itinerary
                hyperResponse = await _hyperlinkDAO.isUserOwnerAsync(userID, itineraryID);
                if (hyperResponse.IsSuccessful == false)
                {
                    return hyperResponse;
                }

                // Pull UserAccountRecord using an email
                UserAccountRecordResponse userResponse = await _hyperlinkDAO.GetUserAccountRecordAsync(email);
                if (userResponse.IsSuccessful == false)
                {
                    return new HyperlinkResponse(userResponse.Message, false, new List<UserItinerary>(), new List<string>());
                }

                // Remove the user to the associated itinerary
                hyperResponse = await _hyperlinkDAO.RemoveUserFromItineraryAsync(userResponse.Data, itineraryID, permission);
            }

            catch (Exception ex)
            {
                return new HyperlinkResponse("Remove user in Manager failed: \n" + ex.Message, false, new List<UserItinerary>(), new List<string>());
            }

            return hyperResponse;
        }
    }
}
