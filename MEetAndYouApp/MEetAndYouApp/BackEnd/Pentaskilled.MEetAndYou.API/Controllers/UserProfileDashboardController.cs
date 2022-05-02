using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers;
using Pentaskilled.MEetAndYou.Managers.Implementation;
using Pentaskilled.MEetAndYou.Services.Contracts;


namespace Pentaskilled.MEetAndYou.API.Controllers
{
    [Route("api/[controller")]
    [ApiController]
    public class UserProfileDashboardController : ControllerBase
    {
         private AuthorizationManager authorizationManager;
         private ItineraryDAO itineraryDAO;
         private UPDManager updManager;

        public UserProfileDashboardController(AuthorizationManager authorizationManager, ItineraryDAO itineraryDAO, UPDManager updManager)
        {
            this.authorizationManager = authorizationManager;
            this.itineraryDAO = itineraryDAO;
            this.updManager = updManager;
        }




        [HttpGet]
        [Route("/GetUPDData")]
        public async Task<ActionResult<UPData>> GetUPDData(int id)
        {

        }
    }
}
