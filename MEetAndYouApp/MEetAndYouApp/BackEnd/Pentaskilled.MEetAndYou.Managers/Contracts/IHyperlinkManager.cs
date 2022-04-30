using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.Managers.Contracts
{
    public interface IHyperlinkManager
    {
        /*  
            Adds a user to an itinerary with the following procedure:
            1. Validate input of the arguments
            2. Check to see if the User is the owner of the itinerary
            3. Pull UserAccountRecord using an email
            4. Add the user to the associated itinerary
        */
        Task<HyperlinkResponse> AddUserToItineraryAsync(int userID, int itineraryID, string email, string permission);
        /*  
            Removes a user to an itinerary with the following procedure:
            1. Validate input of the arguments
            2. Check to see if the User is the owner of the itinerary
            3. Pull UserAccountRecord using an email
            4. Remove the user to the associated itinerary
        */
        Task<HyperlinkResponse> RemoveUserToItineraryAsync(int userID, int itineraryID, string email, string permission);
    }
}