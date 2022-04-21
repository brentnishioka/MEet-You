using Microsoft.AspNetCore.Mvc;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Managers;
namespace Pentaskilled.MEetAndYou.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarManager _calendarManager;
        private readonly MEetAndYouDBContext _dbcontext;

        public CalendarController(CalendarManager calendarManager, MEetAndYouDBContext dbcontext)
        {
            _calendarManager = calendarManager;
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("GetUserItineraries/{userID}")]
        public ActionResult<List<Itinerary>> GetUserItineraries(int userID) 
        {
            return _calendarManager.LoadUserItineraries(userID);
        }
    }
}