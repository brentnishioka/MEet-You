using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.Managers.Contracts
{
    public interface ISuggestionManager
    {
        Task<SuggestionResponse> GetEvents(string category, string location, DateTime date);
        Task<SuggestionResponse> GetRandomEventsAsync();
        Task<BaseResponse> SaveEventAsync(List<Event> e, int itinID, int userID);
        Task<bool> IsInCategory(string category);
        Task<BaseResponse> DeleteEventAsync(int itinID, int eventID, int userID);
        Task<BaseResponse> AddItineraryAsync(List<Itinerary> itineraries);
    }
}
