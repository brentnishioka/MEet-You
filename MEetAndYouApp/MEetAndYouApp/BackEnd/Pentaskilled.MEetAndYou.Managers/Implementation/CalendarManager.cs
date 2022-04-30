using System.Collections.Generic;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities.DBModels;

namespace Pentaskilled.MEetAndYou.Managers
{
    public class CalendarManager : ICalendarManager
    {
        private readonly MEetAndYouDBContext _dbContext;
        private readonly CalendarDAO _calendarDAO;

        public CalendarManager(MEetAndYouDBContext dBContext)
        {
            _dbContext = dBContext;
            _calendarDAO = new CalendarDAO();
        }


        public async Task<List<Itinerary>> LoadUserItineraries(int userID)
        {
            List<Itinerary> userItineraries = await _calendarDAO.GetUserItineraries(userID);

            return userItineraries;
        }
    }
}