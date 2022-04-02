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
        [Route("Signin")]
        public Object SignIn(string userEmail, string userPassword)
        {
            AuthnManager authnManager = new AuthnManager();
            string token = authnManager.AuthenticateUser(userEmail, userPassword);

            return Ok(token);
            //return new ObjectResult(new { Value = token });
        }
    }
}
