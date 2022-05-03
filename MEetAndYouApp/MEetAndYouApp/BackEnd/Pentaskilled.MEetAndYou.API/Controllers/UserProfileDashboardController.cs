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
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileDashboardController : ControllerBase
    {
         // private IAuthorizationManager authorizationManager;
         private ItineraryDAO _itineraryDAO;
         private UPDManager _updManager;
         private UserDAO _userDAO;

        public UserProfileDashboardController(ItineraryDAO itineraryDAO, UPDManager updManager, UserDAO userDAO)
        {
            //this.authorizationManager = authorizationManager;
            _itineraryDAO = itineraryDAO;
            _updManager = updManager;
            _userDAO = userDAO;                         
        }




        [HttpGet]
        [Route("/GetUPDData")]
        public async Task<ActionResult<UPData>> GetUPDData(int id)
        {
            return _updManager.GetUPData(id).Result;   
        }
    }
}
