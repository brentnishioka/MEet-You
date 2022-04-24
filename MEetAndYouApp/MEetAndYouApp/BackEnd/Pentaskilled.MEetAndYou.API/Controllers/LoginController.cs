using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pentaskilled.MEetAndYou.Entities.DBModels;
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
        public ActionResult<AuthnResponse> SignIn(string userEmail, string userPassword)
        {
            AuthnResponse token = _authnManager.AuthenticateUser(userEmail, userPassword);

            return token;

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
