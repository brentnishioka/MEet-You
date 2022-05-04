using Microsoft.AspNetCore.Mvc;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Pentaskilled.MEetAndYou.Managers.Implementation;
using Pentaskilled.MEetAndYou.Services.Contracts;

namespace Pentaskilled.MEetAndYou.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UPDController : ControllerBase
    {
        private ItineraryDAO _itineraryDAO;
        private UPDManager _updManager;
        private UserDAO _userDAO;
        private MEetAndYouDBContext _context;



        public UPDController(ItineraryDAO itineraryDAO, UPDManager updManager, UserDAO userDAO, MEetAndYouDBContext dBContext)
        {
            //this.authorizationManager = authorizationManager;
            _itineraryDAO = itineraryDAO;
            _updManager = updManager;
            _userDAO = userDAO;
            _context = dBContext;

        }

        [HttpGet]
        [Route("GetUPDData")]
        public ActionResult<UPData> GetUPDData(int id)
        {
           return this._updManager.GetUPData(id).Result;
        }
    }
}
