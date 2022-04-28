using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Microsoft.AspNetCore.Mvc;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Managers;

namespace Pentaskilled.MEetAndYou.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CopyItineraryController : ControllerBase
    {
        private readonly CopyManager _copyManager;
        private readonly MEetAndYouDBContext _dbcontext;
        private readonly CopyItineraryDAO _copyItineraryDAO;

        public CopyItineraryController(CopyManager copyManager, MEetAndYouDBContext dbcontext, CopyItineraryDAO copyItineraryDAO)
        {
            _copyManager = copyManager;
            _dbcontext = dbcontext;
            _copyItineraryDAO = copyItineraryDAO;
        }

        [HttpGet]
        [Route("CopyItinerary/{itineraryID}")]
        public ActionResult<Itinerary> CopyItinerary(int itineraryID)
        {
            return _copyManager.LoadItineraryInfo(itineraryID);
        }
    }
}
