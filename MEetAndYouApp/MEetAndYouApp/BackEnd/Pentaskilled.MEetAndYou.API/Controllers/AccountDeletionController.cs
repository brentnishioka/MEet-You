using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pentaskilled.MEetAndYou.Managers;

namespace Pentaskilled.MEetAndYou.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountDeletionController : ControllerBase
    {
        private AccountDeletionManager _accDelManager;

        public AccountDeletionController()
        {
            _accDelManager = new AccountDeletionManager();
        }

        [HttpDelete("self")]
        public IActionResult DeleteUserAccount(IFormCollection formCollection)
        {
            // calls DeleteUser from UMmanager
            return Ok(_accDelManager.SelfDeleteUser());
        }
    }
}
