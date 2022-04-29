using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.DataAccess.Implementation
{
    public interface IHyperlinkDAO
    {
        // Given an email, find the userAccountRecord associated to the itinerary
        Task<UserAccountRecordResponse> GetUserAccountRecordAsync(string email);

        // Given an itineraryID, adds a userAccountRecord to the itinerary by using navigation from EF
        // Adds the UserAccountID and Itinerary to junction table UserItinerary
        Task<HyperlinkResponse> AddUserToItineraryAsync(UserAccountRecord userAccountRecord, int itineraryID, string permission);

        // Given an itineraryID, removes a userAccountRecord from the itinerary by using navigation from EF
        // Removes the UserAccountID and Itinerary from junction table UserItinerary
        Task<HyperlinkResponse> RemoveUserFromItineraryAsync(UserAccountRecord userAccountRecord, int itineraryID, string permission);

        // Compares the userID with the ownerID of the itinerary
        Task<HyperlinkResponse> isUserOwnerAsync(int userID, int itineraryID);
    }
}
