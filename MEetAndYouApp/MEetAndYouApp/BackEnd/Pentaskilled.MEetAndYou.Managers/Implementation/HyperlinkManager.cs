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

        public async Task<HyperlinkResponse> AddUserToItineraryAsync(int userID, int itineraryID, string email, string permission)
        {
            HyperlinkResponse hyperResponse;

            try
            {
                // Validate inputs
                bool isValidUserID = Validator.IsValidNumericality(userID);
                if (!isValidUserID) { return new HyperlinkResponse("Invalid user id: must be greater than 0", false, new List<UserItinerary>(), new List<string>()); }

                bool isValidItineraryID = Validator.IsValidNumericality(itineraryID);
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

        public async Task<HyperlinkResponse> RemoveUserFromItineraryAsync(int userID, int itineraryID, string email, string permission)
        {
            HyperlinkResponse hyperResponse;

            try
            {
                // Validate inputs
                bool isValidUserID = Validator.IsValidNumericality(userID);
                if (!isValidUserID) { return new HyperlinkResponse("Invalid user id: must be greater than 0", false, new List<UserItinerary>(), new List<string>()); }

                bool isValidItineraryID = Validator.IsValidNumericality(itineraryID);
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
