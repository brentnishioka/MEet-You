using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pentaskilled.MEetAndYou.Managers;
using Pentaskilled.MEetAndYou.Entities;
using Newtonsoft.Json.Linq;

namespace Pentaskilled.MEetAndYou.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private AuthnManager _authnManager;

        public LoginController()
        {
            _authnManager = new AuthnManager();
        }

        [HttpPost("/login")]
        public string Login([FromBody] JObject userInfo)
        {
            var email = userInfo["email"].ToString();
            var password = userInfo["password"].ToString();
            return _authnManager.AuthenticateUser(email, password);
        }
    }
}
