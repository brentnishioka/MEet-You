using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
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
                // TODO: Validate inputs
                

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
                    return new HyperlinkResponse(userResponse.Message, false, null);
                }

                // Add the user to the associated itinerary
                hyperResponse = await _hyperlinkDAO.AddUserToItineraryAsync(userResponse.Data, itineraryID, permission);
                if (hyperResponse.IsSuccessful == false)
                {
                    return hyperResponse;
                }
            }

            catch (Exception ex)
            {
                return new HyperlinkResponse("Add user in Manager failed: \n" + ex.Message, false, null);
            }

            return hyperResponse;
        }

        public Task<HyperlinkResponse> RemoveUserToItineraryAsync(int userID, int itineraryID, string email, string permission)
        {
            throw new NotImplementedException();
        }
    }
}
