using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Pentaskilled.MEetAndYou.Managers.Implementation;
using Pentaskilled.MEetAndYou.Services.Contracts;

namespace Pentaskiled.MEetAndYou.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionController : ControllerBase
    {
        private readonly ISuggestionManager _suggestionManager;
        //private readonly AuthorizationManager _authorizationManager;
        private readonly MEetAndYouDBContext _dbcontext;
        private readonly ISuggestionDAO _suggestionDAO;
        private readonly IAPIService _eventAPIService;
        private readonly IAuthorizationManager _authzManager;

        public SuggestionController(ISuggestionManager suggestionManager, MEetAndYouDBContext dbContext, ISuggestionDAO suggestionDAO, IAPIService eventAPIService, IAuthorizationManager authorizationManager)
        {
            _suggestionManager = suggestionManager;
            //_authorizationManager = authorizationManager;
            _dbcontext = dbContext;
            _eventAPIService = eventAPIService;
            _suggestionDAO = suggestionDAO;
            _authzManager = authorizationManager;
        }

        [HttpGet]
        [Route("/GetEvent")]
        public async Task<ActionResult<SuggestionResponse>> GetEvent(string category, string location, DateTime date)
        {
            try
            {
                var userIDString = Request.Headers["userID"];
                int userID = int.Parse(userIDString);
                var userToken = Request.Headers["token"];
                var role = Request.Headers["roles"];

                bool response = _authzManager.IsAuthorized(userID, userToken, role);
                if (response)
                {
                    SuggestionResponse result = await _suggestionManager.GetEvents(category, location, date);
                    return result;
                }
                return BadRequest("You are not authorized to use this feature.");
            }
            catch (Exception ex)
            {
                return BadRequest("Verification met a problem! " + ex.Message);
            }
                //if (token == null)
                //{
                //    return BadRequest("Invalid Token");
                //}
                //bool isAuthroized = _authzManager.IsAuthorized();
                //if (isAuthroized == false)
                //{
                //    return BadRequest("Not authorized");
                //    throw new HttpResponseException();
                //}
                //else
                //{
                //    // Call the manager to execute the feature. 
                //}
        }

        [HttpPost]
        [Route("/SaveEvent")]
        public async Task<ActionResult<BaseResponse>> SaveEvent(List<Event> events, int itinID)
        {
            try
            {
                var userIDString = Request.Headers["userID"];
                int userID = int.Parse(userIDString);
                var userToken = Request.Headers["token"];
                var role = Request.Headers["roles"];

                bool response = _authzManager.IsAuthorized(userID, userToken, role);
                if (response)
                {
                    BaseResponse sResponse = await _suggestionManager.SaveEventAsync(events, itinID, userID);
                    return sResponse;
                }
                return BadRequest("You are not authorized to use this feature.");
            }
            catch (Exception ex)
            {
                return BadRequest("Verification met a problem! " + ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetRandomEvent")]
        public async Task<ActionResult<SuggestionResponse>> GetRandomEvent()
        {
            try
            {
                var userIDString = Request.Headers["userID"];
                int userID = int.Parse(userIDString);
                var userToken = Request.Headers["token"];
                var role = Request.Headers["roles"];

                bool response = _authzManager.IsAuthorized(userID, userToken, role);
                if (response)
                {
                    SuggestionResponse result = await _suggestionManager.GetRandomEventsAsync();
                    return result;
                }
                return BadRequest("You are not authorized to use this feature.");
            }
            catch (Exception ex)
            {
                return BadRequest("Verification met a problem! " + ex.Message);
            }
        }

        [HttpDelete]
        [Route("/DeleteEvent")]
        public async Task<ActionResult<BaseResponse>> DeleteEvent(int itinID, int eventID)
        {
            try
            {
                var userIDString = Request.Headers["userID"];
                int userID = int.Parse(userIDString);
                var userToken = Request.Headers["token"];
                var role = Request.Headers["roles"];

                bool response = _authzManager.IsAuthorized(userID, userToken, role);
                if (response)
                {
                    BaseResponse sResponse = await _suggestionManager.DeleteEventAsync(itinID, eventID, userID);
                    return sResponse;
                }
                return BadRequest("You are not authorized to use this feature.");
            }
            catch (Exception ex)
            {
                return BadRequest("Verification met a problem! " + ex.Message);
            }
        }

        [HttpPost]
        [Route("/AddItinerary")]
        public async Task<ActionResult<BaseResponse>> AddItinerary(List<Itinerary> itineraries)
        {
            try
            {
                var userIDString = Request.Headers["userID"];
                int userID = int.Parse(userIDString);
                var userToken = Request.Headers["token"];
                var role = Request.Headers["roles"];

                bool response = _authzManager.IsAuthorized(userID, userToken, role);
                if (response)
                {
                    BaseResponse sResponse = await _suggestionManager.AddItineraryAsync(itineraries);
                    return sResponse;
                }
                return BadRequest("You are not authorized to use this feature.");
            }
            catch (Exception ex)
            {
                return BadRequest("Verification met a problem! " + ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetUserItineraries")]
        public async Task<ActionResult<ItineraryResponse>> GetUserItineraries()
        {
            try
            {
                var userIDString = Request.Headers["userID"];
                int userID = int.Parse(userIDString);
                var userToken = Request.Headers["token"];
                var role = Request.Headers["roles"];

                bool response = _authzManager.IsAuthorized(userID, userToken, role);
                if (response)
                {
                    ItineraryResponse result = await _suggestionManager.GetUserItineraries(userID);
                    return result;
                }
                return BadRequest("You are not authorized to use this feature.");
            }
            catch (Exception ex)
            {
                return BadRequest("Verification met a problem! " + ex.Message);
            }
        }
    }
}
