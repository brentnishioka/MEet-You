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
        private readonly MEetAndYouDBContext _dbContext;

        public CopyItineraryController(CopyManager copyManager, MEetAndYouDBContext dBContext)
        {
            _copyManager = copyManager;
            _dbContext = dBContext;
        }

        [HttpPost]
        [Route("CopyItinerary/{itineraryID}")]
        public ActionResult<bool> CopyItinerary (int itineraryID)
        {
            throw new NotImplementedException();
        }
    }
}
