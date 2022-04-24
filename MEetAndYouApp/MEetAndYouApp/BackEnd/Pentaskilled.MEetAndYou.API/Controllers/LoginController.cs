using Microsoft.AspNetCore.Mvc;
using Pentaskilled.MEetAndYou.Managers;
using System.Web.Http.Cors;

namespace Pentaskilled.MEetAndYou.API.Controllers
{
    [EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AuthnManager _authnManager;

        public LoginController(AuthnManager authnManager)
        {
            _authnManager = authnManager;
        }

        //Method to login
        [HttpPost]
        [Route("SignIn")]
        public ActionResult<string> SignIn(string userEmail, string userPassword)
        {
            string token = _authnManager.AuthenticateUser(userEmail, userPassword);

            return Ok(token);
            //return new ObjectResult(new { Value = token });
        }

        [HttpDelete]
        [Route("SignOut")]
        public ActionResult<string> SignOut(int userID)
        {
            bool isSignOut = _authnManager.SignOut(userID);

            return Ok(isSignOut);
        }
    }
}
