using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pentaskilled.MEetAndYou.Managers;
using Pentaskilled.MEetAndYou.Entities;
using System.IO;

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

        [HttpDelete("delaccount")]
        public bool DeleteUserAccount()
        {
            Stream req = Request.Body;
            req.Seek(0, SeekOrigin.Begin);
            string json = new StreamReader(req).ReadToEnd();
            return _accDelManager.DeleteUser(json);
        }
    }
}
