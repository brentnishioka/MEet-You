using System.Collections.Generic;
using Pentaskilled.MEetAndYou.Entities.DBModels;

namespace Pentaskilled.MEetAndYou.Managers
{
    public interface ICalendarManager
    {
        List<Itinerary> LoadUserItineraries(int userID);
    }
}