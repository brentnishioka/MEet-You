using Microsoft.AspNetCore.Mvc;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers;
using System.Web.Http.Cors;

namespace Pentaskilled.MEetAndYou.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [ApiController]
    [Route("[controller]")]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarManager _calendarManager;
        private readonly MEetAndYouDBContext _dbcontext;
        private readonly IAuthorizationManager _authzManager;

        public CalendarController(ICalendarManager calendarManager, MEetAndYouDBContext dbcontext, IAuthorizationManager authorizationManager) //IAuthorizationManager authorizationManager
        {
            _calendarManager = calendarManager;
            _authzManager= authorizationManager; 
            _dbcontext = dbcontext;
        }


        [HttpPost(Name = "GetItineraries")]
        //[Route("GetItineraries/{userID}")]
        public async Task<ActionResult<ItineraryResponse>> GetItineraries(string date)
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
                    return await _calendarManager.LoadUserItineraries(userID, date);
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