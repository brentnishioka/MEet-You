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

        public SuggestionController(ISuggestionManager suggestionManager, MEetAndYouDBContext dbContext, ISuggestionDAO suggestionDAO, IAPIService eventAPIService)
        {
            _suggestionManager = suggestionManager;
            //_authorizationManager = authorizationManager;
            _dbcontext = dbContext;
            _eventAPIService = eventAPIService;
            _suggestionDAO = suggestionDAO;
        }

        [HttpGet]
        [Route("/GetEvent")]
        public ActionResult<SuggestionResponse> GetEvent(string category, string location, DateTime date)
        {
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
            SuggestionResponse result = _suggestionManager.GetEvents(category, location, date);
            return result;
        }

        [HttpPost]
        [Route("/SaveEvent")]
        public async Task<ActionResult<BaseResponse>> SaveEvent(List<Event> events, int itinID)
        {
            //string? token;
            //int userID;
            //string userToken;
            //string role;

            //token = Request.Headers["Token"];
            //if (token == null)
            //{
            //    return BadRequest("Null token");
            //}
            ////Splits the token into userID, userToken, and role for Authorization method 
            //userID = (int)token.Split(",").Select(Int32.Parse).ElementAt(0);
            //userToken = token.Split(",")[1];
            //role = token.Split(",")[2];
            ////Checks if the user is authorized before continuing 
            //if (!_authorizationManager.IsAuthorized(userID, userToken, role))
            //{
            //    return BadRequest("User is not authorized to use this service");
            //}

            BaseResponse response = await _suggestionManager.SaveEventAsync(events, itinID);
            return response;
        }

        [HttpGet]
        [Route("/GetSuggestion")]
        public async Task<ActionResult<SuggestionResponse>> GetRandomEvent()
        {
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
            SuggestionResponse result = await _suggestionManager.GetRandomEventsAsync();
            return result;
        }
    }
}
