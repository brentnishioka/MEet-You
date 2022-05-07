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
        //private readonly AuthorizationManager _authorizationManager;
        private readonly MEetAndYouDBContext _dbcontext;
        private readonly IHyperlinkDAO _hyperlinkDAO;

        public HyperlinkController(IHyperlinkManager hyperlinkManager, MEetAndYouDBContext dbContext, IHyperlinkDAO hyperlinkDAO)
        {
            _hyperlinkManager = hyperlinkManager;
            //_authorizationManager = authorizationManager;
            _dbcontext = dbContext;
            _hyperlinkDAO = hyperlinkDAO;
        }

        [HttpPost]
        [Route("/AddUser")]
        public async Task<ActionResult<HyperlinkResponse>> AddUser(int userID, int itineraryID, string email, string permission)
        {
            //TODO: Implement authorization
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
            try
            {
                HyperlinkResponse response = await _hyperlinkManager.AddUserToItineraryAsync(userID, itineraryID, email, permission);

                return response;
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("/RemoveUser")]
        public async Task<ActionResult<HyperlinkResponse>> RemoveUser(int userID, int itineraryID, string email, string permission)
        {
            //TODO: Implement authorization
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

            try
            {
                HyperlinkResponse response = await _hyperlinkManager.RemoveUserFromItineraryAsync(userID, itineraryID, email, permission);

                return response;
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
