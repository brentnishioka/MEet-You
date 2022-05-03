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
         private UserDAO userDAO;

        public UserProfileDashboardController(AuthorizationManager authorizationManager, ItineraryDAO itineraryDAO, UPDManager updManager, UserDAO userDAO)
        {
            this.authorizationManager = authorizationManager;
            this.itineraryDAO = itineraryDAO;
            this.updManager = updManager;
            this.userDAO = userDAO;                         
        }




        [HttpGet]
        [Route("/GetUPDData")]
        public async Task<ActionResult<UPData>> GetUPDData(int id)
        {
            UPDManager manager = new UPDManager(itineraryDAO, userDAO);
            UPData userData = await manager.GetUPData(id);   
            return Ok(userData);    

        }
    }
}
