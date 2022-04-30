using System.Collections.Generic;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;

namespace Pentaskilled.MEetAndYou.Managers
{
    public interface ICalendarManager
    {
        Task<List<Itinerary>> LoadUserItineraries(int userID);
    }
}