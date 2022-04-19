using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Managers;

namespace WeatherDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CopyItineraryController : ControllerBase
    {
        private readonly CopyManager _copyManager;
        private readonly MEetAndYouDBContext _dbcontext;

        public CopyItineraryController(CopyManager copyManager, MEetAndYouDBContext dbcontext)
        {
            _copyManager = copyManager;
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("CopyItinerary/{itineraryID}")]
        public ActionResult<Itinerary> CopyItinerary (int itineraryID)
        {
            return _copyManager.LoadItineraryInfo (itineraryID);
        }

        [HttpPost]
        [Route("AddEvent")]
        public ActionResult<bool> AddEvent()
        {
            bool result = _copyManager.AddEvent();
            return result;
        }

        [HttpGet]
        [Route("GetItineraries")]
        public ActionResult<List<Itinerary>> GetAllItinerary(int userID)
        {
            List<Itinerary> result =  (_copyManager.GetItineraries(userID)).ToList();
            return result;
        }
    }
}
