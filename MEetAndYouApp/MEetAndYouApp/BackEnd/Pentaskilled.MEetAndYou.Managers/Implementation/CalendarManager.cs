using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.DataAccess; 




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


        public List<Itinerary> LoadUserItineraries(int userID)
        {
            List<Itinerary> userItineraries = _calendarDAO.GetUserItineraries(userID).Result;

            return userItineraries;
        }
    }
}