using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pentaskilled.MEetAndYou.Managers;

namespace WeatherDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        //Method to login
        [HttpPost]
        [Route("SignIn")]
        public Object SignIn(string userEmail, string userPassword)
        {
            AuthnManager authnManager = new AuthnManager();
            string token = authnManager.AuthenticateUser(userEmail, userPassword);

            return Ok(token);
            //return new ObjectResult(new { Value = token });
        }

        [HttpDelete]
        [Route("SignOut")]
        public Object SignOut(int userID)
        {
            AuthnManager authnManager = new AuthnManager();
            bool isSignOut = authnManager.SignOut(userID);

            return Ok(isSignOut);
        }
    }
}
