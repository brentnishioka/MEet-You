using Microsoft.AspNetCore.Mvc;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Managers;

namespace Pentaskilled.MEetAndYou.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarManager _calendarManager;
        private readonly IAuthorizationManager _authorizationManager; 
        private readonly MEetAndYouDBContext _dbcontext;

        public CalendarController(ICalendarManager calendarManager, IAuthorizationManager authorizationManager, MEetAndYouDBContext dbcontext)
        {
            _calendarManager = calendarManager;
            _authorizationManager = authorizationManager; 
            _dbcontext = dbcontext;
        }


        [HttpPost]
        [Route("GetItineraries/{userID}")]
        public async Task<ActionResult<List<Itinerary>>> GetItineraries(int userID)
        {
            /*string? token;
            int userID;
            string userToken;
            string role; */

            try
            {
                /*token = Request.Headers["Token"]; 

                if (token == null)                 
                {
                    return BadRequest("Null token");
                }

                //Splits the token into userID, userToken, and role for Authorization method 
                userID = (int)token.Split(",").Select(Int32.Parse).ElementAt(0);
                userToken = token.Split(",")[1];
                role = token.Split(",")[2];

                //Checks if the user is authorized before continuing 
                if (!_authorizationManager.IsAuthorized(userID, userToken, role))
                {
                    return BadRequest("User is not authorized to use this service");
                }*/

                return await _calendarManager.LoadUserItineraries(userID);
            }

            catch(Exception e)
            {
                return BadRequest(e.Message); 
            }
        }
    }
}