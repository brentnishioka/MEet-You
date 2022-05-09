using Microsoft.AspNetCore.Mvc;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Managers;

namespace Pentaskilled.MEetAndYou.API.Controllers
{
    [Route("/api/Authorization")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationManager _authzManager;

        public AuthorizationController(IAuthorizationManager authzManager)
        {
            _authzManager = authzManager;
        }

        [HttpGet]
        [Route("ValidateUserCredentials")]
        public ActionResult<bool> ValidateUserCredentials(int userID, string userToken, string role)
        {
            bool response;
            try
            {
                response = _authzManager.IsAuthorized(userID, userToken, role);
            }
            catch (Exception ex)
            {
                return BadRequest("Verification met a problem!");
            }
            return response;
        }
    }
}
