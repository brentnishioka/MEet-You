using System.Collections.Generic;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.Managers
{
    public interface ICalendarManager
    {
        Task<ItineraryResponse> LoadUserItineraries(int userID, string date);
    }
}