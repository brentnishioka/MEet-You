using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pentaskilled.MEetAndYou.Managers;

namespace API.Controllers
{
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
