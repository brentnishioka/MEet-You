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
        private IItineraryDAO _itineraryDAO;
        private UPDManager _updManager;
        private IUserDAO _userDAO;
        private MEetAndYouDBContext _context;
        private ISuggestionManager _suggestionManager;



        public UPDController(IItineraryDAO itineraryDAO, UPDManager updManager, IUserDAO userDAO, MEetAndYouDBContext dBContext, ISuggestionManager suggestionManager)
        {
            //this.authorizationManager = authorizationManager;
            _itineraryDAO = itineraryDAO;
            _updManager = updManager;
            _userDAO = userDAO;
            _context = dBContext;
            _suggestionManager = suggestionManager;

        }
        /// <summary>
        /// HTTP get method for fetching the itineraries of the user 
        /// </summary>
        /// <param name="id"> id of the user</param
        /// <returns> Itinerary response</returns>
        [HttpGet]
        [Route("/GetUPDData")]
        public async Task<ActionResult<ItineraryResponse>> GetUPDData(int id)
        {
            ItineraryResponse result = await _suggestionManager.GetUserItineraries(id);
            return result;
        } 
    }
}
