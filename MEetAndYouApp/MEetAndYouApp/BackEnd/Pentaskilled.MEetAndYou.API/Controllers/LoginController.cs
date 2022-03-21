using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pentaskilled.MEetAndYou.Managers;
using Pentaskilled.MEetAndYou.Entities;
using Newtonsoft.Json.Linq;

namespace Pentaskilled.MEetAndYou.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private AuthnManager _authnManager;

        public LoginController()
        {
            _authnManager = new AuthnManager();
        }

        [HttpPost("Login")]
        public string Login([FromBody] JObject userInfo)
        {
            var email = userInfo["email"].ToString();
            var password = userInfo["password"].ToString();
            return _authnManager.AuthenticateUser(email, password);
        }
    }
}
