using System.Web.Http.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Managers;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Pentaskilled.MEetAndYou.Managers.Implementation;
using Pentaskilled.MEetAndYou.Services.Contracts;

namespace Pentaskiled.MEetAndYou.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HyperlinkController : ControllerBase
    {
        private readonly IHyperlinkManager _hyperlinkManager;
        private readonly IAuthorizationManager _authorizationManager;
        private readonly MEetAndYouDBContext _dbcontext;
        private readonly IHyperlinkDAO _hyperlinkDAO;

        public HyperlinkController(IAuthorizationManager authorizationManager, IHyperlinkManager hyperlinkManager, MEetAndYouDBContext dbContext, IHyperlinkDAO hyperlinkDAO)
        {
            _hyperlinkManager = hyperlinkManager;
            _authorizationManager = authorizationManager;
            _dbcontext = dbContext;
            _hyperlinkDAO = hyperlinkDAO;
        }

        [HttpPost]
        [Route("/AddUser")]
        public async Task<ActionResult<HyperlinkResponse>> AddUser(int itineraryID, string email, string permission)
        {
            bool isAuthorized;

            try
            {
                var userIDString = Request.Headers["userID"];
                int userID = int.Parse(userIDString);
                var userToken = Request.Headers["token"];
                var role = Request.Headers["roles"];

                isAuthorized = _authorizationManager.IsAuthorized(userID, userToken, role);

                if (isAuthorized)
                {
                    HyperlinkResponse hyResponse = await _hyperlinkManager.AddUserToItineraryAsync(userID, itineraryID, email, permission);
                    return hyResponse;
                }

                return BadRequest("You are not authorized to use this feature.");
            }
            catch (Exception ex)
            {
                return BadRequest("Verification met a problem! " + ex.Message);
            }
        }

        [HttpDelete]
        [Route("/RemoveUser")]
        public async Task<ActionResult<HyperlinkResponse>> RemoveUser(int itineraryID, string email, string permission)
        {
            bool isAuthorized;

            try
            {
                var userIDString = Request.Headers["userID"];
                int userID = int.Parse(userIDString);
                var userToken = Request.Headers["token"];
                var role = Request.Headers["roles"];

                isAuthorized = _authorizationManager.IsAuthorized(userID, userToken, role);

                // If authorized is true, allow feature access
                if (isAuthorized)
                {
                    HyperlinkResponse hyResponse = await _hyperlinkManager.RemoveUserFromItineraryAsync(userID, itineraryID, email, permission);
                    return hyResponse;
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
