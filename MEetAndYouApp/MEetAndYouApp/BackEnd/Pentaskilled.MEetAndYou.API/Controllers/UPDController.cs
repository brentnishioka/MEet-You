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
        private SuggestionManager _suggestionManager;



        public UPDController(ItineraryDAO itineraryDAO, UPDManager updManager, UserDAO userDAO, MEetAndYouDBContext dBContext, SuggestionManager suggestionManager)
        {
            //this.authorizationManager = authorizationManager;
            _itineraryDAO = itineraryDAO;
            _updManager = updManager;
            _userDAO = userDAO;
            _context = dBContext;
            _suggestionManager = suggestionManager;

        }

        [HttpGet]
        [Route("GetUPDData")]
        public Task<ActionResult<ItineraryResponse>> GetUPDData(int id)
        {
            ItineraryResponse result = _suggestionManager.GetUserItineraries(id);
            return result;
        }
    }
}
