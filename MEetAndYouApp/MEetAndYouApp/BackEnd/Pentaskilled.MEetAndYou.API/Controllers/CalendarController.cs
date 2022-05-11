﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IAuthorizationManager _authorizationManager; 
        private readonly MEetAndYouDBContext _dbcontext;

        public CalendarController(ICalendarManager calendarManager, MEetAndYouDBContext dbcontext, IAuthorizationManager authorizationManager) //IAuthorizationManager authorizationManager
        {
            _calendarManager = calendarManager;
            _authorizationManager = authorizationManager; 
            _dbcontext = dbcontext;
        }


        [HttpPost(Name = "GetItineraries")]
        //[Route("GetItineraries/{userID}")]
        public async Task<ActionResult<ItineraryResponse>> GetItineraries(int userID, string date)
        {
           
            /*string? token;
            string? userIDToken;
            string? role; */

            try
            {
                /*token = Request.Headers["token"]; //Takes in the userID from the browser session storage
                userIDToken = Request.Headers["userID"];
                role = Request.Headers["roles"];
                //userToken = Request.Headers[];


                if (token == null)  //Checks if token is null  
                {
                    return BadRequest("Null token");
                }

                //Splits the token into userID, userToken, and role for Authorization method 
                //userID = (int)token.Split(",").Select(Int32.Parse).ElementAt(0);
                //userToken = token.Split(",")[1];
                //role = token.Split(",")[2];

                //Checks if the user is authorized before continuing 
                if (!_authorizationManager.IsAuthorized(userID, userIDToken, role))
                {
                    return BadRequest("User is not authorized to view this page.");
                }*/

                return await _calendarManager.LoadUserItineraries(userID, date);
            }

            catch(Exception e)
            {
                return BadRequest(e.Message); 
            }
        }
    }
}